"""
Лабораторная работа №4. Работа с базами данных
Вариант 6: Структура данных "Человек - Фирма - Должность"
Задание:
1. Для каждого человека: «ФИО персоны», его должность, в какой фирме работает
2. Для каждой фирмы: сколько в ней сотрудников
"""

import csv
import os
from typing import List, Dict, Optional


class Person:
    """
    Класс для представления человека (сотрудника)
    """
    
    def __init__(self, person_id: int, full_name: str, position_id: int, company_id: int):
        self.id = person_id
        self.full_name = full_name
        self.position_id = position_id
        self.company_id = company_id
    
    def __str__(self):
        return f"Person(id={self.id}, name={self.full_name})"


class Position:
    """
    Класс для представления должности
    """
    
    def __init__(self, position_id: int, title: str):
        self.id = position_id
        self.title = title
    
    def __str__(self):
        return f"Position(id={self.id}, title={self.title})"


class Company:
    """
    Класс для представления фирмы
    """
    
    def __init__(self, company_id: int, name: str):
        self.id = company_id
        self.name = name
    
    def __str__(self):
        return f"Company(id={self.id}, name={self.name})"


class Database:
    """
    Класс для работы с базой данных
    Хранит связанные структуры: люди, должности, фирмы
    """
    
    def __init__(self):
        """Инициализация пустой базы данных"""
        self.persons: Dict[int, Person] = {}
        self.positions: Dict[int, Position] = {}
        self.companies: Dict[int, Company] = {}
        
        # Счетчики для автоматической генерации ID
        self.next_person_id = 1
        self.next_position_id = 1
        self.next_company_id = 1
        
        print("Создана новая база данных")
    
    def add_position(self, title: str) -> int:
        """
        Добавление новой должности
        Возвращает ID созданной должности
        """
        position_id = self.next_position_id
        self.positions[position_id] = Position(position_id, title)
        self.next_position_id += 1
        print(f"Добавлена должность: {title} (ID: {position_id})")
        return position_id
    
    def add_company(self, name: str) -> int:
        """
        Добавление новой фирмы
        Возвращает ID созданной фирмы
        """
        company_id = self.next_company_id
        self.companies[company_id] = Company(company_id, name)
        self.next_company_id += 1
        print(f"Добавлена фирма: {name} (ID: {company_id})")
        return company_id
    
    def add_person(self, full_name: str, position_title: str, company_name: str) -> int:
        """
        Добавление нового человека
        Автоматически создает должность и фирму, если их нет
        Возвращает ID созданного человека
        """
        # Поиск или создание должности
        position_id = self._find_or_create_position(position_title)
        
        # Поиск или создание фирмы
        company_id = self._find_or_create_company(company_name)
        
        # Создание человека
        person_id = self.next_person_id
        self.persons[person_id] = Person(person_id, full_name, position_id, company_id)
        self.next_person_id += 1
        print(f"Добавлен человек: {full_name} (ID: {person_id})")
        return person_id
    
    def _find_or_create_position(self, title: str) -> int:
        """
        Поиск ID должности по названию или создание новой
        """
        for pos_id, pos in self.positions.items():
            if pos.title.lower() == title.lower():
                return pos_id
        return self.add_position(title)
    
    def _find_or_create_company(self, name: str) -> int:
        """
        Поиск ID фирмы по названию или создание новой
        """
        for comp_id, comp in self.companies.items():
            if comp.name.lower() == name.lower():
                return comp_id
        return self.add_company(name)
    
    def delete_person(self, person_id: int) -> bool:
        """
        Удаление человека по ID
        """
        if person_id in self.persons:
            person = self.persons[person_id]
            del self.persons[person_id]
            print(f"Удален человек: {person.full_name} (ID: {person_id})")
            return True
        print(f"Человек с ID {person_id} не найден")
        return False
    
    def delete_company(self, company_id: int) -> bool:
        """
        Удаление фирмы по ID
        При удалении фирмы удаляются все сотрудники этой фирмы
        """
        if company_id in self.companies:
            company = self.companies[company_id]
            
            # Удаление всех сотрудников этой фирмы
            persons_to_delete = [p_id for p_id, p in self.persons.items() 
                               if p.company_id == company_id]
            for p_id in persons_to_delete:
                self.delete_person(p_id)
            
            # Удаление фирмы
            del self.companies[company_id]
            print(f"Удалена фирма: {company.name} (ID: {company_id})")
            return True
        print(f"Фирма с ID {company_id} не найдена")
        return False
    
    def delete_position(self, position_id: int) -> bool:
        """
        Удаление должности по ID
        При удалении должности удаляются все сотрудники с этой должностью
        """
        if position_id in self.positions:
            position = self.positions[position_id]
            
            # Удаление всех сотрудников с этой должностью
            persons_to_delete = [p_id for p_id, p in self.persons.items() 
                               if p.position_id == position_id]
            for p_id in persons_to_delete:
                self.delete_person(p_id)
            
            # Удаление должности
            del self.positions[position_id]
            print(f"Удалена должность: {position.title} (ID: {position_id})")
            return True
        print(f"Должность с ID {position_id} не найдена")
        return False
    
    def update_person(self, person_id: int, full_name: Optional[str] = None,
                     position_title: Optional[str] = None, 
                     company_name: Optional[str] = None) -> bool:
        """
        Обновление данных человека
        """
        if person_id not in self.persons:
            print(f"Человек с ID {person_id} не найден")
            return False
        
        person = self.persons[person_id]
        
        if full_name:
            person.full_name = full_name
        
        if position_title:
            person.position_id = self._find_or_create_position(position_title)
        
        if company_name:
            person.company_id = self._find_or_create_company(company_name)
        
        print(f"Обновлены данные человека с ID {person_id}")
        return True
    
    def get_person_info(self) -> List[Dict]:
        """
        Задание 1: Для каждого человека: «ФИО персоны», его должность, в какой фирме работает
        """
        result = []
        for person in self.persons.values():
            position = self.positions.get(person.position_id)
            company = self.companies.get(person.company_id)
            
            result.append({
                'full_name': person.full_name,
                'position': position.title if position else 'Неизвестно',
                'company': company.name if company else 'Неизвестно'
            })
        
        return result
    
    def get_company_employee_count(self) -> List[Dict]:
        """
        Задание 2: Для каждой фирмы: сколько в ней сотрудников
        """
        # Подсчет сотрудников по фирмам
        company_counts = {}
        for person in self.persons.values():
            company_id = person.company_id
            company_counts[company_id] = company_counts.get(company_id, 0) + 1
        
        # Формирование результата
        result = []
        for company in self.companies.values():
            result.append({
                'company_name': company.name,
                'employee_count': company_counts.get(company.id, 0)
            })
        
        # Сортировка по убыванию количества сотрудников
        result.sort(key=lambda x: x['employee_count'], reverse=True)
        
        return result
    
    def print_person_info(self):
        """
        Вывод в консоль информации о каждом человеке
        """
        print("\n" + "=" * 80)
        print("ЗАДАНИЕ 1: Информация о каждом человеке")
        print("=" * 80)
        
        persons_info = self.get_person_info()
        if not persons_info:
            print("Нет данных о людях")
            return
        
        print(f"{'ФИО':<30} | {'Должность':<25} | {'Фирма':<20}")
        print("-" * 80)
        
        for info in persons_info:
            print(f"{info['full_name']:<30} | {info['position']:<25} | {info['company']:<20}")
    
    def print_company_employee_count(self):
        """
        Вывод в консоль количества сотрудников в каждой фирме
        """
        print("\n" + "=" * 80)
        print("ЗАДАНИЕ 2: Количество сотрудников в каждой фирме")
        print("=" * 80)
        
        companies_info = self.get_company_employee_count()
        if not companies_info:
            print("Нет данных о фирмах")
            return
        
        print(f"{'Фирма':<40} | {'Количество сотрудников':<20}")
        print("-" * 80)
        
        for info in companies_info:
            print(f"{info['company_name']:<40} | {info['employee_count']:<20}")
    
    def save_to_csv(self, filename: str = "database.csv"):
        """
        Сохранение данных в CSV файлы
        """
        # Сохранение людей
        with open(f"persons_{filename}", 'w', newline='', encoding='utf-8') as f:
            writer = csv.writer(f)
            writer.writerow(['ID', 'ФИО', 'ID_должности', 'ID_фирмы'])
            for person in self.persons.values():
                writer.writerow([person.id, person.full_name, 
                               person.position_id, person.company_id])
        
        # Сохранение должностей
        with open(f"positions_{filename}", 'w', newline='', encoding='utf-8') as f:
            writer = csv.writer(f)
            writer.writerow(['ID', 'Название'])
            for position in self.positions.values():
                writer.writerow([position.id, position.title])
        
        # Сохранение фирм
        with open(f"companies_{filename}", 'w', newline='', encoding='utf-8') as f:
            writer = csv.writer(f)
            writer.writerow(['ID', 'Название'])
            for company in self.companies.values():
                writer.writerow([company.id, company.name])
        
        print(f"\nДанные сохранены в файлы: persons_{filename}, positions_{filename}, companies_{filename}")
    
    def load_from_csv(self, filename: str = "database.csv"):
        """
        Загрузка данных из CSV файлов
        """
        try:
            # Загрузка должностей
            self.positions.clear()
            with open(f"positions_{filename}", 'r', encoding='utf-8') as f:
                reader = csv.reader(f)
                next(reader)  # Пропуск заголовка
                for row in reader:
                    if row:
                        pos_id = int(row[0])
                        self.positions[pos_id] = Position(pos_id, row[1])
                        if pos_id >= self.next_position_id:
                            self.next_position_id = pos_id + 1
            
            # Загрузка фирм
            self.companies.clear()
            with open(f"companies_{filename}", 'r', encoding='utf-8') as f:
                reader = csv.reader(f)
                next(reader)
                for row in reader:
                    if row:
                        comp_id = int(row[0])
                        self.companies[comp_id] = Company(comp_id, row[1])
                        if comp_id >= self.next_company_id:
                            self.next_company_id = comp_id + 1
            
            # Загрузка людей
            self.persons.clear()
            with open(f"persons_{filename}", 'r', encoding='utf-8') as f:
                reader = csv.reader(f)
                next(reader)
                for row in reader:
                    if row:
                        p_id = int(row[0])
                        self.persons[p_id] = Person(p_id, row[1], int(row[2]), int(row[3]))
                        if p_id >= self.next_person_id:
                            self.next_person_id = p_id + 1
            
            print(f"\nДанные загружены из файлов: persons_{filename}, positions_{filename}, companies_{filename}")
            return True
            
        except FileNotFoundError:
            print(f"Файлы не найдены. Загрузка невозможна.")
            return False
        except Exception as e:
            print(f"Ошибка при загрузке данных: {e}")
            return False


def main():
    """
    Основная функция для демонстрации работы с базой данных
    """
    print("=" * 80)
    print("ЛАБОРАТОРНАЯ РАБОТА №4. РАБОТА С БАЗАМИ ДАННЫХ")
    print("Вариант 6: Человек - Фирма - Должность")
    print("=" * 80)
    
    # Создание базы данных
    db = Database()
    
    # Заполнение базы данных тестовыми данными
    print("\n--- ЗАПОЛНЕНИЕ БАЗЫ ДАННЫХ ТЕСТОВЫМИ ДАННЫМИ ---")
    
    # Добавление должностей
    db.add_position("Директор")
    db.add_position("Менеджер")
    db.add_position("Разработчик")
    db.add_position("Бухгалтер")
    
    # Добавление фирм
    db.add_company("ООО ТехноПром")
    db.add_company("АО ИнвестГрупп")
    db.add_company("ИП Иванов")
    
    # Добавление людей
    db.add_person("Иванов Иван Иванович", "Директор", "ООО ТехноПром")
    db.add_person("Петров Петр Петрович", "Разработчик", "ООО ТехноПром")
    db.add_person("Сидорова Анна Сергеевна", "Бухгалтер", "ООО ТехноПром")
    db.add_person("Смирнов Алексей Владимирович", "Менеджер", "АО ИнвестГрупп")
    db.add_person("Кузнецова Елена Дмитриевна", "Разработчик", "АО ИнвестГрупп")
    db.add_person("Васильев Дмитрий Николаевич", "Разработчик", "АО ИнвестГрупп")
    db.add_person("Михайлова Ольга Павловна", "Бухгалтер", "ИП Иванов")
    
    # Вывод информации по заданию 1
    db.print_person_info()
    
    # Вывод информации по заданию 2
    db.print_company_employee_count()
    
    # Демонстрация добавления данных
    print("\n" + "=" * 80)
    print("ДЕМОНСТРАЦИЯ ДОБАВЛЕНИЯ ДАННЫХ")
    print("=" * 80)
    
    db.add_person("Новиков Константин Андреевич", "Менеджер", "ООО ТехноПром")
    db.print_company_employee_count()
    
    # Демонстрация изменения данных
    print("\n" + "=" * 80)
    print("ДЕМОНСТРАЦИЯ ИЗМЕНЕНИЯ ДАННЫХ")
    print("=" * 80)
    
    # Поиск ID человека для изменения
    person_to_update = None
    for p_id, person in db.persons.items():
        if "Иванов" in person.full_name:
            person_to_update = p_id
            break
    
    if person_to_update:
        db.update_person(person_to_update, 
                        company_name="АО ИнвестГрупп",
                        position_title="Руководитель отдела")
        db.print_person_info()
    
    # Демонстрация удаления данных
    print("\n" + "=" * 80)
    print("ДЕМОНСТРАЦИЯ УДАЛЕНИЯ ДАННЫХ")
    print("=" * 80)
    
    # Удаление одного человека
    person_to_delete = None
    for p_id, person in db.persons.items():
        if "Сидорова" in person.full_name:
            person_to_delete = p_id
            break
    
    if person_to_delete:
        db.delete_person(person_to_delete)
        db.print_person_info()
    
    # Демонстрация каскадного удаления (удаление фирмы удаляет всех её сотрудников)
    print("\n--- Каскадное удаление: удаление фирмы ---")
    company_to_delete = None
    for c_id, company in db.companies.items():
        if "ИП Иванов" in company.name:
            company_to_delete = c_id
            break
    
    if company_to_delete:
        db.delete_company(company_to_delete)
        db.print_person_info()
        db.print_company_employee_count()
    
    # Демонстрация сохранения в CSV
    print("\n" + "=" * 80)
    print("СОХРАНЕНИЕ ДАННЫХ В CSV ФАЙЛЫ")
    print("=" * 80)
    
    db.save_to_csv("lab4_data.csv")
    
    # Демонстрация загрузки из CSV
    print("\n" + "=" * 80)
    print("ЗАГРУЗКА ДАННЫХ ИЗ CSV ФАЙЛОВ")
    print("=" * 80)
    
    db2 = Database()
    if db2.load_from_csv("lab4_data.csv"):
        db2.print_person_info()
        db2.print_company_employee_count()
    
    # Интерактивный режим
    print("\n" + "=" * 80)
    print("ИНТЕРАКТИВНЫЙ РЕЖИМ РАБОТЫ")
    print("=" * 80)
    print("Вы можете добавить свои данные")
    
    while True:
        print("\nМеню:")
        print("1. Добавить человека")
        print("2. Добавить фирму")
        print("3. Добавить должность")
        print("4. Показать всех людей")
        print("5. Показать статистику по фирмам")
        print("6. Сохранить в CSV")
        print("7. Выйти")
        
        choice = input("Выберите действие (1-7): ").strip()
        
        if choice == '1':
            name = input("Введите ФИО: ")
            position = input("Введите должность: ")
            company = input("Введите название фирмы: ")
            db.add_person(name, position, company)
        
        elif choice == '2':
            name = input("Введите название фирмы: ")
            db.add_company(name)
        
        elif choice == '3':
            title = input("Введите название должности: ")
            db.add_position(title)
        
        elif choice == '4':
            db.print_person_info()
        
        elif choice == '5':
            db.print_company_employee_count()
        
        elif choice == '6':
            filename = input("Введите имя файла (по умолчанию lab4_data.csv): ") or "lab4_data.csv"
            db.save_to_csv(filename)
        
        elif choice == '7':
            print("Выход из программы...")
            break
        
        else:
            print("Неверный выбор. Пожалуйста, выберите 1-7")


if __name__ == "__main__":
    main()
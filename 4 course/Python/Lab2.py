"""
Лабораторная работа №2. Создание типа данных «класс»
Вариант 6: Класс "Дата" (три числа): день, месяц, год
Методы:
1. Увеличить год на 1
2. Уменьшить дату на 2 дня
"""

class Date:
    """
    Класс для представления даты.
    Поля: день, месяц, год
    """
    
    def __init__(self, day=1, month=1, year=2000):
        """
        Конструктор по умолчанию.
        Если параметры не переданы, устанавливается дата 01.01.2000
        """
        self.day = day
        self.month = month
        self.year = year
        print(f"Создан объект Date: {self}")
    
    def __del__(self):
        """Деструктор для освобождения памяти"""
        print(f"Объект Date {self} уничтожен")
    
    def __str__(self):
        """Строковое представление даты"""
        return f"{self.day:02d}.{self.month:02d}.{self.year}"
    
    def get_info(self):
        """Функция формирования строки информации об объекте"""
        info = f"Дата: {self}\n"
        info += f"День: {self.day}\n"
        info += f"Месяц: {self.month}\n"
        info += f"Год: {self.year}"
        return info
    
    def increase_year_by_one(self):
        """
        Функция-метод 1: Увеличить год на 1
        """
        self.year += 1
        print(f"Год увеличен на 1. Новая дата: {self}")
    
    def decrease_by_two_days(self):
        """
        Функция-метод 2: Уменьшить дату на 2 дня
        Учитываются особенности разных месяцев и високосных годов
        """
        # Сохраняем исходную дату для вывода
        original = str(self)
        
        # Уменьшаем на 2 дня
        for _ in range(2):
            self._decrease_by_one_day()
        
        print(f"Дата уменьшена на 2 дня. Было: {original}, стало: {self}")
    
    def _decrease_by_one_day(self):
        """
        Вспомогательный метод для уменьшения даты на 1 день
        """
        self.day -= 1
        
        # Если день стал 0, переходим на предыдущий месяц
        if self.day == 0:
            self.month -= 1
            
            # Если месяц стал 0, переходим на декабрь предыдущего года
            if self.month == 0:
                self.month = 12
                self.year -= 1
            
            # Устанавливаем последний день предыдущего месяца
            self.day = self._get_days_in_month()
    
    def _get_days_in_month(self):
        """
        Возвращает количество дней в текущем месяце
        """
        # Месяцы с 31 днем
        if self.month in [1, 3, 5, 7, 8, 10, 12]:
            return 31
        # Месяцы с 30 днями
        elif self.month in [4, 6, 9, 11]:
            return 30
        # Февраль
        elif self.month == 2:
            # Проверка на високосный год
            if self._is_leap_year():
                return 29
            else:
                return 28
    
    def _is_leap_year(self):
        """
        Проверяет, является ли текущий год високосным
        """
        year = self.year
        return (year % 4 == 0 and year % 100 != 0) or (year % 400 == 0)


def main():
    """
    Основная функция для демонстрации работы класса
    """
    print("=" * 60)
    print("Демонстрация работы класса Date")
    print("=" * 60)
    
    # 1. Создание объекта со значениями-константами
    print("\n--- Объект 1: создание с константными значениями ---")
    date1 = Date(15, 3, 2023)  # 15 марта 2023
    print("\nИнформация об объекте:")
    print(date1.get_info())
    
    print("\n--- Демонстрация методов для объекта 1 ---")
    date1.increase_year_by_one()
    date1.decrease_by_two_days()
    
    # 2. Создание объекта со значениями по умолчанию
    print("\n--- Объект 2: создание с параметрами по умолчанию ---")
    date2 = Date()  # 01.01.2000
    print("\nИнформация об объекте:")
    print(date2.get_info())
    
    print("\n--- Демонстрация методов для объекта 2 ---")
    date2.decrease_by_two_days()
    date2.increase_year_by_one()
    
    # 3. Создание объекта с введенными с клавиатуры значениями
    print("\n--- Объект 3: создание с введенными с клавиатуры значениями ---")
    print("Введите дату (для демонстрации работы методов):")
    
    try:
        day = int(input("Введите день (1-31): "))
        month = int(input("Введите месяц (1-12): "))
        year = int(input("Введите год: "))
        
        date3 = Date(day, month, year)
        print("\nИнформация об объекте:")
        print(date3.get_info())
        
        print("\n--- Демонстрация методов для объекта 3 ---")
        date3.increase_year_by_one()
        date3.decrease_by_two_days()
        
    except ValueError:
        print("Ошибка: введены некорректные данные. Используем значения по умолчанию.")
        date3 = Date()
        print("\nИнформация об объекте (со значениями по умолчанию):")
        print(date3.get_info())
        
        print("\n--- Демонстрация методов для объекта 3 ---")
        date3.increase_year_by_one()
        date3.decrease_by_two_days()
    
    print("\n" + "=" * 60)
    print("Программа завершена. Объекты будут уничтожены.")
    print("=" * 60)


if __name__ == "__main__":
    main()
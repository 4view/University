"""
Лабораторная работа №3. Наследование и полиморфизм
Вариант 6: Геометрические фигуры (куб, цилиндр, тетраэдр)
Реализовать методы вычисления объема и площади поверхности фигуры.
"""

import math
from abc import ABC, abstractmethod


class GeometricFigure(ABC):
    """
    Базовый абстрактный класс для геометрических фигур.
    Содержит виртуальный метод calculate_volume(), который переопределяется в наследниках.
    """
    
    def __init__(self, name):
        """
        Конструктор базового класса
        """
        self.name = name
        print(f"Создан объект базового класса GeometricFigure: {self.name}")
    
    def __del__(self):
        """
        Деструктор для освобождения памяти
        """
        print(f"Объект {self.name} уничтожен")
    
    @abstractmethod
    def calculate_volume(self):
        """
        Виртуальный метод для вычисления объема.
        Должен быть переопределен в классах-наследниках.
        """
        pass
    
    @abstractmethod
    def calculate_surface_area(self):
        """
        Абстрактный метод для вычисления площади поверхности.
        Должен быть переопределен в классах-наследниках.
        """
        pass
    
    def get_info(self):
        """
        Метод для получения информации о фигуре
        """
        info = f"\nИнформация о фигуре: {self.name}\n"
        info += f"Объем: {self.calculate_volume():.2f}\n"
        info += f"Площадь поверхности: {self.calculate_surface_area():.2f}"
        return info


class Cube(GeometricFigure):
    """
    Класс Куб, наследник GeometricFigure
    """
    
    def __init__(self, side_length):
        """
        Конструктор класса Cube
        """
        super().__init__("Куб")
        self.side_length = side_length
        print(f"Сторона куба: {self.side_length}")
    
    def calculate_volume(self):
        """
        Переопределение метода вычисления объема для куба
        V = a³
        """
        return self.side_length ** 3
    
    def calculate_surface_area(self):
        """
        Переопределение метода вычисления площади поверхности для куба
        S = 6 * a²
        """
        return 6 * (self.side_length ** 2)
    
    def get_info(self):
        """
        Перегрузка метода родителя для добавления специфической информации
        """
        info = super().get_info()
        info += f"\nДлина стороны: {self.side_length}"
        return info


class Cylinder(GeometricFigure):
    """
    Класс Цилиндр, наследник GeometricFigure
    """
    
    def __init__(self, radius, height):
        """
        Конструктор класса Cylinder
        """
        super().__init__("Цилиндр")
        self.radius = radius
        self.height = height
        print(f"Радиус цилиндра: {self.radius}, высота: {self.height}")
    
    def calculate_volume(self):
        """
        Переопределение метода вычисления объема для цилиндра
        V = π * r² * h
        """
        return math.pi * (self.radius ** 2) * self.height
    
    def calculate_surface_area(self):
        """
        Переопределение метода вычисления площади поверхности для цилиндра
        S = 2πr² + 2πrh = 2πr(r + h)
        """
        return 2 * math.pi * self.radius * (self.radius + self.height)
    
    def get_info(self):
        """
        Перегрузка метода родителя
        """
        info = super().get_info()
        info += f"\nРадиус: {self.radius}, высота: {self.height}"
        return info


class Tetrahedron(GeometricFigure):
    """
    Класс Правильный тетраэдр, наследник GeometricFigure
    """
    
    def __init__(self, edge_length):
        """
        Конструктор класса Tetrahedron
        """
        super().__init__("Правильный тетраэдр")
        self.edge_length = edge_length
        print(f"Длина ребра тетраэдра: {self.edge_length}")
    
    def calculate_volume(self):
        """
        Переопределение метода вычисления объема для тетраэдра
        V = (a³ * √2) / 12
        """
        return (self.edge_length ** 3 * math.sqrt(2)) / 12
    
    def calculate_surface_area(self):
        """
        Переопределение метода вычисления площади поверхности для тетраэдра
        S = √3 * a²
        """
        return math.sqrt(3) * (self.edge_length ** 2)


def main():
    """
    Основная функция для демонстрации работы классов
    """
    print("=" * 60)
    print("Лабораторная работа №3. Наследование и полиморфизм")
    print("Вариант 6: Куб, Цилиндр, Тетраэдр")
    print("=" * 60)
    
    # Создание объектов с константными значениями
    print("\n--- Создание объектов с константными значениями ---")
    
    cube1 = Cube(5)
    cylinder1 = Cylinder(3, 7)
    tetrahedron1 = Tetrahedron(4)
    
    print("\n--- Демонстрация работы методов ---")
    print(cube1.get_info())
    print(cylinder1.get_info())
    print(tetrahedron1.get_info())
    
    # Создание объектов с введенными с клавиатуры значениями
    print("\n" + "=" * 60)
    print("Создание объектов с введенными с клавиатуры значениями")
    print("=" * 60)
    
    try:
        # Создание куба
        print("\n--- Ввод данных для куба ---")
        side = float(input("Введите длину стороны куба: "))
        cube2 = Cube(side)
        print(cube2.get_info())
        
        # Создание цилиндра
        print("\n--- Ввод данных для цилиндра ---")
        radius = float(input("Введите радиус цилиндра: "))
        height = float(input("Введите высоту цилиндра: "))
        cylinder2 = Cylinder(radius, height)
        print(cylinder2.get_info())
        
        # Создание тетраэдра
        print("\n--- Ввод данных для тетраэдра ---")
        edge = float(input("Введите длину ребра тетраэдра: "))
        tetrahedron2 = Tetrahedron(edge)
        print(tetrahedron2.get_info())
        
    except ValueError:
        print("Ошибка: введены некорректные данные!")
    
    print("\n" + "=" * 60)
    print("Демонстрация вызова конструктора родительского класса")
    print("=" * 60)
    print("При создании каждого объекта сначала вызывается")
    print("конструктор базового класса GeometricFigure, затем конструктор наследника.")
    
    print("\n" + "=" * 60)
    print("Программа завершена. Объекты будут уничтожены.")
    print("=" * 60)


if __name__ == "__main__":
    main()
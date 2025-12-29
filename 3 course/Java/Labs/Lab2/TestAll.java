import java.util.ArrayList;

/**
 * Базовый класс Работник
 * @author [Kochergin AA]
 * @version 1.0
 */
class Employee {

    private String name;        // Имя работника
    private String position;    // Должность работника

    /**
     * Конструктор по умолчанию
     * Создает работника с именем "Неизвестно" и должностью "Не указана"
     */
    public Employee() {
        this.name = "Неизвестно";
        this.position = "Не указана";
    }

    /**
     * Конструктор с параметрами
     * @param name Имя работника
     * @param position Должность работника
     */
    public Employee(String name, String position) {
        this.name = name;
        this.position = position;
    }

    /**
     * Получить имя работника
     * @return Имя работника
     */
    public String getName() {
        return name;
    }

    /**
     * Установить имя работника
     * @param name Новое имя работника
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Получить должность работника
     * @return Должность работника
     */
    public String getPosition() {
        return position;
    }

    /**
     * Установить должность работника
     * @param position Новая должность работника
     */
    public void setPosition(String position) {
        this.position = position;
    }

    /**
     * Преобразование объекта в строку
     * @return Строковое представление работника
     */
    @Override
    public String toString() {
        return "Имя: " + name + ", Должность: " + position;
    }
}

/**
 * Класс Работник на почасовой оплате
 * Наследуется от класса Employee
 * @author [Ваше Имя]
 * @version 1.0
 * @see Employee
 */
class HourlyEmployee extends Employee {

    private double hourlyRate;  // Почасовая ставка
    private int hoursWorked;    // Отработанные часы

    /**
     * Конструктор по умолчанию
     * Создает работника с нулевой ставкой и отработанными часами
     */
    public HourlyEmployee() {
        super();
        this.hourlyRate = 0.0;
        this.hoursWorked = 0;
    }

    /**
     * Конструктор с параметрами
     * @param name Имя работника
     * @param position Должность работника
     * @param hourlyRate Почасовая ставка
     * @param hoursWorked Отработанные часы
     */
    public HourlyEmployee(String name, String position, double hourlyRate, int hoursWorked) {
        super(name, position);
        this.hourlyRate = hourlyRate;
        this.hoursWorked = hoursWorked;
    }

    /**
     * Получить почасовую ставку
     * @return Почасовая ставка
     */
    public double getHourlyRate() {
        return hourlyRate;
    }

    /**
     * Установить почасовую ставку
     * @param hourlyRate Новая почасовая ставка
     */
    public void setHourlyRate(double hourlyRate) {
        this.hourlyRate = hourlyRate;
    }

    /**
     * Получить количество отработанных часов
     * @return Отработанные часы
     */
    public int getHoursWorked() {
        return hoursWorked;
    }

    /**
     * Установить количество отработанных часов
     * @param hoursWorked Новое количество отработанных часов
     */
    public void setHoursWorked(int hoursWorked) {
        this.hoursWorked = hoursWorked;
    }

    /**
     * Рассчитать зарплату работника
     * @return Зарплата (ставка * часы)
     */
    public double calculateSalary() {
        return hourlyRate * hoursWorked;
    }

    /**
     * Преобразование объекта в строку
     * @return Строковое представление работника с почасовой оплатой
     */
    @Override
    public String toString() {
        return super.toString() + ", Тип: Почасовая оплата, Ставка: " + hourlyRate
                + ", Отработано часов: " + hoursWorked + ", Зарплата: " + calculateSalary();
    }
}

/**
 * Класс Работник на окладе
 * Наследуется от класса Employee
 * @author [Ваше Имя]
 * @version 1.0
 * @see Employee
 */
class SalariedEmployee extends Employee {

    private double monthlySalary;  // Месячный оклад

    /**
     * Конструктор по умолчанию
     * Создает работника с нулевым окладом
     */
    public SalariedEmployee() {
        super();
        this.monthlySalary = 0.0;
    }

    /**
     * Конструктор с параметрами
     * @param name Имя работника
     * @param position Должность работника
     * @param monthlySalary Месячный оклад
     */
    public SalariedEmployee(String name, String position, double monthlySalary) {
        super(name, position);
        this.monthlySalary = monthlySalary;
    }

    /**
     * Получить месячный оклад
     * @return Месячный оклад
     */
    public double getMonthlySalary() {
        return monthlySalary;
    }

    /**
     * Установить месячный оклад
     * @param monthlySalary Новый месячный оклад
     */
    public void setMonthlySalary(double monthlySalary) {
        this.monthlySalary = monthlySalary;
    }

    /**
     * Преобразование объекта в строку
     * @return Строковое представление работника с окладом
     */
    @Override
    public String toString() {
        return super.toString() + ", Тип: Оклад, Зарплата в месяц: " + monthlySalary;
    }
}

/**
 * Класс Предприятие
 * Содержит список работников и методы для работы с ними
 * @author [Ваше Имя]
 * @version 1.0
 * @since 1.0
 */
class Company {

    private ArrayList<Employee> employees = new ArrayList<>();  // Список работников

    /**
     * Конструктор по умолчанию
     * Создает пустое предприятие
     */
    public Company() {
    }

    /**
     * Добавить работника на предприятие
     * @param emp Работник для добавления
     */
    public void addEmployee(Employee emp) {
        employees.add(emp);
    }

    /**
     * Подсчитать количество работников на почасовой оплате
     * Использует оператор instanceof для определения типа
     * @return Количество работников на почасовой оплате
     * @see HourlyEmployee
     */
    public int countHourlyEmployees() {
        int count = 0;
        for (Employee emp : employees) {
            if (emp instanceof HourlyEmployee) {
                count++;
            }
        }
        return count;
    }

    /**
     * Подсчитать количество работников на окладе
     * Использует оператор instanceof для определения типа
     * @return Количество работников на окладе
     * @see SalariedEmployee
     */
    public int countSalariedEmployees() {
        int count = 0;
        for (Employee emp : employees) {
            if (emp instanceof SalariedEmployee) {
                count++;
            }
        }
        return count;
    }

    /**
     * Вывести список всех работников предприятия
     */
    public void printEmployees() {
        System.out.println("Список работников:");
        for (Employee emp : employees) {
            System.out.println(emp.toString());
        }
    }

    /**
     * Получить список всех работников
     * @return Список работников
     */
    public ArrayList<Employee> getEmployees() {
        return employees;
    }
}

/**
 * Содержит метод main для запуска программы
 * @author [Ваше Имя]
 * @version 1.0
 */
public class TestAll {
    /**
     * Главный метод программы
     * Создает предприятие, добавляет работников разных типов,
     * выводит информацию и подсчитывает количество работников по типам
     * @param args Аргументы командной строки (не используются)
     */
    public static void main(String[] args) {
        // Создание предприятия
        Company company = new Company();

        // Создание работников на почасовой оплате
        HourlyEmployee emp1 = new HourlyEmployee("Иван Петров", "Разработчик", 25.5, 160);
        HourlyEmployee emp2 = new HourlyEmployee("Мария Сидорова", "Дизайнер", 20.0, 150);
        
        // Создание работников на окладе
        SalariedEmployee emp3 = new SalariedEmployee("Алексей Козлов", "Менеджер", 50000.0);
        SalariedEmployee emp4 = new SalariedEmployee("Ольга Новикова", "Бухгалтер", 45000.0);

        // Добавление работников на предприятие
        company.addEmployee(emp1);
        company.addEmployee(emp2);
        company.addEmployee(emp3);
        company.addEmployee(emp4);

        // Вывод всех работников
        company.printEmployees();

        // Подсчет и вывод количества работников по типам
        System.out.println("\nКоличество работников на почасовой оплате: " + company.countHourlyEmployees());
        System.out.println("Количество работников на окладе: " + company.countSalariedEmployees());
        
        // Дополнительная демонстрация работы instanceof
        System.out.println("\nДемонстрация работы instanceof:");
        for (Employee emp : company.getEmployees()) {
            if (emp instanceof HourlyEmployee) {
                System.out.println(emp.getName() + " - работник на почасовой оплате");
            } else if (emp instanceof SalariedEmployee) {
                System.out.println(emp.getName() + " - работник на окладе");
            }
        }
    }
}
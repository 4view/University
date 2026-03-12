package Lab3;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class Lab3_3 {
    public static void main(String[] args) {
        Company company = new Company();
        
        company.addEmployee(new HourlyEmployee("Иван Петров", "Разработчик", 25.5, 160));
        company.addEmployee(new HourlyEmployee("Мария Сидорова", "Дизайнер", 20.0, 150));
        company.addEmployee(new SalariedEmployee("Алексей Козлов", "Менеджер", 50000.0));
        company.addEmployee(new SalariedEmployee("Ольга Новикова", "Бухгалтер", 45000.0));
        company.addEmployee(new HourlyEmployee("Сергей Васильев", "Тестировщик", 18.5, 140));
        
        ArrayList<Employee> employees = company.getEmployees();
        
        System.out.println("\n1. Работники на почасовой оплате:");
        List<Employee> hourlyEmployees = employees.stream()
            .filter(e -> e instanceof HourlyEmployee)
            .toList();
        hourlyEmployees.forEach(e -> System.out.println(e.getName()));
        
        // Сортировка по имени
        System.out.println("\n2. Все работники, отсортированные по имени:");
        List<Employee> sortedByName = employees.stream()
            .sorted(Comparator.comparing(Employee::getName))
            .toList();
        sortedByName.forEach(e -> System.out.println(e.getName()));
        
        // Поиск работников с определенной должностью
        System.out.println("\n3. Поиск разработчиков:");
        List<Employee> developers = employees.stream()
            .filter(e -> e.getPosition().toLowerCase().contains("разработчик"))
            .toList();
        developers.forEach(e -> System.out.println(e.getName() + " - " + e.getPosition()));
        
        // Группировка по типу работника
        System.out.println("\n5. Группировка работников по типу:");
        long hourlyCount = employees.stream()
            .filter(e -> e instanceof HourlyEmployee)
            .count();
        long salariedCount = employees.stream()
            .filter(e -> e instanceof SalariedEmployee)
            .count();
        System.out.println("Почасовых: " + hourlyCount);
        System.out.println("На окладе: " + salariedCount);
    }
}

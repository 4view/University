package Lab3;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Scanner;

public class Lab3_2_8 {
    public static void main(String[] args) {
        Map<String, List<String>> teachers = new HashMap<>();
        
        teachers.put("Иванов И.И.", Arrays.asList("Math", "Fizika", "Informatika"));
        teachers.put("Петров П.П.", Arrays.asList("History", "Philosophy"));
        teachers.put("Сидорова С.С.", Arrays.asList("Informatika", "Programming", "DataBase"));
        teachers.put("Кузнецов К.К.", Arrays.asList("Fizika", "Chemistry"));

        teachers.forEach((k, v) -> System.out.println("Name: " + k + " Lessons: " + v));
        
        Scanner scanner = new Scanner(System.in);
        System.out.print("Введите предмет для поиска: ");
        String searchSubject = scanner.nextLine();
        
        List<String> foundTeachers = new ArrayList<>();
        
        for (Map.Entry<String, List<String>> entry : teachers.entrySet()) {
            if (entry.getValue().contains(searchSubject)) {
                foundTeachers.add(entry.getKey());
            }
        }
        
        if (!foundTeachers.isEmpty()) {
            System.out.println("Преподаватели, ведущие предмет '" + searchSubject + "':");
            for (String teacher : foundTeachers) {
                System.out.println("- " + teacher);
            }
        } 
        else {
            System.out.println("Преподавателей по предмету '" + searchSubject + "' не найдено");
        }
        
        scanner.close();
    }
}

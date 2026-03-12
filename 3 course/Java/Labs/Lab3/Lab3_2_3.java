package Lab3;

import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Scanner;

public class Lab3_2_3 {
    public static void main(String[] args) {

        Map<String, List<String>> groups = new HashMap<>();
        
        groups.put("ИВТ-3312", Arrays.asList("Ivanov", "Petrov", "Sidorov", "Alekseev"));
        groups.put("ИВТ-3302", Arrays.asList("Antonov", "Borisov", "Vasiliev"));
        groups.put("ИВТ-3304", Arrays.asList("Andreev", "Alexandrov", "Artemiev"));
        groups.put("ИВТ-3310", Arrays.asList("Bogdanov", "Vladimirov", "Grigoriev"));
        
        groups.forEach((k, v) -> System.out.println("Group: " + k + " Name: " + v));

        Scanner scanner = new Scanner(System.in);
        System.out.print("Введите букву для поиска: ");
        char searchLetter = scanner.nextLine().toUpperCase().charAt(0);
        
        String maxGroup = null;
        int maxCount = 0;
        
        for (Map.Entry<String, List<String>> entry : groups.entrySet()) {
            int count = 0;
            for (String student : entry.getValue()) {
                if (student.toUpperCase().charAt(0) == searchLetter) {
                    count++;
                }
            }
            
            if (count > maxCount) {
                maxCount = count;
                maxGroup = entry.getKey();
            }
        }
        
        if (maxGroup != null) 
        {
            System.out.println("Группа с наибольшим количеством студентов на букву '" 
                + searchLetter + "': " + maxGroup);
            System.out.println("Количество студентов: " + maxCount);
        } 
        else 
        {
            System.out.println("Студентов на букву '" + searchLetter + "' не найдено");
        }
        
        scanner.close();
    }
}

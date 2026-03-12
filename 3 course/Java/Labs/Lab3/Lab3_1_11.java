package Lab3;

import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;

public class Lab3_1_11 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        
        System.out.print("Введите строку: ");
        String input = scanner.nextLine();
        
        Set<Character> upperCaseLetters = new HashSet<>();

        for (char c : input.toCharArray()) {
            if (Character.isUpperCase(c) && Character.isLetter(c)) {
                // Проверяем, что это латинская буква (A-Z)
                if (c >= 'A' && c <= 'Z') {
                    upperCaseLetters.add(c);
                }
            }
        }
        
        System.out.println("Заглавные латинские буквы во входной строке:");
        for (char c : upperCaseLetters) {
            System.out.print(c + " ");
        }
        
        scanner.close();
    }
}

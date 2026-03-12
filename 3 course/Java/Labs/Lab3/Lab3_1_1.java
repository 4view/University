package Lab3;
import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;

public class Lab3_1_1 {
    public static void main(String[] args) {
        // Создаем множество требуемых букв
        Set<Character> requiredSet = new HashSet<>();
        
        // Добавляем буквы от 'a' до 'f'
        for (char c = 'a'; c <= 'f'; c++) {
            requiredSet.add(c);
        }
        
        // Добавляем буквы от 'x' до 'z'
        for (char c = 'x'; c <= 'z'; c++) {
            requiredSet.add(c);
        }
        
        Scanner scanner = new Scanner(System.in);
        System.out.print("Введите последовательность символов: ");
        String input = scanner.nextLine().toLowerCase();
        
        System.out.println("\nБуквы из введенной последовательности, которые входят в заданное множество:");
        for (char c : input.toCharArray()) {
            if (requiredSet.contains(c)) {
                System.out.print(c + " ");
            }
        }
        
        scanner.close();
    }
}
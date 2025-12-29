
import java.util.HashMap;
import java.util.Map;
import java.util.Random;
import java.util.Scanner;

// 18. В массиве целых чисел найти наиболее часто встречающееся число. Если таких чисел несколько, то определить наименьшее из них.

public class Lab1_1 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Random random = new Random();
        Map<Integer, Integer> DigitCount = new HashMap<Integer, Integer>();
        int arrLength = 0;

        System.out.print("Введите размерность массива: ");
        while (true) {
            if (scanner.hasNextInt()) {
                arrLength = scanner.nextInt();
                if (arrLength > 0)
                    break;
                else
                    System.out.println("Ошибка! Массив должен иметь положительную размерность");
            } else {
                System.out.println("Ошибка! Вы ввели не число");
                scanner.next();
            }
        }
        scanner.close();

        int[] arr = new int[arrLength];

        for (int i = 0; i < arr.length; i++) {
            arr[i] = random.nextInt(10);
        }

        System.out.println("\nСгенерированный массив:");
        for (int i = 0; i < arr.length; i++) {
            System.out.print(arr[i] + " ");
            if ((i + 1) % 10 == 0)
                System.out.println();
        }

        System.out.println();

        for (int number : arr) {
            DigitCount.put(number, DigitCount.getOrDefault(number, 0) + 1);
        }

        int maxCount = 0;
        int mostFrequentNumber = 0;

        for (Map.Entry<Integer, Integer> entry : DigitCount.entrySet()) {
            int currentNumber = entry.getKey();
            int currentCount = entry.getValue();

            if (currentCount > maxCount) {
                maxCount = currentCount;
                mostFrequentNumber = currentNumber;
            } else if (currentCount == maxCount) {
                if (currentNumber < mostFrequentNumber) {
                    mostFrequentNumber = currentNumber;
                }
            }
        }

        System.out.println("Число, которое повторяется чаще всего: " + mostFrequentNumber);
        System.out.println("Количество повторений: " + maxCount);

    }
}

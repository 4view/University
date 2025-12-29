
import java.util.Random;
import java.util.Scanner;

// 19. Дан целочисленный массив с количеством элементов n. Сжать массив, выбросив из него каждый второй элемент

public class Lab1_2 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Random random = new Random();
        int arrLength = 0;

        System.out.print("Введите размерность массива: ");
        while (true) {
            if (scanner.hasNextInt())
            {
                arrLength = scanner.nextInt();
                if (arrLength > 0)
                    break;
                else
                    System.out.println("Ошибка! Массив должен иметь положительную размерность");                    
            }
            else
            {
                System.out.println("Ошибка! Вы ввели не число");
                scanner.next();
            }
        }
        scanner.close();

        int[] arr = new int[arrLength];

        for (int i = 0; i < arr.length; i++)
        {
            arr[i] = random.nextInt(201) - 100;
        }

        for (int item : arr) {
            System.out.print(item + " ");
        }

        System.out.println("\nОтформатированный массив: ");
        for (int i = 0; i < arr.length; i++)
        {
            if (i % 2 == 0)
            {
                System.out.print(arr[i] + " ");
            }
        }

    }
}

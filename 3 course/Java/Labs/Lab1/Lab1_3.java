
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;
import java.util.Scanner;

// 18. Дан двумерный массив. Выяснить, есть ли строки с одинаковой суммой элементов. Если есть, вывести их номера.

public class Lab1_3 {
   public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Random random = new Random();
        
        int arrRowLength = 0;
        int arrColLength = 0;

        while (true) { 
            System.out.print("Введите кол-во строк: ");
            
            if (scanner.hasNextInt()) {
                arrRowLength = scanner.nextInt();
                if (arrRowLength > 0) {
                    break;
                } else {
                    System.out.println("Количество строк должно быть положительным!");
                }
            } else {
                System.out.println("Ошибка! Вы ввели не число");
                scanner.next();
            }
        }

        while (true) { 
            System.out.print("Введите кол-во столбцов: ");
            
            if (scanner.hasNextInt()) {
                arrColLength = scanner.nextInt();
                if (arrColLength > 0) {
                    break;
                } else {
                    System.out.println("Количество столбцов должно быть положительным!");
                }
            } else {
                System.out.println("Ошибка! Вы ввели не число");
                scanner.next(); 
            }
        }

        scanner.close();

        int[][] array = new int[arrRowLength][arrColLength];
        Map<Integer, Integer> RowsSum = new HashMap<Integer, Integer>();
        
        for (int i = 0; i < arrRowLength; i++)
        {
            System.out.print(i + " | ");
            int sum = 0;
            
            for (int j = 0; j < arrColLength; j++)
            {
                array[i][j] = random.nextInt(5);
                sum += array[i][j];
                System.out.printf("%4d", array[i][j]);
            }
            RowsSum.put(i, sum);

            System.out.println("  | Сумма: " + sum);
        }
        
        Map<Integer, List<Integer>> sumToRows = new HashMap<>();

        for (Map.Entry<Integer, Integer> entry : RowsSum.entrySet())
        {
            int row = entry.getKey();
            int sum = entry.getValue();

            sumToRows.computeIfAbsent(sum, k -> new ArrayList<Integer>()).add(row);
        }
        
        System.out.println("Строки с одинаковой суммой: ");
        for (Map.Entry<Integer, List<Integer>> entry : sumToRows.entrySet())
        {
            List<Integer> rows = entry.getValue();
            if (rows.size() > 1)
            {
                System.out.println("Сумма " + entry.getKey() + " встречается в строках: " + rows);
            }
        }
   } 
}

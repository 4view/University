
import java.util.*;
//Дан двумерный массив. Выяснить, есть ли столбцы с одинаковой суммой элементов. Если есть, вывести их номера.
public class Lab1_4 {

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

        // Заполняем и выводим массив
        for (int i = 0; i < arrRowLength; i++) {
            for (int j = 0; j < arrColLength; j++) {
                array[i][j] = random.nextInt(21) - 10; // от -10 до 10
                System.out.printf("%4d", array[i][j]);
            }
            System.out.println();
        }

        findColumnsWithSameSum(array);
    }

    public static void findColumnsWithSameSum(int[][] array) {
        if (array == null || array.length == 0) {
            System.out.println("Массив пустой!");
            return;
        }

        int rows = array.length;
        int cols = array.length;

        Map<Integer, Integer> columnsSum = new HashMap<>();

        System.out.println("\nСуммы столбцов:");

        for (int j = 0; j < cols; j++) {
            int sum = 0;
            for (int i = 0; i < rows; i++) {
                sum += array[i][j];
            }
            columnsSum.put(j, sum);
            System.out.println("Столбец " + j + ": сумма = " + sum);
        }

        Map<Integer, List<Integer>> sumToColumns = new HashMap<>();

        for (Map.Entry<Integer, Integer> entry : columnsSum.entrySet()) {
            int column = entry.getKey();
            int sum = entry.getValue();

            sumToColumns.computeIfAbsent(sum, k -> new ArrayList<Integer>()).add(column);
        }

        System.out.println("\nАнализ одинаковых сумм столбцов:");
        boolean foundSameSums = false;

        for (Map.Entry<Integer, List<Integer>> entry : sumToColumns.entrySet()) {
            int sum = entry.getKey();
            List<Integer> columns = entry.getValue();

            if (columns.size() > 1) {
                foundSameSums = true;
                System.out.println("Сумма " + sum + " встречается в столбцах: " + columns);

                System.out.println("  Количество столбцов с этой суммой: " + columns.size());

                System.out.print("  Значения этих столбцов:\n");
                for (int col : columns) {
                    System.out.print("    Столбец " + col + ": [");
                    for (int i = 0; i < rows; i++) {
                        System.out.print(array[i][col]);
                        if (i < rows - 1) {
                            System.out.print(", ");
                        }
                    }
                    System.out.println("]");
                }
            }
        }

        if (!foundSameSums) {
            System.out.println("Нет столбцов с одинаковыми суммами.");
        }
    }
}

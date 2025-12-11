namespace Lab8;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №8 ===");
        Console.WriteLine("=== Обработка исключений ===\n");

        try
        {
            // 2. Программа с перехватом исключений int и string
            Console.WriteLine("1. ПЕРЕХВАТ ИСКЛЮЧЕНИЙ int И string:");
            Console.WriteLine("-----------------------------------");
            TestIntStringExceptions();

            // 3. Добавление перехвата любых исключений catch(...)
            Console.WriteLine("\n\n2. ПЕРЕХВАТ ЛЮБЫХ ИСКЛЮЧЕНИЙ:");
            Console.WriteLine("-----------------------------");
            TestCatchAllExceptions();

            // 4. Перехват стандартных исключений
            Console.WriteLine("\n\n3. ПЕРЕХВАТ СТАНДАРТНЫХ ИСКЛЮЧЕНИЙ:");
            Console.WriteLine("-----------------------------------");
            TestStandardExceptions();

            // 5. Собственные классы исключений
            Console.WriteLine("\n\n4. СОБСТВЕННЫЕ КЛАССЫ ИСКЛЮЧЕНИЙ:");
            Console.WriteLine("--------------------------------");
            TestCustomExceptions();

            // 6. Исключения при вложенных вызовах методов
            Console.WriteLine("\n\n5. ВЛОЖЕННЫЕ ВЫЗОВЫ МЕТОДОВ:");
            Console.WriteLine("----------------------------");
            TestNestedMethodCalls();

            // 7. Локальная обработка исключений
            Console.WriteLine("\n\n6. ЛОКАЛЬНАЯ ОБРАБОТКА ИСКЛЮЧЕНИЙ:");
            Console.WriteLine("---------------------------------");
            TestLocalExceptionHandling();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nНеобработанное исключение в Main: {ex.GetType().Name}");
            Console.WriteLine($"Сообщение: {ex.Message}");
        }
    }

    // Метод с исключениями типа int и string
    static void TestIntStringExceptions()
    {
        try
        {
            Console.Write(
                "Введите число для обработки (0 для int-исключения, 'error' для string-исключения): "
            );
            string input = Console.ReadLine();

            if (input == "0")
            {
                // Генерация исключения с кодом ошибки
                throw new Exception("404"); // Используем строку "404" в сообщении
            }

            if (input == "error")
            {
                // Генерация исключения с сообщением
                throw new Exception("Произошла ошибка ввода данных!");
            }

            Console.WriteLine($"Введено: {input} - корректное значение");
        }
        catch (Exception ex)
        {
            if (ex.Message == "404")
            {
                Console.WriteLine($" Перехвачено исключение с кодом ошибки: {ex.Message}");
                Console.WriteLine($"Сообщение: Обнаружена ошибка с кодом {ex.Message}");
            }
            else if (ex.Message == "Произошла ошибка ввода данных!")
            {
                Console.WriteLine($" Перехвачено исключение: {ex.Message}");
            }
            else
            {
                Console.WriteLine($" Перехвачено исключение: {ex.Message}");
            }
        }
    }

    // Метод с перехватом любых исключений (catch...)
    static void TestCatchAllExceptions()
    {
        try
        {
            Console.Write("Введите команду ('throw' для исключения, 'null' для NullReference): ");
            string command = Console.ReadLine();

            if (command == "throw")
            {
                throw new InvalidOperationException("Тестовое исключение из TestCatchAll");
            }
            else if (command == "null")
            {
                Person nullPerson = null;
                Console.WriteLine(nullPerson.Name); // NullReferenceException
            }
            else if (command == "format")
            {
                string invalidNumber = "abc123";
                int num = int.Parse(invalidNumber); // FormatException
            }
            else
            {
                Console.WriteLine("Команда не распознана, исключений не было");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Перехвачено любое исключение: {ex.GetType().Name}");
            Console.WriteLine($"Сообщение: {ex.Message}");
        }
    }

    // Метод со стандартными исключениями
    static void TestStandardExceptions()
    {
        try
        {
            // Создаем небольшую коллекцию для тестирования
            MyCollection<int> testCollection = new MyCollection<int>(3);

            Console.WriteLine("\nТестирование стандартных исключений:");

            // ArgumentException
            Console.WriteLine("\n1. ArgumentException:");
            Console.Write("Введите размер коллекции (0 или отрицательное для исключения): ");
            string sizeInput = Console.ReadLine();

            if (int.TryParse(sizeInput, out int size) && size <= 0)
            {
                throw new ArgumentException(
                    "Размер коллекции должен быть положительным",
                    nameof(size)
                );
            }

            // IndexOutOfRangeException
            Console.WriteLine("\n2. IndexOutOfRangeException:");
            int[] array = { 10, 20, 30 };
            Console.Write($"Введите индекс для массива [0-2] (10 для ошибки): ");
            string idxInput = Console.ReadLine();

            if (int.TryParse(idxInput, out int index))
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException(
                        $"Индекс {index} вне диапазона [0, {array.Length - 1}]"
                    );
                }
                Console.WriteLine($"array[{index}] = {array[index]}");
            }

            // InvalidOperationException
            Console.WriteLine("\n3. InvalidOperationException:");
            MyCollection<int> emptyCollection = new MyCollection<int>(2);
            Console.Write("Попытаться получить Min() из пустой коллекции? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                // Вызовет InvalidOperationException
                int min = emptyCollection.Min();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ArgumentException: {ex.Message}");
            Console.WriteLine($"Параметр: {ex.ParamName}");
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"IndexOutOfRangeException: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"InvalidOperationException: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"FormatException: {ex.Message}");
        }
    }

    // Метод с собственными классами исключений
    static void TestCustomExceptions()
    {
        try
        {
            Console.WriteLine("\nТестирование собственных исключений:");

            // Создание Encyclopedia с валидацией
            Console.Write("Введите название энциклопедии: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new EncyclopediaValidationException(
                    "Название энциклопедии не может быть пустым",
                    "Title",
                    title
                );
            }

            if (title.Length < 3)
            {
                throw new EncyclopediaValidationException(
                    "Название энциклопедии слишком короткое (минимум 3 символа)",
                    "Title",
                    title
                );
            }

            // Проверка года издания
            Console.Write("Введите год издания энциклопедии: ");
            string yearInput = Console.ReadLine();

            if (!int.TryParse(yearInput, out int year))
            {
                throw new EncyclopediaFormatException(
                    "Неверный формат года издания",
                    yearInput,
                    "YYYY или dd.MM.YYYY"
                );
            }

            if (year < 1500 || year > DateTime.Now.Year)
            {
                throw new EncyclopediaValidationException(
                    $"Год издания {year} вне допустимого диапазона (1500-{DateTime.Now.Year})",
                    "PublicationYear",
                    yearInput
                );
            }

            // Создание энциклопедии
            var enc = new Encyclopedia(title, year.ToString());
            Console.WriteLine($"Создана энциклопедия: {enc}");

            // Добавление персонажей с проверкой
            Console.Write("\nСколько персонажей добавить? ");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"\n--- Персонаж {i + 1} ---");
                    Console.Write("Имя: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        throw new PersonDataException(
                            "Имя персонажа обязательно",
                            nameof(Person.Name)
                        );
                    }

                    var person = new Person(name, "Не указано", "Нет описания");
                    enc.AddPerson(person);
                }

                Console.WriteLine($"Успешно добавлено {count} персонажей");
                enc.PrintAll();
            }
        }
        catch (EncyclopediaValidationException ex)
        {
            Console.WriteLine($"\nEncyclopediaValidationException:");
            Console.WriteLine($"Сообщение: {ex.Message}");
            Console.WriteLine($"Поле: {ex.FieldName}");
            Console.WriteLine($"Значение: '{ex.InvalidValue}'");
        }
        catch (EncyclopediaFormatException ex)
        {
            Console.WriteLine($"\nEncyclopediaFormatException:");
            Console.WriteLine($"Сообщение: {ex.Message}");
            Console.WriteLine($"Некорректное значение: '{ex.InvalidValue}'");
            Console.WriteLine($"Ожидаемый формат: {ex.ExpectedFormat}");
        }
        catch (PersonDataException ex)
        {
            Console.WriteLine($"\nPersonDataException:");
            Console.WriteLine($"Сообщение: {ex.Message}");
            Console.WriteLine($"Поле: {ex.FieldName}");
        }
    }

    // Исключения при вложенных вызовах методов
    static void TestNestedMethodCalls()
    {
        try
        {
            Console.WriteLine("\nТестирование вложенных вызовов:");
            Console.WriteLine("Вызывается Method1() -> Method2() -> Method3()");

            Method1();
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"\nИсключение перехвачено на верхнем уровне (в TestNestedMethodCalls):"
            );
            Console.WriteLine($"Тип: {ex.GetType().Name}");
            Console.WriteLine($"Сообщение: {ex.Message}");

            // Проверка внутреннего исключения
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
            }
        }
    }

    static void Method1()
    {
        Console.WriteLine("Выполняется Method1()");
        Method2();
    }

    static void Method2()
    {
        Console.WriteLine("Выполняется Method2()");
        Method3();
    }

    static void Method3()
    {
        Console.WriteLine("Выполняется Method3()");

        Console.Write("Введите тип исключения (1-Format, 2-Index, 3-Custom): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // FormatException
                Console.Write("Введите нечисловую строку для преобразования: ");
                string invalidNumber = Console.ReadLine();
                int num = int.Parse(invalidNumber);
                break;

            case "2":
                // IndexOutOfRangeException
                var smallArray = new int[3];
                Console.WriteLine($"Попытка доступа к элементу smallArray[10]");
                int value = smallArray[10];
                break;

            case "3":
                // Наше собственное исключение
                throw new EncyclopediaValidationException(
                    "Исключение сгенерировано в Method3",
                    "TestField",
                    "TestValue"
                );

            default:
                Console.WriteLine("Исключений не было");
                break;
        }
    }

    // Локальная обработка исключений (без передачи выше)
    static void TestLocalExceptionHandling()
    {
        Console.WriteLine("\nЛокальная обработка исключений:");

        // Пример 1: Обработка в цикле с продолжением работы
        ProcessMultiplePersons();

        // Пример 2: Обработка с восстановлением
        ProcessEncyclopediaWithRecovery();

        // Пример 3: Использование try-catch-finally для ресурсов
        UseCollectionWithCleanup();
    }

    static void ProcessMultiplePersons()
    {
        Console.WriteLine("\n1. Обработка нескольких персонажей:");

        var persons = new List<Person>();
        int successCount = 0;
        int errorCount = 0;

        for (int i = 0; i < 3; i++)
        {
            try
            {
                Console.WriteLine($"\n--- Персонаж {i + 1} ---");
                Console.Write("Имя: ");
                string name = Console.ReadLine();

                Console.Write("Род деятельности: ");
                string activity = Console.ReadLine();

                // Проверка данных
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Имя не может быть пустым");
                }

                if (string.IsNullOrWhiteSpace(activity))
                {
                    throw new ArgumentException("Род деятельности не может быть пустым");
                }

                var person = new Person(name, activity, "Описание");
                persons.Add(person);
                successCount++;

                Console.WriteLine("Персонаж успешно добавлен");
            }
            catch (ArgumentException ex)
            {
                errorCount++;
                Console.WriteLine($"Ошибка при добавлении персонажа: {ex.Message}");
                Console.WriteLine("Попробуйте ввести данные снова");
                // Продолжаем выполнение цикла
            }
            catch (Exception ex)
            {
                errorCount++;
                Console.WriteLine($"Неожиданная ошибка: {ex.GetType().Name}");
                // Продолжаем выполнение цикла
            }
        }

        Console.WriteLine($"\n Итог: успешно - {successCount}, с ошибками - {errorCount}");
        Console.WriteLine($"Всего персонажей в списке: {persons.Count}");
    }

    static void ProcessEncyclopediaWithRecovery()
    {
        Console.WriteLine("\n2. Создание энциклопедии с обработкой ошибок:");

        Encyclopedia enc = null;

        try
        {
            Console.Write("Введите название энциклопедии: ");
            string title = Console.ReadLine();

            Console.Write("Введите год издания: ");
            string yearInput = Console.ReadLine();

            // Попытка создать энциклопедию
            enc = new Encyclopedia(title, yearInput);

            // Попытка добавить персонажей
            Console.Write("Сколько персонажей добавить? ");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                for (int i = 0; i < count; i++)
                {
                    // Локальная обработка для каждого персонажа
                    try
                    {
                        var person = new Person($"Персонаж {i + 1}", "Тест", "Тест");
                        enc.AddPerson(person);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при добавлении персонажа {i + 1}: {ex.Message}");
                        // Продолжаем добавлять остальных
                    }
                }
            }

            Console.WriteLine($"Энциклопедия создана: {enc}");
            if (enc.PersonCount > 0)
            {
                Console.WriteLine($"Количество персонажей: {enc.PersonCount}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка аргумента: {ex.Message}");

            // Восстановление - создаем энциклопедию по умолчанию
            Console.WriteLine("Создание энциклопедии по умолчанию...");
            enc = new Encyclopedia("Энциклопедия по умолчанию", DateTime.Now.Year.ToString());
            Console.WriteLine($"Создана: {enc}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критическая ошибка: {ex.Message}");

            // Восстановление - создаем пустую энциклопедию
            enc = new Encyclopedia("Резервная энциклопедия", "2023");
            Console.WriteLine($"Создана резервная энциклопедия: {enc}");
        }
        finally
        {
            Console.WriteLine(" Блок finally выполнен");
        }
    }

    static void UseCollectionWithCleanup()
    {
        Console.WriteLine("\n3. Работа с коллекцией и очистка ресурсов:");

        MyCollection<int> collection = null;

        try
        {
            Console.Write("Введите размер коллекции: ");
            string sizeInput = Console.ReadLine();

            if (!int.TryParse(sizeInput, out int size) || size <= 0)
            {
                Console.WriteLine("Некорректный размер, используется значение по умолчанию: 5");
                size = 5;
            }

            collection = new MyCollection<int>(size);

            // Добавляем элементы с возможными ошибками
            for (int i = 0; i < size + 2; i++) // Намеренно добавляем больше элементов
            {
                try
                {
                    collection.AddItem(i * 10);
                    Console.WriteLine($"Добавлен элемент: {i * 10}");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($" Превышена вместимость коллекции при добавлении {i * 10}");
                    break; // Выходим из цикла
                }
            }

            // Попытка получить элемент по индексу
            Console.Write($"Введите индекс для получения элемента [0-{collection.Count - 1}]: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                try
                {
                    int element = collection.GetItem(index);
                    Console.WriteLine($"Элемент [{index}] = {element}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($" {ex.Message}");
                }
            }

            // Попытка найти минимум/максимум
            if (collection.Count > 0)
            {
                try
                {
                    Console.WriteLine($"Минимальный элемент: {collection.Min()}");
                    Console.WriteLine($"Максимальный элемент: {collection.Max()}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($" {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Общая ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Выполняется очистка ресурсов...");
            // В реальном приложении здесь была бы очистка ресурсов
            Console.WriteLine("Ресурсы коллекции освобождены");
        }
    }
}

// ============================================================
// 5. СОБСТВЕННЫЕ КЛАССЫ ИСКЛЮЧЕНИЙ
// ============================================================

// 5.1. Базовый класс для исключений энциклопедии
public class EncyclopediaException : Exception
{
    public DateTime ErrorTime { get; }

    public EncyclopediaException()
        : base("Ошибка в работе с энциклопедией")
    {
        ErrorTime = DateTime.Now;
    }

    public EncyclopediaException(string message)
        : base(message)
    {
        ErrorTime = DateTime.Now;
    }

    public EncyclopediaException(string message, Exception inner)
        : base(message, inner)
    {
        ErrorTime = DateTime.Now;
    }
}

// 5.2. Исключение для некорректного формата данных энциклопедии
public class EncyclopediaFormatException : EncyclopediaException
{
    public string InvalidValue { get; }
    public string ExpectedFormat { get; }

    public EncyclopediaFormatException(string message, string invalidValue, string expectedFormat)
        : base(message)
    {
        InvalidValue = invalidValue;
        ExpectedFormat = expectedFormat;
    }
}

// 5.3. Исключение для некорректных данных валидации энциклопедии
public class EncyclopediaValidationException : EncyclopediaException
{
    public string FieldName { get; }
    public string InvalidValue { get; }

    public EncyclopediaValidationException(string message, string fieldName, string invalidValue)
        : base(message)
    {
        FieldName = fieldName;
        InvalidValue = invalidValue;
    }
}

// 5.4. Исключение для некорректных данных персонажа
public class PersonDataException : ArgumentException
{
    public string FieldName { get; }

    public PersonDataException(string message, string fieldName)
        : base(message)
    {
        FieldName = fieldName;
    }
}

public class MyCollection<T>
    where T : IComparable<T>
{
    public int Count => count;
    public int Capacity => capacity;
    private T[] items;
    private int count = 0;
    private int capacity;

    public MyCollection(int n)
    {
        if (n <= 0)
            throw new ArgumentException(
                "Вместимость коллекции должна быть положительной",
                nameof(n)
            );

        items = new T[n];
        capacity = n;
    }

    public void AddItem(T item)
    {
        if (count >= capacity)
        {
            throw new IndexOutOfRangeException($"Коллекция переполнена (вместимость: {capacity})");
        }

        items[count] = item;
        count++;
    }

    public T GetItem(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException($"Индекс {index} вне диапазона [0, {count - 1}]");

        return items[index];
    }

    public T Min()
    {
        if (count == 0)
            throw new InvalidOperationException("Коллекция пуста");

        T min = items[0];
        for (int i = 1; i < count; i++)
        {
            if (items[i].CompareTo(min) < 0)
                min = items[i];
        }
        return min;
    }

    public T Max()
    {
        if (count == 0)
            throw new InvalidOperationException("Коллекция пуста");

        T max = items[0];
        for (int i = 1; i < count; i++)
        {
            if (items[i].CompareTo(max) > 0)
                max = items[i];
        }
        return max;
    }
}

public class Person : IComparable<Person>
{
    private static int _count = 0;
    private string _name = string.Empty;
    private string _activityType = string.Empty;
    private string _description = string.Empty;

    /// <summary>
    /// Свойство Имени
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// Свойство типа кативности
    /// </summary>
    public string ActivityType
    {
        get => _activityType;
        set => _activityType = value;
    }

    /// <summary>
    /// Свойство описания
    /// </summary>
    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public static int Count
    {
        get => _count;
        set => _count = value;
    }

    public static int GetCount() => _count;

    public static void DisplayStaticInfo()
    {
        Console.WriteLine($"\n[Статическая информация класса Person]");
        Console.WriteLine($"Всего создано объектов: {_count}");
        Console.WriteLine($"Текущее время: {DateTime.Now}");
    }

    // Конструктор без параметров
    public Person()
    {
        Console.WriteLine("Person: parameterless constructor");
        Count++;
    }

    // Конструктор с параметрами
    public Person(string name, string activityType, string description)
    {
        Name = name;
        ActivityType = activityType;
        Description = description;
        Console.WriteLine("Person: constructor with parameters");
        Count++;
    }

    // Копирующий конструктор
    public Person(Person other)
    {
        Name = other.Name;
        ActivityType = other.ActivityType;
        Description = other.Description;
        Console.WriteLine("Person: copy constructor");
        Count++;
    }

    // Деструктор
    ~Person()
    {
        Console.WriteLine("Person: destructor");
        Count--;
    }

    public int CompareTo(Person other)
    {
        if (other == null)
            return 1;

        return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
    }

    // Переопределение Equals и GetHashCode для правильной работы сравнения
    public override bool Equals(object obj)
    {
        return obj is Person person
            && _name == person._name
            && _activityType == person._activityType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _activityType);
    }

    // Переопределение ToString для вывода
    public override string ToString()
    {
        return $"{_name} ({_activityType})";
    }

    // Метод устанавливающий свойства через ввод с консоли
    public void SetProperty()
    {
        Console.Write("Name: ");
        Name = Console.ReadLine();
        Console.Write("Date: ");
        ActivityType = Console.ReadLine();
        Console.Write("Description: ");
        Description = Console.ReadLine();
    }

    // Метод устанавливающий свойства через параметры метода
    public void SetProperty(string name, string activityType, string description)
    {
        Name = name;
        ActivityType = activityType;
        Description = description;
    }

    public virtual void Print()
    {
        Console.WriteLine(
            $"Name: {Name} - ActivityType: {ActivityType} - Descriotion: {Description}"
        );
    }
}

public class Painter : Person, IComparable<Painter>
{
    private string _style;

    public string Style
    {
        get => _style;
        set => _style = value;
    }

    public Painter(string name, string activityType, string description, string style)
        : base(name, activityType, description)
    {
        Style = style;
        Console.WriteLine($"Painter: constructor with parameters {typeof(Painter)}");
    }

    // Сравнение по имени (алфавитное)
    public int CompareTo(Painter other)
    {
        if (other == null)
            return 1;
        return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
    }

    public override string ToString()
    {
        return $"[Художник] {Name}, Стиль: {_style}";
    }

    public override void Print()
    {
        Console.WriteLine(
            $"Painter: \nName: {Name}\nActivityType: {ActivityType}\nDescriotion: {Description}\nStyle: {Style}"
        );
    }

    ~Painter()
    {
        Console.WriteLine($"Painter: destructor");
    }
}

public class Writer : Person, IComparable<Writer>
{
    private string _genre;

    public string Genre
    {
        get => _genre;
        set => _genre = value;
    }

    public Writer(string name, string activityType, string description, string genre)
        : base(name, activityType, description)
    {
        Genre = genre;
        Console.WriteLine($"Writer: constructor with parameters {typeof(Writer)}");
    }

    // Сравнение по имени (алфавитное)
    public int CompareTo(Writer other)
    {
        if (other == null)
            return 1;
        return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
    }

    public override string ToString()
    {
        return $"[Писатель] {Name}, Жанр: {_genre}";
    }

    public override void Print()
    {
        Console.WriteLine(
            $"Writer: \nName: {Name}\nActivityType: {ActivityType}\nDescriotion: {Description}\nGenre: {Genre}"
        );
    }

    ~Writer()
    {
        Console.WriteLine($"Writer: destructor");
    }
}

public class Encyclopedia
{
    private string _name = string.Empty;

    private string _publicationYear = string.Empty;

    private List<Person> personList = new List<Person>();

    private static int objectsInArrCount = 0;

    /// <summary>
    /// Свойство имени
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// Свойство даты публикации
    /// </summary>
    public string PublicationDate
    {
        get => _publicationYear;
        set => _publicationYear = value;
    }

    // Свойство для получения количества личностей в этой энциклопедии
    public int PersonCount => personList.Count;

    // Статическое свойство для получения информации о количестве объектов в массивах
    public static int ObjectsInArrCount => objectsInArrCount;

    // Статический метод для получения счетчика
    public static int GetObjectsInArrCount() => objectsInArrCount;

    public static Encyclopedia operator +(Encyclopedia enc, Person person)
    {
        enc.AddPerson(person);
        return enc;
    }

    public static Encyclopedia operator ++(Encyclopedia enc)
    {
        Person newPerson = new Person();
        enc.AddPerson(newPerson);
        return enc;
    }

    public Person this[int index]
    {
        get { return personList[index]; }
        set { personList[index] = value; }
    }

    public override string ToString()
    {
        return $"Энциклопедия: \"{Name}\" ({PublicationDate})";
    }

    // Конструктор без параметров
    public Encyclopedia()
    {
        Console.WriteLine("Encyclopedia: parameterless constructor");
    }

    // Конструктор с параметрами
    public Encyclopedia(string name, string publicationDate)
    {
        Name = name;
        PublicationDate = publicationDate;
        Console.WriteLine("Encyclopedia: constructor with parameters");
    }

    // Копирующий конструктор
    public Encyclopedia(Encyclopedia other)
    {
        Name = other.Name;
        PublicationDate = other.PublicationDate;
        Console.WriteLine("Encyclopedia: copy constructor");
    }

    ~Encyclopedia()
    {
        Console.WriteLine("Enciclopedia: destructor");
    }

    // Метод добавляющий личность в список
    public void AddPerson(Person person)
    {
        personList.Add(person);
        objectsInArrCount++;
    }

    // Метод добавляющий личность через параметры метода
    public void AddParamPerson(string name, string activityType, string description)
    {
        var person = new Person(name, activityType, description);
        personList.Add(person);
    }

    public void AddPersonCopies(Person person, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            var copiedPerson = new Person(person);
            personList.Add(copiedPerson);
        }
    }

    // Метод вывода всех данных по энциклопедии
    public void PrintAll()
    {
        Console.WriteLine("\n====== Enciclopedia ======");
        Console.WriteLine($"Title: {Name} \nPublicationDate: {PublicationDate}");

        Console.WriteLine("====== Personalitys ======");
        foreach (var person in personList)
        {
            person.Print();
            Console.WriteLine("--------------------------");
        }
    }
}

namespace Lab7;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №7 ===");
        Console.WriteLine("=== Шаблоны классов (Generics) ===\n");

        Console.WriteLine("СОЗДАНИЕ ШАБЛОНА КЛАССА MyCollection<T>:");
        Console.WriteLine("-------------------------------------------");

        // Применение шаблона для разных типов данных

        // Для типа int
        Console.WriteLine("\nДЛЯ ТИПА int:");
        Console.WriteLine("------------------");

        MyCollection<int> intCollection = new MyCollection<int>(5);
        intCollection.AddItem(10);
        intCollection.AddItem(25);
        intCollection.AddItem(5);
        intCollection.AddItem(40);
        intCollection.AddItem(15);

        Console.WriteLine("Содержимое коллекции int:");
        intCollection.Print();
        Console.WriteLine($"Минимальное значение: {intCollection.Min()}");
        Console.WriteLine($"Максимальное значение: {intCollection.Max()}");

        // Для типа char
        Console.WriteLine("\nДЛЯ ТИПА char:");
        Console.WriteLine("-------------------");

        MyCollection<char> charCollection = new MyCollection<char>(4);
        charCollection.AddItem('d');
        charCollection.AddItem('a');
        charCollection.AddItem('c');
        charCollection.AddItem('b');

        Console.WriteLine("Содержимое коллекции char:");
        charCollection.Print();
        Console.WriteLine($"Минимальное значение: {charCollection.Min()}");
        Console.WriteLine($"Максимальное значение: {charCollection.Max()}");

        // Для указателей на базовый класс (в C# - ссылки на Person)
        Console.WriteLine("\nДЛЯ ТИПА Person (БАЗОВЫЙ КЛАСС):");
        Console.WriteLine("-------------------------------------");

        // Создаем объекты Person
        Person p1 = new Person("Иван Грозный", "Правление", "Первый русский царь");
        Person p2 = new Person("Петр I", "Реформы", "Российский император");
        Person p3 = new Person("Екатерина II", "Правление", "Императрица России");

        MyCollection<Person> personCollection = new MyCollection<Person>(3);
        personCollection.AddItem(p1);
        personCollection.AddItem(p2);
        personCollection.AddItem(p3);

        Console.WriteLine("Содержимое коллекции Person:");
        personCollection.Print();

        // Для работы Min() и Max() нужна перегрузка операторов сравнения в Person
        Console.WriteLine($"\nМинимальное значение (по имени):");
        Console.WriteLine(personCollection.Min());
        Console.WriteLine($"\nМаксимальное значение (по имени):");
        Console.WriteLine(personCollection.Max());

        // Для каждого из классов-наследников
        Console.WriteLine("\nДЛЯ КЛАССОВ-НАСЛЕДНИКОВ:");
        Console.WriteLine("----------------------------");

        // Для Painter
        Console.WriteLine("\nДЛЯ ТИПА Painter:");
        Console.WriteLine("-----------------------");

        Painter painter1 = new Painter(
            "Леонардо да Винчи",
            "Живопись",
            "Итальянский художник",
            "Возрождение"
        );
        Painter painter2 = new Painter(
            "Ван Гог",
            "Живопись",
            "Нидерландский художник",
            "Постимпрессионизм"
        );
        Painter painter3 = new Painter("Илья Репин", "Живопись", "Русский художник", "Реализм");

        MyCollection<Painter> painterCollection = new MyCollection<Painter>(3);
        painterCollection.AddItem(painter1);
        painterCollection.AddItem(painter2);
        painterCollection.AddItem(painter3);

        Console.WriteLine("Содержимое коллекции Painter:");
        painterCollection.Print();

        Console.WriteLine($"\nМинимальное значение (по имени):");
        Console.WriteLine(painterCollection.Min());
        Console.WriteLine($"\nМаксимальное значение (по имени):");
        Console.WriteLine(painterCollection.Max());

        // Для Writer
        Console.WriteLine("\nДЛЯ ТИПА Writer:");
        Console.WriteLine("-----------------------");

        Writer writer1 = new Writer("Лев Толстой", "Литература", "Русский писатель", "Роман");
        Writer writer2 = new Writer(
            "Федор Достоевский",
            "Литература",
            "Русский писатель",
            "Психологический роман"
        );
        Writer writer3 = new Writer("Антон Чехов", "Литература", "Русский писатель", "Рассказ");

        MyCollection<Writer> writerCollection = new MyCollection<Writer>(3);
        writerCollection.AddItem(writer1);
        writerCollection.AddItem(writer2);
        writerCollection.AddItem(writer3);

        Console.WriteLine("Содержимое коллекции Writer:");
        writerCollection.Print();

        Console.WriteLine($"\nМинимальное значение (по имени):");
        Console.WriteLine(writerCollection.Min());
        Console.WriteLine($"\nМаксимальное значение (по имени):");
        Console.WriteLine(writerCollection.Max());
    }
}

public class MyCollection<T>
    where T : IComparable<T>
{
    // Свойства для доступа к информации о коллекции
    public int Count => count;
    public int Capacity => capacity;

    private T[] items;
    private int count = 0;
    private int capacity;

    // Конструктор с параметром n
    public MyCollection(int n)
    {
        if (n <= 0)
            throw new ArgumentException(
                "Вместимость коллекции должна быть положительной",
                nameof(n)
            );

        items = new T[n];
        capacity = n;
        Console.WriteLine($"Создана MyCollection<{typeof(T).Name}> вместимостью {n}");
    }

    // Метод добавления элемента
    public void AddItem(T item)
    {
        if (count >= capacity)
        {
            Console.WriteLine($"Ошибка: коллекция MyCollection<{typeof(T).Name}> переполнена!");
            return;
        }

        items[count] = item;
        count++;
    }

    // Метод получения элемента по индексу
    public T GetItem(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException($"Индекс {index} вне диапазона [0, {count - 1}]");

        return items[index];
    }

    // Метод Min() для поиска минимального значения
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

    // Метод Max() для поиска максимального значения
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

    // Вспомогательный метод для вывода коллекции
    public void Print()
    {
        if (count == 0)
        {
            Console.WriteLine("Коллекция пуста");
            return;
        }

        Console.Write("[");
        for (int i = 0; i < count; i++)
        {
            Console.Write(items[i]);
            if (i < count - 1)
                Console.Write(", ");
        }
        Console.WriteLine("]");
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

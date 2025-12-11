namespace Lab6;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №6 ===");
        Console.WriteLine("=== Перегрузка операторов ===\n");

        Console.WriteLine("1. СОЗДАНИЕ ОБЪЕКТОВ И ИНИЦИАЛИЗАЦИЯ:");
        Console.WriteLine("------------------------------------");

        // Создаем объекты
        var painter1 = new Painter(
            "Леонардо да Винчи",
            "Живопись",
            "Итальянский художник",
            "Возрождение"
        );

        var painter2 = new Painter(
            "Винсент Ван Гог",
            "Живопись",
            "Нидерландский художник",
            "Постимпрессионизм"
        );

        var writer1 = new Writer("Лев Толстой", "Литература", "Русский писатель", "Роман");

        var writer2 = new Writer(
            "Федор Достоевский",
            "Литература",
            "Русский писатель",
            "Психологический роман"
        );

        // Создаем энциклопедию
        var mainEnc = new Encyclopedia("История искусства", DateTime.Now.ToString("dd.MM.yy"));

        Console.WriteLine($"\n2. ДЕМОНСТРАЦИЯ ПЕРЕГРУЖЕННОГО ОПЕРАТОРА +:");
        Console.WriteLine("------------------------------------------");

        // Используем перегруженный оператор + для добавления объектов
        Console.WriteLine("Добавляем объекты с помощью оператора +:");
        mainEnc = mainEnc + painter1;
        mainEnc = mainEnc + painter2;
        mainEnc = mainEnc + writer1;
        mainEnc = mainEnc + writer2;

        Console.WriteLine($"Всего объектов в энциклопедии: {mainEnc.PersonCount}");

        Console.WriteLine($"\n3. ДЕМОНСТРАЦИЯ ПРЕФИКСНОГО И ПОСТФИКСНОГО ++:");
        Console.WriteLine("----------------------------------------------");

        // Создаем новую энциклопедию для демонстрации
        var enc2 = new Encyclopedia("Тестовая", "01.01.23");

        Console.WriteLine("\n3.1. Использование префиксного ++:");
        Console.WriteLine($"До: количество объектов = {enc2.PersonCount}");
        ++enc2;
        Console.WriteLine($"После ++enc2: количество объектов = {enc2.PersonCount}");
        ++enc2;
        Console.WriteLine($"После ++enc2: количество объектов = {enc2.PersonCount}");

        Console.WriteLine("\n3.2. Использование постфиксного ++:");
        Console.WriteLine($"До: количество объектов = {enc2.PersonCount}");
        enc2++;
        Console.WriteLine($"После enc2++: количество объектов = {enc2.PersonCount}");
        enc2++;
        Console.WriteLine($"После enc2++: количество объектов = {enc2.PersonCount}");

        Console.WriteLine($"\n4. ДЕМОНСТРАЦИЯ ОПЕРАТОРА []:");
        Console.WriteLine("----------------------------");

        Console.WriteLine($"\nВсего объектов в mainEnc: {mainEnc.PersonCount}");
        Console.WriteLine("\nДоступ к элементам по индексу:");

        // Используем оператор [] для доступа к элементам
        Console.WriteLine($"Элемент 0: {mainEnc[0].Name}");
        Console.WriteLine($"Элемент 1: {mainEnc[1].Name}");
        Console.WriteLine($"Элемент 2: {mainEnc[2].Name}");
        Console.WriteLine($"Элемент 3: {mainEnc[3].Name}");

        Console.WriteLine($"\n5. ДЕМОНСТРАЦИЯ ГЛОБАЛЬНОГО ОПЕРАТОРА <<:");
        Console.WriteLine("-----------------------------------------");

        // Используем глобальный оператор << через Console.WriteLine
        Console.WriteLine("\nВывод энциклопедии с помощью оператора <<:");
        Console.WriteLine(mainEnc.ToString());
    }
}

public class Person
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

public class Painter : Person
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

public class Writer : Person
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

namespace Lab4;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №4 ===");
        Console.WriteLine("=== Полиморфизм и виртуальные методы ===\n");

        Console.WriteLine("1. СОЗДАНИЕ ОБЪЕКТОВ ENCYCLOPEDIA:");
        Console.WriteLine("----------------------------------");

        var enc1 = new Encyclopedia();
        Console.WriteLine($"Name: {enc1.Name}, {enc1.PublicationDate}");

        var enc2 = new Encyclopedia("TestName", DateTime.Now.ToString("dd.MM.yy"));
        Console.WriteLine($"Name: {enc2.Name}, {enc2.PublicationDate}");

        var enc3 = new Encyclopedia(enc2);
        Console.WriteLine($"Name: {enc3.Name}, {enc3.PublicationDate}");

        Console.WriteLine("\n2. СОЗДАНИЕ ОБЪЕКТОВ PERSON:");
        Console.WriteLine("----------------------------");

        var person1 = new Person();
        Console.WriteLine(
            $"Name: {person1.Name}, ActivityType: {person1.ActivityType}, Description: {person1.Description}"
        );

        var person2 = new Person("TestName", "TestActivityType", "TestDesription");
        Console.WriteLine(
            $"Name: {person2.Name}, ActivityType: {person2.ActivityType}, Description: {person2.Description}"
        );

        var person3 = new Person(person2);
        Console.WriteLine(
            $"Name: {person3.Name}, ActivityType: {person3.ActivityType}, Description: {person3.Description}"
        );

        Console.WriteLine("\n3. СОЗДАНИЕ ОБЪЕКТОВ НАСЛЕДНИКОВ:");
        Console.WriteLine("----------------------------");

        var painter = new Painter(
            "PainterName",
            "PainterActivityType",
            "PainterDescription",
            "PainterStyle"
        );
        var writer = new Writer(
            "WriterName",
            "WriterActivityType",
            "WriterDescription",
            "WriterGenre"
        );

        painter.Print();
        writer.Print();

        Console.WriteLine("\n3.1 СОЗДАНИЕ МАССИВА БАЗОВОГО КЛАССА С НАСЛЕДНИКАМИ:");
        Console.WriteLine("----------------------------");

        List<Person> persons = new List<Person>();
        persons.Add(painter);
        persons.Add(writer);
        persons.Add(person2);

        Console.WriteLine($"Вывод массива данных: ");
        foreach (var item in persons)
        {
            item.Print();
        }

        Console.WriteLine("\n3.2 СОЗДАНИЕ МАССИВА КЛАССА-НАСЛЕДНИКА:");
        Console.WriteLine("----------------------------");

        List<Painter> painters = new List<Painter>()
        {
            new Painter(
                "ArrPainterName1",
                "ArrPainterActivityType1",
                "ArrPainterDescription1",
                "ArrPainterStyle1"
            ),
            new Painter(
                "ArrPainterName2",
                "ArrPainterActivityType2",
                "ArrPainterDescription2",
                "ArrPainterStyle2"
            ),
        };

        foreach (var item in painters)
        {
            item.Print();
        }

        Console.WriteLine("\n3.3 СОЗДАНИЕ МАССИВА БАЗОВОГО КЛАССА:");
        Console.WriteLine("----------------------------");

        var inherited = new List<Person>()
        {
            new Painter(
                "testInheritedPainterName",
                "testInheritedPainterActivityType",
                "TestDescription",
                "testStyleInh"
            ),
            new Writer(
                "testInheritedWriterName",
                "testInheritedWriterActivityType",
                "TestDescription",
                "TestGenre"
            ),
        };

        foreach (var item in inherited)
        {
            item.Print();
        }

        Console.WriteLine("\n4. РАБОТА С ЭНЦИКЛОПЕДИЕЙ:");
        Console.WriteLine("---------------------------");

        var mainEnc = new Encyclopedia("Main Encyclopedia", DateTime.Now.ToString("dd.MM.yy"));

        var writerForEnc = new Writer(
            "WriterName",
            "WriterActivityType",
            "WriterDescription",
            "WriterGenre"
        );
        var painterForEnc = new Painter(
            "PainterName",
            "PainterActivityType",
            "PainterDescription",
            "PainterStyle"
        );

        mainEnc.AddPerson(writerForEnc);
        mainEnc.AddPerson(painterForEnc);

        var mainPerson1 = new Person("MainPerson1", "TestPersonActivity", "TestPersonDescription");
        mainEnc.AddPerson(mainPerson1);

        mainEnc.AddParamPerson("MainPerson2", "TestPerson2Activity", "TestPerson2Description");

        var personToCopy = new Person(
            "PersonToCopy",
            "TestCopyPersonActivity",
            "TestCopyPersonDescription"
        );
        mainEnc.AddPersonCopies(personToCopy, 3);

        mainEnc.PrintAll();

        Console.WriteLine("\n5. ДЕМОНСТРАЦИЯ ДЕСТРУКТОРОВ:");
        Console.WriteLine("----------------------------");

        CreateAndDestroyTemporaryObjects();

        // Вызываем GC явно
        Console.WriteLine("\nВызываем сборку мусора явно...");

        // нужно вызвать сборку мусора дважды
        // Первый вызов запускает финализацию
        GC.Collect();

        // Ждем завершения финализаторов
        GC.WaitForPendingFinalizers();

        // Второй вызов для очистки памяти
        GC.Collect();

        Console.WriteLine("\nДеструкторы были вызваны!");

        // Отдельный метод для создания и уничтожения временных объектов
        static void CreateAndDestroyTemporaryObjects()
        {
            Console.WriteLine("\nСоздание временных объектов в отдельном методе:");

            // Эти объекты будут уничтожены при выходе из метода
            Encyclopedia tempEnc = new Encyclopedia("TempEnc", DateTime.Now.ToString("dd.MM.yy"));
            Person tempPerson = new Person("TempPerson", "TempActivityType", "TempDescription");

            Console.WriteLine("Выход из метода CreateAndDestroyTemporaryObjects()...");
            // Объекты tempEnc и tempPerson теперь недоступны и могут быть удалены
        }
    }
}

public class Person
{
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

    // Конструктор без параметров
    public Person()
    {
        Console.WriteLine("Person: parameterless constructor");
    }

    // Конструктор с параметрами
    public Person(string name, string activityType, string description)
    {
        Name = name;
        ActivityType = activityType;
        Description = description;
        Console.WriteLine("Person: constructor with parameters");
    }

    // Копирующий конструктор
    public Person(Person other)
    {
        Name = other.Name;
        ActivityType = other.ActivityType;
        Description = other.Description;
        Console.WriteLine("Person: copy constructor");
    }

    // Деструктор
    ~Person()
    {
        Console.WriteLine("Person: destructor");
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

    public List<Person> personList = new List<Person>();

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

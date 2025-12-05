namespace Lab2;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №2 ===");
        Console.WriteLine("=== Конструкторы и деструкторы ===\n");

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

        Console.WriteLine("\n3. РАБОТА С ЭНЦИКЛОПЕДИЕЙ:");
        Console.WriteLine("---------------------------");

        var mainEnc = new Encyclopedia("Main Encyclopedia", DateTime.Now.ToString("dd.MM.yy"));

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

        Console.WriteLine("\n4. ДЕМОНСТРАЦИЯ ДЕСТРУКТОРОВ:");
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
            Console.WriteLine(
                $"Name: {person.Name}\nactivity type: {person.ActivityType}\nDescription: {person.Description}"
            );
            Console.WriteLine("--------------------------");
        }
    }
}

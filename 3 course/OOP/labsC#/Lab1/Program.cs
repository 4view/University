namespace Lab1;

public class Program
{
    public static void Main(string[] args)
    {
        Encyclopedia encyclopedia = new Encyclopedia()
        {
            Name = "TestEnciclopedia",
            PublicationDate = DateTime.Now.ToString(),
        };

        for (int i = 0; i < 3; i++)
        {
            Person person = new Person();
            person.SetProperty();
            encyclopedia.AddPerson(person);
        }

        encyclopedia.PrintAll();
    }
}

public class Person
{
    private string _name = string.Empty;
    private string _activityType = string.Empty;
    private string _description = string.Empty;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string ActivityType
    {
        get => _activityType;
        set => _activityType = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public void SetProperty()
    {
        Console.Write("Name: ");
        Name = Console.ReadLine();
        Console.Write("Date: ");
        ActivityType = Console.ReadLine();
        Console.Write("Description: ");
        Description = Console.ReadLine();
    }

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

    public List<Person> persons = new List<Person>();

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string PublicationDate
    {
        get => _publicationYear;
        set => _publicationYear = value;
    }

    public void AddPerson(Person person)
    {
        persons.Add(person);
    }

    public void PrintAll()
    {
        Console.WriteLine("====== Enciclopedia ======");
        Console.WriteLine($"Title: {Name} \nPublicationDate: {PublicationDate}");

        Console.WriteLine("====== Personalitys ======");
        foreach (var person in persons)
        {
            Console.WriteLine(
                $"Name: {person.Name}\nactivity type: {person.ActivityType}\nDescription: {person.Description}"
            );
            Console.WriteLine("--------------------------");
        }
    }
}

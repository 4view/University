namespace Lab13;

// Базовый класс для статей
public abstract class Article
{
    public string Name { get; set; }
    public string Author { get; set; }

    public Article(string name, string author)
    {
        Name = name;
        Author = author;
    }

    public abstract void Print();
}

public class Person : Article
{
    public string ActivityPeriod { get; set; }
    public string Description { get; set; }

    public Person(string name, string author, string activityPeriod, string description)
        : base(name, author)
    {
        ActivityPeriod = activityPeriod;
        Description = description;
    }

    public override void Print()
    {
        Console.WriteLine(
            $"Person: {Name}, Author: {Author}, Period: {ActivityPeriod}, Description: {Description}"
        );
    }
}

public interface IPrintStrategy
{
    string Format(Article article);
}

// Стратегия для HTML
public class HtmlPrintStrategy : IPrintStrategy
{
    public string Format(Article article)
    {
        return $"<p><b>{article.Name}</b> by {article.Author}</p>";
    }
}

// Стратегия для Text
public class TextPrintStrategy : IPrintStrategy
{
    public string Format(Article article)
    {
        return $"{article.Name} | Author: {article.Author}";
    }
}

public class Encyclopedia
{
    public string Title { get; set; }
    public int Year { get; set; }
    private List<Article> articles = new List<Article>();
    private IPrintStrategy printStrategy;

    public Encyclopedia(string title, int year, IPrintStrategy strategy)
    {
        Title = title;
        Year = year;
        printStrategy = strategy;
    }

    public void SetPrintStrategy(IPrintStrategy strategy)
    {
        printStrategy = strategy;
    }

    public void AddArticle(Article article)
    {
        articles.Add(article);
    }

    public void PrintAll()
    {
        Console.WriteLine($"Encyclopedia: {Title} ({Year})");
        foreach (var article in articles)
        {
            Console.WriteLine(printStrategy.Format(article));
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var htmlStrategy = new HtmlPrintStrategy();
        var textStrategy = new TextPrintStrategy();

        // Создаем энциклопедию с HTML стратегией
        Encyclopedia encyclopedia = new Encyclopedia("History in Faces", 2023, htmlStrategy);

        encyclopedia.AddArticle(
            new Person("Leonardo da Vinci", "Biographer1", "1452-1519", "Renaissance polymath")
        );
        encyclopedia.AddArticle(
            new Person("Isaac Newton", "Biographer2", "1643-1727", "Physicist and mathematician")
        );

        Console.WriteLine("=== HTML Format ===");
        encyclopedia.PrintAll();

        // Меняем стратегию на Text
        encyclopedia.SetPrintStrategy(textStrategy);

        Console.WriteLine("\n=== Text Format ===");
        encyclopedia.PrintAll();
    }
}

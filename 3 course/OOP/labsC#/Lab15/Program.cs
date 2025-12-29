using System;
using System.Collections.Generic;
using CommonClasses;

// Интерфейс компонента
public interface IArticleComponent
{
    void Print();
    string GetContent();
}

// Конкретный компонент
public class ArticleComponent : IArticleComponent
{
    private Article article;

    public ArticleComponent(Article article)
    {
        this.article = article;
    }

    public virtual void Print()
    {
        article.Print();
    }

    public virtual string GetContent()
    {
        return $"{article.Name} (автор: {article.Author})";
    }
}

// Базовый декоратор
public abstract class ArticleDecorator : IArticleComponent
{
    protected IArticleComponent component;

    public ArticleDecorator(IArticleComponent component)
    {
        this.component = component;
    }

    public virtual void Print()
    {
        component.Print();
    }

    public virtual string GetContent()
    {
        return component.GetContent();
    }
}

// Конкретные декораторы

// Декоратор для добавления рейтинга
public class RatingDecorator : ArticleDecorator
{
    private int rating;
    private string ratingSource;

    public RatingDecorator(IArticleComponent component, int rating, string ratingSource)
        : base(component)
    {
        this.rating = rating;
        this.ratingSource = ratingSource;
    }

    public override void Print()
    {
        component.Print();
        Console.WriteLine($"Рейтинг: {rating}/10 (источник: {ratingSource})");
    }

    public override string GetContent()
    {
        return $"{component.GetContent()} | Рейтинг: {rating}/10";
    }
}

// Декоратор для добавления тегов
public class TagsDecorator : ArticleDecorator
{
    private List<string> tags;

    public TagsDecorator(IArticleComponent component, List<string> tags)
        : base(component)
    {
        this.tags = tags;
    }

    public override void Print()
    {
        component.Print();
        Console.WriteLine($"Теги: {string.Join(", ", tags)}");
    }

    public override string GetContent()
    {
        return $"{component.GetContent()} | Теги: {string.Join(", ", tags)}";
    }
}

// Декоратор для добавления даты публикации
public class PublicationDateDecorator : ArticleDecorator
{
    private DateTime publicationDate;
    private bool isUpdated;

    public PublicationDateDecorator(
        IArticleComponent component,
        DateTime publicationDate,
        bool isUpdated = false
    )
        : base(component)
    {
        this.publicationDate = publicationDate;
        this.isUpdated = isUpdated;
    }

    public override void Print()
    {
        component.Print();
        string status = isUpdated ? "обновлено" : "опубликовано";
        Console.WriteLine($"{status}: {publicationDate:dd.MM.yyyy}");
    }

    public override string GetContent()
    {
        string status = isUpdated ? "обновлено" : "опубликовано";
        return $"{component.GetContent()} | {status}: {publicationDate:dd.MM.yyyy}";
    }
}

// Декоратор для добавления статуса проверки
public class VerificationDecorator : ArticleDecorator
{
    private bool isVerified;
    private string verifier;

    public VerificationDecorator(
        IArticleComponent component,
        bool isVerified,
        string verifier = "Администратор"
    )
        : base(component)
    {
        this.isVerified = isVerified;
        this.verifier = verifier;
    }

    public override void Print()
    {
        component.Print();
        string status = isVerified ? "Проверено" : "Не проверено";
        Console.WriteLine($"{status} ({verifier})");
    }

    public override string GetContent()
    {
        string status = isVerified ? "Проверено" : "Не проверено";
        return $"{component.GetContent()} | Статус: {status}";
    }
}

// Декоратор для добавления популярности
public class PopularityDecorator : ArticleDecorator
{
    private int views;
    private int likes;

    public PopularityDecorator(IArticleComponent component, int views, int likes)
        : base(component)
    {
        this.views = views;
        this.likes = likes;
    }

    public override void Print()
    {
        component.Print();
        double likeRatio = views > 0 ? (double)likes / views * 100 : 0;
        Console.WriteLine($"Просмотры: {views} | Лайки: {likes} ({likeRatio:F1}%)");
    }

    public override string GetContent()
    {
        return $"{component.GetContent()} | Просмотры: {views}, Лайки: {likes}";
    }
}

public class EncyclopediaManager
{
    private List<IArticleComponent> articles = new List<IArticleComponent>();

    public void AddArticle(IArticleComponent article)
    {
        articles.Add(article);
    }

    public void PrintAllArticles()
    {
        Console.WriteLine("\n=== ВСЕ СТАТЬИ ЭНЦИКЛОПЕДИИ ===\n");

        int counter = 1;
        foreach (var article in articles)
        {
            Console.WriteLine($"Статья #{counter}:");
            article.Print();
            Console.WriteLine($"Полный контент: {article.GetContent()}");
            Console.WriteLine("─".PadRight(50, '─') + "\n");
            counter++;
        }
    }

    public void PrintSimpleArticles()
    {
        Console.WriteLine("\n=== ПРОСТЫЕ СТАТЬИ (без декораторов) ===\n");

        foreach (var article in articles)
        {
            // Проверяем, является ли статья базовым компонентом
            if (article is ArticleComponent)
            {
                article.Print();
                Console.WriteLine();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем менеджер энциклопедии
        EncyclopediaManager manager = new EncyclopediaManager();

        // Создаем базовые статьи
        Article person = new Person(
            "Иван Грозный",
            "Историк Петров",
            "1530-1584",
            "Первый русский царь"
        );
        Article artist = new Artist("Андрей Рублёв", "Искусствовед Иванов", "Иконопись", "Троица");
        Article writer = new Writer(
            "Александр Пушкин",
            "Литературовед Смирнов",
            "Романтизм",
            "Евгений Онегин"
        );

        // Простая статья без декораторов
        Console.WriteLine("Простая статья без декораторов:");
        IArticleComponent simpleArticle = new ArticleComponent(person);
        simpleArticle.Print();
        Console.WriteLine();

        // Статья с одним декоратором (рейтинг)
        Console.WriteLine("Статья с рейтингом:");
        IArticleComponent ratedArticle = new RatingDecorator(
            new ArticleComponent(artist),
            9,
            "Экспертный совет"
        );
        ratedArticle.Print();
        manager.AddArticle(ratedArticle);
        Console.WriteLine();

        // Статья с несколькими декораторами
        Console.WriteLine("Статья с несколькими декораторами:");
        IArticleComponent decoratedArticle = new VerificationDecorator(
            new PublicationDateDecorator(
                new TagsDecorator(
                    new RatingDecorator(new ArticleComponent(writer), 10, "Читательский выбор"),
                    new List<string> { "Поэзия", "Классика", "Русская литература" }
                ),
                new DateTime(2023, 10, 15),
                true
            ),
            true,
            "Главный редактор"
        );
        decoratedArticle.Print();
        manager.AddArticle(decoratedArticle);
        Console.WriteLine();

        // Полностью декорированная статья (цепочка декораторов)
        Console.WriteLine("Полностью декорированная статья:");

        // Создаем новую статью
        Article newArtist = new Artist(
            "Илья Репин",
            "Критик Сидоров",
            "Реализм",
            "Бурлаки на Волге"
        );

        IArticleComponent fullyDecorated = new PopularityDecorator(
            new VerificationDecorator(
                new PublicationDateDecorator(
                    new TagsDecorator(
                        new RatingDecorator(new ArticleComponent(newArtist), 8, "Музей искусств"),
                        new List<string> { "Живопись", "Реализм", "XIX век" }
                    ),
                    new DateTime(2024, 1, 20)
                ),
                true
            ),
            12500,
            980
        );

        fullyDecorated.Print();
        manager.AddArticle(fullyDecorated);
        Console.WriteLine();

        // Выводим все статьи
        manager.PrintAllArticles();

        Console.WriteLine("Демонстрация динамического изменения декораторов:");

        IArticleComponent dynamicArticle = new ArticleComponent(
            new Person("Екатерина II", "Биограф", "1729-1796", "Императрица России")
        );

        Console.WriteLine("\nИзначальная статья:");
        dynamicArticle.Print();

        // Динамически добавляем декораторы
        dynamicArticle = new PublicationDateDecorator(dynamicArticle, new DateTime(2023, 12, 10));
        Console.WriteLine("\nПосле добавления даты публикации:");
        dynamicArticle.Print();

        dynamicArticle = new TagsDecorator(
            dynamicArticle,
            new List<string> { "История", "Монархия", "Просвещение" }
        );
        Console.WriteLine("\nПосле добавления тегов:");
        dynamicArticle.Print();

        dynamicArticle = new RatingDecorator(dynamicArticle, 7, "Студенческое голосование");
        Console.WriteLine("\nПосле добавления рейтинга:");
        dynamicArticle.Print();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}

namespace Lab16;

using System;
using System.Collections.Generic;
using CommonClasses;

public interface IEncyclopediaObserver
{
    void Update(EncyclopediaModel model);
}

public class EncyclopediaModel
{
    private string title;
    private int year;
    private List<Article> articles = new List<Article>();
    private List<IEncyclopediaObserver> observers = new List<IEncyclopediaObserver>();

    public string Title
    {
        get => title;
        set
        {
            if (title != value)
            {
                title = value;
                NotifyObservers();
            }
        }
    }

    public int Year
    {
        get => year;
        set
        {
            if (year != value)
            {
                year = value;
                NotifyObservers();
            }
        }
    }

    public IReadOnlyList<Article> Articles => articles.AsReadOnly();

    public void AddArticle(Article article)
    {
        articles.Add(article);
        NotifyObservers();
    }

    public void RemoveArticle(Article article)
    {
        if (articles.Remove(article))
        {
            NotifyObservers();
        }
    }

    public void ClearArticles()
    {
        articles.Clear();
        NotifyObservers();
    }

    public void AttachObserver(IEncyclopediaObserver observer)
    {
        observers.Add(observer);
    }

    public void DetachObserver(IEncyclopediaObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(this);
        }
    }

    public int GetArticlesCount() => articles.Count;

    public Dictionary<string, int> GetArticlesByType()
    {
        var stats = new Dictionary<string, int>();
        foreach (var article in articles)
        {
            string type = article.GetType().Name;
            if (stats.ContainsKey(type))
                stats[type]++;
            else
                stats[type] = 1;
        }
        return stats;
    }
}

public abstract class EncyclopediaView : IEncyclopediaObserver
{
    protected EncyclopediaModel model;
    protected string viewName;

    public string ViewName => viewName;

    public EncyclopediaView(EncyclopediaModel model, string viewName)
    {
        this.model = model;
        this.viewName = viewName;
        model.AttachObserver(this);
    }

    public abstract void Update(EncyclopediaModel model);
    public abstract void Display();

    public virtual void Dispose()
    {
        model.DetachObserver(this);
    }
}

public class ConsoleEncyclopediaView : EncyclopediaView
{
    public ConsoleEncyclopediaView(EncyclopediaModel model)
        : base(model, "Консольное представление") { }

    public override void Update(EncyclopediaModel model)
    {
        Console.WriteLine($"\n[{viewName}] Обновление!");
        Display();
    }

    public override void Display()
    {
        Console.WriteLine("=".PadRight(50, '='));
        Console.WriteLine($"ЭНЦИКЛОПЕДИЯ: {model.Title} ({model.Year})");

        Console.WriteLine($"\nСтатей: {model.GetArticlesCount()}");

        if (model.Articles.Count == 0)
        {
            Console.WriteLine("Нет статей");
        }
        else
        {
            Console.WriteLine("\nСодержание:");
            for (int i = 0; i < model.Articles.Count; i++)
            {
                Console.Write($"  {i + 1}. ");
                model.Articles[i].Print();
            }
        }
        Console.WriteLine("=".PadRight(50, '='));
    }
}

public class StatisticsEncyclopediaView : EncyclopediaView
{
    public StatisticsEncyclopediaView(EncyclopediaModel model)
        : base(model, "Статистика") { }

    public override void Update(EncyclopediaModel model)
    {
        Console.WriteLine($"\n[{viewName}] Обновлена статистика!");
        Display();
    }

    public override void Display()
    {
        Console.WriteLine($"\nСТАТИСТИКА: {model.Title}");
        int total = model.GetArticlesCount();
        var byType = model.GetArticlesByType();

        Console.WriteLine($"Всего статей: {total}");
        foreach (var type in byType)
        {
            Console.WriteLine($"  {type.Key}: {type.Value}");
        }
    }
}

public class EncyclopediaController
{
    private EncyclopediaModel model;
    private List<EncyclopediaView> views = new List<EncyclopediaView>();

    public EncyclopediaController(EncyclopediaModel model)
    {
        this.model = model;
    }

    public void SetTitle(string title)
    {
        model.Title = title;
    }

    public void SetYear(int year)
    {
        model.Year = year;
    }

    public void AddPersonArticle(string name, string author, string period, string description)
    {
        model.AddArticle(new Person(name, author, period, description));
    }

    public void AddArtistArticle(string name, string author, string style, string famousWork)
    {
        model.AddArticle(new Artist(name, author, style, famousWork));
    }

    public void AddWriterArticle(string name, string author, string genre, string famousBook)
    {
        model.AddArticle(new Writer(name, author, genre, famousBook));
    }

    public void RemoveArticle(int index)
    {
        if (index >= 0 && index < model.Articles.Count)
        {
            model.RemoveArticle(model.Articles[index]);
        }
    }

    public void ClearAllArticles()
    {
        model.ClearArticles();
    }

    public void AttachView(EncyclopediaView view)
    {
        views.Add(view);
    }

    public void DetachView(EncyclopediaView view)
    {
        views.Remove(view);
        view.Dispose();
    }

    public void DemonstrateMVC()
    {
        Console.WriteLine("\nСоздание энциклопедии и представлений");

        // Создаем представления
        var consoleView = new ConsoleEncyclopediaView(model);
        var statsView = new StatisticsEncyclopediaView(model);
        AttachView(consoleView);
        AttachView(statsView);

        // Настройка энциклопедии
        SetTitle("Великие личности");
        SetYear(2024);

        Console.WriteLine("\nДобавление статей");
        AddPersonArticle("Иван Грозный", "Историк", "1530-1584", "Первый царь");
        AddArtistArticle("Андрей Рублёв", "Искусствовед", "Иконопись", "Троица");

        Console.WriteLine("\nДобавление еще статей и изменение");
        AddWriterArticle("Александр Пушкин", "Литературовед", "Романтизм", "Евгений Онегин");
        SetTitle("Великие личности России");

        Console.WriteLine("\nУдаление статьи");
        RemoveArticle(0); // Удаляем первую статью

        Console.WriteLine("\nОчистка всех статей");
        ClearAllArticles();

        Console.WriteLine("\nДобавление новых статей");
        AddPersonArticle("Пётр I", "Историограф", "1672-1725", "Реформатор");
        AddArtistArticle("Илья Репин", "Критик", "Реализм", "Бурлаки на Волге");

        // Освобождаем ресурсы
        consoleView.Dispose();
        statsView.Dispose();
    }
}

static class Program
{
    [STAThread]
    static void Main()
    {
        // Создаем модель и контроллер
        var model = new EncyclopediaModel();
        var controller = new EncyclopediaController(model);

        // Запускаем демонстрацию
        controller.DemonstrateMVC();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}

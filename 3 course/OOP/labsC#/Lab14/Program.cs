using System;
using System.Collections.Generic;
using CommonClasses;

namespace Lab14_AbstractFactory
{
    // 1. Абстрактные продукты
    public abstract class PersonArticle : Article
    {
        public string ActivityPeriod { get; set; }
        public string Description { get; set; }

        protected PersonArticle(
            string name,
            string author,
            string activityPeriod,
            string description
        )
            : base(name, author)
        {
            ActivityPeriod = activityPeriod;
            Description = description;
        }
    }

    public abstract class ArtistArticle : Article
    {
        public string Style { get; set; }
        public string FamousWork { get; set; }

        protected ArtistArticle(string name, string author, string style, string famousWork)
            : base(name, author)
        {
            Style = style;
            FamousWork = famousWork;
        }
    }

    public abstract class WriterArticle : Article
    {
        public string Genre { get; set; }
        public string FamousBook { get; set; }

        protected WriterArticle(string name, string author, string genre, string famousBook)
            : base(name, author)
        {
            Genre = genre;
            FamousBook = famousBook;
        }
    }

    // 2. Конкретные продукты (русские)
    public class RussianPersonArticle : PersonArticle
    {
        public RussianPersonArticle(
            string name,
            string author,
            string activityPeriod,
            string description
        )
            : base(name, author, activityPeriod, description) { }

        public override void Print()
        {
            Console.WriteLine(
                $"Российская личность: {Name}, Автор: {Author}, Период: {ActivityPeriod}, Описание: {Description}"
            );
        }
    }

    public class RussianArtistArticle : ArtistArticle
    {
        public RussianArtistArticle(string name, string author, string style, string famousWork)
            : base(name, author, style, famousWork) { }

        public override void Print()
        {
            Console.WriteLine(
                $"Российский художник: {Name}, Стиль: {Style}, Известная работа: {FamousWork}"
            );
        }
    }

    public class RussianWriterArticle : WriterArticle
    {
        public RussianWriterArticle(string name, string author, string genre, string famousBook)
            : base(name, author, genre, famousBook) { }

        public override void Print()
        {
            Console.WriteLine(
                $"Российский писатель: {Name}, Жанр: {Genre}, Известная книга: {FamousBook}"
            );
        }
    }

    // 3. Конкретные продукты (английские)
    public class EnglishPersonArticle : PersonArticle
    {
        public EnglishPersonArticle(
            string name,
            string author,
            string activityPeriod,
            string description
        )
            : base(name, author, activityPeriod, description) { }

        public override void Print()
        {
            Console.WriteLine(
                $"English Person: {Name}, Author: {Author}, Period: {ActivityPeriod}, Description: {Description}"
            );
        }
    }

    public class EnglishArtistArticle : ArtistArticle
    {
        public EnglishArtistArticle(string name, string author, string style, string famousWork)
            : base(name, author, style, famousWork) { }

        public override void Print()
        {
            Console.WriteLine($"English Artist: {Name}, Style: {Style}, Famous Work: {FamousWork}");
        }
    }

    public class EnglishWriterArticle : WriterArticle
    {
        public EnglishWriterArticle(string name, string author, string genre, string famousBook)
            : base(name, author, genre, famousBook) { }

        public override void Print()
        {
            Console.WriteLine($"English Writer: {Name}, Genre: {Genre}, Famous Book: {FamousBook}");
        }
    }

    // 4. Абстрактная фабрика
    public abstract class ArticleFactory
    {
        public abstract PersonArticle CreatePersonArticle(
            string name,
            string author,
            string activityPeriod,
            string description
        );

        public abstract ArtistArticle CreateArtistArticle(
            string name,
            string author,
            string style,
            string famousWork
        );

        public abstract WriterArticle CreateWriterArticle(
            string name,
            string author,
            string genre,
            string famousBook
        );
    }

    // 5. Конкретные фабрики
    public class RussianArticleFactory : ArticleFactory
    {
        public override PersonArticle CreatePersonArticle(
            string name,
            string author,
            string activityPeriod,
            string description
        )
        {
            return new RussianPersonArticle(name, author, activityPeriod, description);
        }

        public override ArtistArticle CreateArtistArticle(
            string name,
            string author,
            string style,
            string famousWork
        )
        {
            return new RussianArtistArticle(name, author, style, famousWork);
        }

        public override WriterArticle CreateWriterArticle(
            string name,
            string author,
            string genre,
            string famousBook
        )
        {
            return new RussianWriterArticle(name, author, genre, famousBook);
        }
    }

    public class EnglishArticleFactory : ArticleFactory
    {
        public override PersonArticle CreatePersonArticle(
            string name,
            string author,
            string activityPeriod,
            string description
        )
        {
            return new EnglishPersonArticle(name, author, activityPeriod, description);
        }

        public override ArtistArticle CreateArtistArticle(
            string name,
            string author,
            string style,
            string famousWork
        )
        {
            return new EnglishArtistArticle(name, author, style, famousWork);
        }

        public override WriterArticle CreateWriterArticle(
            string name,
            string author,
            string genre,
            string famousBook
        )
        {
            return new EnglishWriterArticle(name, author, genre, famousBook);
        }
    }

    public class Encyclopedia
    {
        public string Title { get; set; }
        public int Year { get; set; }
        private List<Article> articles = new List<Article>();

        public Encyclopedia(string title, int year)
        {
            Title = title;
            Year = year;
        }

        public void AddArticle(Article article)
        {
            articles.Add(article);
        }

        public void PrintAll()
        {
            Console.WriteLine($"\n=== Энциклопедия: {Title} ({Year}) ===");
            Console.WriteLine($"Всего статей: {articles.Count}");
            Console.WriteLine("----------------------------------------");

            int counter = 1;
            foreach (var article in articles)
            {
                Console.Write($"{counter}. ");
                article.Print();
                counter++;
            }
            Console.WriteLine("----------------------------------------\n");
        }
    }

    public class EncyclopediaCreator
    {
        private ArticleFactory factory;

        public EncyclopediaCreator(ArticleFactory factory)
        {
            this.factory = factory;
        }

        public Encyclopedia CreateEncyclopedia(string title, int year)
        {
            Encyclopedia encyclopedia = new Encyclopedia(title, year);

            // Создаем статьи с помощью фабрики
            encyclopedia.AddArticle(
                factory.CreatePersonArticle(
                    "Ivan the Terrible",
                    "Historian Petrov",
                    "1530-1584",
                    "First Russian Tsar"
                )
            );

            encyclopedia.AddArticle(
                factory.CreateArtistArticle(
                    "Andrei Rublev",
                    "Art Historian Ivanov",
                    "Iconography",
                    "Trinity"
                )
            );

            encyclopedia.AddArticle(
                factory.CreateWriterArticle(
                    "Alexander Pushkin",
                    "Literary Critic Smirnov",
                    "Romanticism",
                    "Eugene Onegin"
                )
            );

            return encyclopedia;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем русскую энциклопедию с помощью русской фабрики
            Console.WriteLine("Создаем русскую энциклопедию:");
            ArticleFactory russianFactory = new RussianArticleFactory();
            EncyclopediaCreator russianCreator = new EncyclopediaCreator(russianFactory);
            Encyclopedia russianEncyclopedia = russianCreator.CreateEncyclopedia(
                "Российская история",
                2023
            );
            russianEncyclopedia.PrintAll();

            // Создаем английскую энциклопедию с помощью английской фабрики
            Console.WriteLine("Создаем английскую энциклопедию:");
            ArticleFactory englishFactory = new EnglishArticleFactory();
            EncyclopediaCreator englishCreator = new EncyclopediaCreator(englishFactory);
            Encyclopedia englishEncyclopedia = englishCreator.CreateEncyclopedia(
                "World History",
                2023
            );
            englishEncyclopedia.PrintAll();

            // Демонстрация работы с разными фабриками
            Console.WriteLine("Демонстрация работы фабрик:");

            // Используем русскую фабрику
            Console.WriteLine("\nИспользуем русскую фабрику:");
            Article russianArticle1 = russianFactory.CreatePersonArticle(
                "Пётр I",
                "Историк",
                "1672-1725",
                "Царь-реформатор"
            );
            Article russianArticle2 = russianFactory.CreateArtistArticle(
                "Илья Репин",
                "Искусствовед",
                "Реализм",
                "Бурлаки на Волге"
            );
            russianArticle1.Print();
            russianArticle2.Print();

            // Используем английскую фабрику
            Console.WriteLine("\nИспользуем английскую фабрику:");
            Article englishArticle1 = englishFactory.CreatePersonArticle(
                "William Shakespeare",
                "Biographer",
                "1564-1616",
                "Playwright"
            );
            Article englishArticle2 = englishFactory.CreateArtistArticle(
                "Vincent van Gogh",
                "Art Critic",
                "Post-Impressionism",
                "Starry Night"
            );
            englishArticle1.Print();
            englishArticle2.Print();

            // Демонстрация полиморфизма
            Console.WriteLine("\nДемонстрация полиморфизма (работа с абстрактной фабрикой):");
            DemonstrateFactoryPolymorphism(russianFactory);
            DemonstrateFactoryPolymorphism(englishFactory);

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void DemonstrateFactoryPolymorphism(ArticleFactory factory)
        {
            Console.WriteLine($"\nРаботаем с фабрикой типа: {factory.GetType().Name}");

            List<Article> articles = new List<Article>
            {
                factory.CreatePersonArticle(
                    "Test Person",
                    "Test Author",
                    "1900-2000",
                    "Test Description"
                ),
                factory.CreateArtistArticle("Test Artist", "Art Author", "Test Style", "Test Work"),
                factory.CreateWriterArticle(
                    "Test Writer",
                    "Writer Author",
                    "Test Genre",
                    "Test Book"
                ),
            };

            foreach (var article in articles)
            {
                article.Print();
            }
        }
    }
}

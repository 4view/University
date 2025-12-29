namespace CommonClasses;

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

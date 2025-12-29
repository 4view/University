namespace CommonClasses;

public class Writer : Person
{
    public string Genre { get; set; }
    public string FamousBook { get; set; }

    public Writer(string name, string author, string genre, string famousBook)
        : base(name, author, "Writer", "Literary activity")
    {
        Genre = genre;
        FamousBook = famousBook;
    }

    public override void Print()
    {
        Console.WriteLine($"Writer: {Name}, Genre: {Genre}, Famous Book: {FamousBook}");
    }
}

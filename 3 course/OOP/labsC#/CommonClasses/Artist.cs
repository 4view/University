namespace CommonClasses;

public class Artist : Person
{
    public string Style { get; set; }
    public string FamousWork { get; set; }

    public Artist(string name, string author, string style, string famousWork)
        : base(name, author, "Artist", "Artistic activity")
    {
        Style = style;
        FamousWork = famousWork;
    }

    public override void Print()
    {
        Console.WriteLine($"Artist: {Name}, Style: {Style}, Famous Work: {FamousWork}");
    }
}

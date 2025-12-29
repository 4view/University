namespace CommonClasses;

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

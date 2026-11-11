namespace Books;

public enum GenreType
{
    Fiction,
    Science,
    History,
    Technology
}

public class Book {

    public int Id { get; }
    public GenreType Genre { get; }
    public int Year { get; }
    public bool IsBorrowed { get; private set; }
    public string Title { get; }
    public string Author { get; }
    private static int _count = 0;


    public Book(string title, string author, GenreType genre, int year) {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null or empty.");
        } else if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author cannot be null or empty.");
        } else if(year<1450 || year > DateTime.Now.Year)
        {
            throw new ArgumentException("Year must be between 1450 and the current year.");
        }

        Title = title;
        Author = author;
        Year = year;
        Genre = genre;
        _count++;
        Id = _count;

    }

    public Book(string title, string author): this(title, author, GenreType.Fiction , DateTime.Now.Year) { }

    public int Age => DateTime.Now.Year - Year;
    public bool IsClassic => Age > 50;
    public bool Borrow()
    {
        if(IsBorrowed)
        {
            return false;
        }
        IsBorrowed = true;
        return true;

    }

    public bool Return()
    {
        if (!IsBorrowed)
        {
            return false;
        }
        IsBorrowed = false;
        return true;
    }

    public string GetDescription()
    {
        var status = IsBorrowed ? "Borrowed" : "Available";
        return  $"#{Id} \"{Title}\" by {Author} ({Year}, {Genre}) - {status}"; 
    }
}
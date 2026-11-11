using Books;

namespace Practice_Library;

public enum LoanResult
{
    Success,
    NotFound,
    AlreadyBorrowed,
    NotBorrowed
}

public class Library
{
    private readonly List<Book> _books = new List<Book>();

    public int TotalBooks => _books.Count;

    public void AddBook(Book book)
    {
        _books.Add(book);
    }
    public Book? FindById(int id)
    {
        foreach (var book in _books)
        {
            if (id == book.Id)
            {
                return book;
            }
        }
        return null;
    }

    public List<Book> FindByAuthor(string author)
    {
        var books = new List<Book>();
        foreach (var book in _books)
        {
            if (book.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
            {
                books.Add(book);
            }

        }
        return books;
    }

    public List<Book> GetAvailableBooks()
    {
        var books = new List<Book>();
        foreach (var book in _books)
        {
            if (!book.IsBorrowed)
            {
                books.Add(book);
            }
        }
        return books;
    }

    public int CountByGenre(GenreType genre)
    {
        var count = 0;
        foreach (var book in _books)
        {
            if (book.Genre == genre)
            {
                count++;
            }
        }
        return count;
    }

    public LoanResult BorrowBook(int id)
    {
        var book = FindById(id);

        if (book is null)
        {
            return LoanResult.NotFound;
        }
        return book.Borrow() ? LoanResult.Success : LoanResult.AlreadyBorrowed;

    }
    public LoanResult ReturnBook(int id)
    {
        var book = FindById(id);

        if (book is null)
        {
            return LoanResult.NotFound;
        }
        return book.Return() ? LoanResult.Success : LoanResult.NotBorrowed;

    }

    public List<Book> GetAllBooks() => new List<Book>(_books);

}

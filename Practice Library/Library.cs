using Books;

namespace Practice_Library;

public enum BorrowResult
{
    Success,
    NotFound,
    AlreadyBorrowed,
    NotBorrowed
}

public class Library
{
    private List<Book> Bookslist { get; } = new List<Book>();

    public int TotalBooks => Bookslist.Count;

    public void AddBook(Book book)
    {
        Bookslist.Add(book);
    }
    public Book? FindByID(int id)
    {
        foreach (var book in Bookslist)
        {
            if (id == book.Id)
            {
                return book;
            }
        }
        return null;
    }

    public List<Book>? FindByAuthor(string author)
    {
        var books = new List<Book>();
        foreach (var book in Bookslist)
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
        foreach (var book in Bookslist)
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
        foreach (var book in Bookslist)
        {
            if (book.Genre == genre)
            {
                count++;
                continue;
            }
        }
        return count;
    }

    public BorrowResult BorrowBook(int id)
    {
        var isBookAvailable = FindByID(id);

        if (isBookAvailable is null)
        {
            return BorrowResult.NotFound;
        }
        return isBookAvailable.Borrow() ? BorrowResult.Success : BorrowResult.AlreadyBorrowed;

    }
    public BorrowResult ReturnBook(int id)
    {
        var isBookAvailable = FindByID(id);

        if (isBookAvailable is null)
        {
            return BorrowResult.NotFound;
        }
        return isBookAvailable.Return() ? BorrowResult.Success : BorrowResult.NotBorrowed;

    }

    public List<Book> GetAllBooks => new List<Book>(Bookslist);

}

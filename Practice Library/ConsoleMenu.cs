using Books;
using Practice_Library;

namespace Console_Menu;

public class ConsoleMenu
{
    private const int MinYear = 1450;

    private readonly Library _library;

    public ConsoleMenu(Library library)
    {
        _library = library ?? throw new ArgumentNullException(nameof(library));
    }

    public void Run()
    {
        int choice;
        do
        {
            PrintMenu();
            choice = ReadNumber("Choose an option: ");
            HandleChoice(choice);
            Console.WriteLine();
        } while (choice != 0);
    }

    private void PrintMenu()
    {
        Console.WriteLine("===== LIBRARY MENU =====");
        Console.WriteLine("1. List all books");
        Console.WriteLine("2. List available books");
        Console.WriteLine("3. Add a book");
        Console.WriteLine("4. Borrow a book");
        Console.WriteLine("5. Return a book");
        Console.WriteLine("6. Search by author");
        Console.WriteLine("7. Count books by genre");
        Console.WriteLine("0. Exit");
        Console.WriteLine("------------------------");
    }

    private void HandleChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                ListAllBooks();
                break;
            case 2:
                ListAvailableBooks();
                break;
            case 3:
                AddBook();
                break;
            case 4:
                BorrowBook();
                break;
            case 5:
                ReturnBook();
                break;
            case 6:
                SearchByAuthor();
                break;
            case 7:
                CountByGenre();
                break;
            case 0:
                Console.WriteLine("Thanks for visiting the library. Goodbye!");
                break;
            default:
                Console.WriteLine("Unknown option.");
                break;
        }
    }

    private void ListAllBooks()
    {
        Console.WriteLine($"--- All books ({_library.TotalBooks}) ---");
        PrintBooks(_library.GetAllBooks());
    }

    private void ListAvailableBooks()
    {
        var availableBooks = _library.GetAvailableBooks();
        Console.WriteLine($"--- Available books ({availableBooks.Count}) ---");
        PrintBooks(availableBooks);
    }

    private void AddBook()
    {
        Console.WriteLine("--- Add a book ---");

        var title = ReadText("Title: ");
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty. Book not added.");
            return;
        }

        var author = ReadText("Author: ");
        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Author cannot be empty. Book not added.");
            return;
        }

        if (!TryReadGenre(out GenreType genre))
        {
            Console.WriteLine("Book not added.");
            return;
        }

        var year = ReadNumber("Year: ");
        if (year < MinYear || year > DateTime.Now.Year)
        {
            Console.WriteLine($"Year must be between {MinYear} and {DateTime.Now.Year}. Book not added.");
            return;
        }

        var book = new Book(title, author, genre, year);
        _library.AddBook(book);
        Console.WriteLine($"Book added: {book.GetDescription()}");
    }

    private void BorrowBook()
    {
        var id = ReadNumber("Enter book ID to borrow: ");
        var result = _library.BorrowBook(id);

        switch (result)
        {
            case LoanResult.Success:
                Console.WriteLine("Book borrowed successfully.");
                break;
            case LoanResult.NotFound:
                Console.WriteLine("Book not found.");
                break;
            case LoanResult.AlreadyBorrowed:
                Console.WriteLine("This book is already borrowed.");
                break;
        }
    }

    private void ReturnBook()
    {
        var id = ReadNumber("Enter book ID to return: ");
        var result = _library.ReturnBook(id);

        switch (result)
        {
            case LoanResult.Success:
                Console.WriteLine("Book returned successfully.");
                break;
            case LoanResult.NotFound:
                Console.WriteLine("Book not found.");
                break;
            case LoanResult.NotBorrowed:
                Console.WriteLine("This book wasn't borrowed.");
                break;
        }
    }

    private void SearchByAuthor()
    {
        var author = ReadText("Author name (or part of it): ");
        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Author name cannot be empty.");
            return;
        }

        var results = _library.FindByAuthor(author);
        Console.WriteLine($"--- Books matching \"{author}\" ({results.Count}) ---");
        PrintBooks(results);
    }

    private void CountByGenre()
    {
        if (!TryReadGenre(out GenreType genre))
        {
            return;
        }

        var count = _library.CountByGenre(genre);
        Console.WriteLine($"{genre}: {count} book(s)");
    }

    private void PrintBooks(List<Book> books)
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine(book.GetDescription());
        }
    }

    private bool TryReadGenre(out GenreType genre)
    {
        Console.WriteLine("1. Fiction  2. Science  3. History  4. Technology");
        var number = ReadNumber("Choose a genre: ");

        if (number < 1 || number > 4)
        {
            Console.WriteLine("Invalid genre.");
            genre = GenreType.Fiction;
            return false;
        }

        genre = (GenreType)(number - 1);
        return true;
    }

    private string ReadText(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private int ReadNumber(string prompt)
    {
        int number;
        bool isValid;

        do
        {
            Console.Write(prompt);
            isValid = int.TryParse(Console.ReadLine(), out number);

            if (!isValid)
            {
                Console.WriteLine("Invalid number, please try again.");
            }
        } while (!isValid);

        return number;
    }
}
using Practice_Library;

namespace Console_Menu;

public class ConsoleMenu
{
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
                // TODO step 2: List all books
                break;
            case 2:
                // TODO step 2: List available books
                break;
            case 3:
                // TODO step 6: Add a book
                break;
            case 4:
                // TODO step 3: Borrow a book
                break;
            case 5:
                // TODO step 3: Return a book
                break;
            case 6:
                // TODO step 4: Search by author
                break;
            case 7:
                // TODO step 5: Count books by genre
                break;
            case 0:
                Console.WriteLine("Thanks for visiting the library. Goodbye!");
                break;
            default:
                Console.WriteLine("Unknown option.");
                break;
        }
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
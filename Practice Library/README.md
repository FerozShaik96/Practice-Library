# Practice Library – C# Library Management System

A console application for managing a small library: list books, add new ones, borrow and return them, search by author and count books by genre.

This is a **practice project** for the first three sections of the *Ultimate C# Masterclass* (C# Fundamentals and Basics of Object-Oriented Programming). It uses only concepts from those sections.

---

## Goals of the project

The aim is to practise writing well-designed classes, not just code that runs.

1. **Classes and objects:** model real things (`Book`, `Library`) as classes with their own data and behaviour.
2. **Encapsulation:** protect each object's state with `private` fields, get-only properties and `private set`, so data only changes through methods such as `Borrow()` and `Return()`.
3. **Constructors:** validate input **before** changing any state, throw `ArgumentException` for invalid data, and reuse logic with **constructor chaining** (`: this(...)`).
4. **Static members:** generate unique, automatic book IDs with a private static counter.
5. **Computed properties:** derive values (`Age`, `IsClassic`, `TotalBooks`) instead of storing them.
6. **Enums:** represent fixed choices (`GenreType`) and operation results (`LoanResult`) instead of magic numbers or strings.
7. **Collections and loops:** store books in a private `List<Book>` and search or filter it with `foreach` loops.
8. **Single Responsibility Principle (SRP):** each class has one job:
   - `Book` → one book's data and rules
   - `Library` → managing the collection
   - `ConsoleMenu` → user input and output only
9. **Robust user input:** read numbers with `int.TryParse` in a loop, so invalid input never crashes the program.
10. **Clean code habits:** C# naming conventions, clear names, no duplicated logic, and no magic numbers.

---

## Starting point

The program starts in **`UserInteractions.cs`**. It uses C# **top-level statements**, so there is no `Main` method; the code in this file runs first.

`UserInteractions.cs` does the following:

1. Creates a `Library`.
2. Adds the sample books (at least one classic, several genres, one created with the 2-parameter constructor).
3. Borrows one book in code.
4. Creates a `ConsoleMenu`, passing it the library.
5. Calls `menu.Run()` to start the interactive menu.

```csharp
var library = new Library();
library.AddBook(new Book("Clean Code", "Robert Martin", GenreType.Technology, 2008));
library.AddBook(new Book("Dune", "Frank Herbert", GenreType.Fiction, 1965));
// ...more books

var menu = new ConsoleMenu(library);
menu.Run();
```

### How to run

**Visual Studio:** open `Practice Library.sln` and press **F5** (or **Ctrl + F5** to run without debugging).

**Command line**, from the project folder:

```bash
dotnet run
```

---

## Project structure

```
Solution 'Practice Library'
└── Practice Library
    ├── Dependencies
    ├── Books.cs              GenreType enum + Book class
    ├── Library.cs            LoanResult enum + Library class
    ├── ConsoleMenu.cs        ConsoleMenu class (all user input and output)
    └── UserInteractions.cs   Entry point: sample data + starts the menu
```

| File | Contains | Namespace | Responsibility |
|---|---|---|---|
| `UserInteractions.cs` | Top-level statements | — | Starting point: sets up sample data and starts the menu |
| `Books.cs` | `GenreType`, `Book` | `Books` | A single book: its data, validation and borrow state |
| `Library.cs` | `LoanResult`, `Library` | `Practice_Library` | Manages the private collection of books |
| `ConsoleMenu.cs` | `ConsoleMenu` | `Console_Menu` | Shows the menu, reads and checks input, prints results |

---

## Features

### `GenreType` (enum)
`Fiction`, `Science`, `History`, `Technology`

### `Book`
- Automatic, unique `Id` from a private static counter (1, 2, 3, …)
- `Title`, `Author`, `Genre`, `Year`: get-only, set once in the constructor
- `IsBorrowed`: readable from outside, changed only by `Borrow()` / `Return()`
- Validation: title and author not empty; year from 1450 to the current year
- Second constructor `Book(title, author)` chains to the main one (Fiction, current year)
- Computed properties: `Age`, `IsClassic` (older than 50 years)
- `Borrow()` / `Return()` return `false` if the book is already in that state
- `GetDescription()`, for example: `#1 "Clean Code" by Robert Martin (2008, Technology) - Available`

### `LoanResult` (enum)
`Success`, `NotFound`, `AlreadyBorrowed`, `NotBorrowed`

The library reports **what happened**; the menu decides **what message to show**.

### `Library`
- Private readonly `List<Book>`; outside code can't change it directly
- `AddBook(Book book)`
- `FindById(int id)`: returns the book, or `null` if not found
- `FindByAuthor(string author)`: case-insensitive, partial match (`martin` finds "Robert Martin")
- `GetAllBooks()` and `GetAvailableBooks()`: return **new** lists, so the private list stays protected
- `CountByGenre(GenreType genre)`
- `BorrowBook(int id)` / `ReturnBook(int id)`: return a `LoanResult`
- `TotalBooks` computed property

### `ConsoleMenu`
```
===== LIBRARY MENU =====
1. List all books
2. List available books
3. Add a book
4. Borrow a book
5. Return a book
6. Search by author
7. Count books by genre
0. Exit
```
- Receives the `Library` through its constructor (dependency injection)
- `Run()`: a `do...while` loop that ends when the user chooses 0
- `HandleChoice()`: a `switch` where each option calls one private method
- Helpers:
  - `ReadNumber(prompt)`: keeps asking until a valid number is typed
  - `ReadText(prompt)`: reads text safely (never `null`)
  - `TryReadGenre(out genre)`: reads a genre 1–4, following the TryParse pattern
  - `PrintBooks(books)`: prints a list, or "No books found."
- Every input is checked **before** creating a book, so the program never crashes
- Separate messages for *not found*, *already borrowed* and *not borrowed*

## Progress

- [x] `GenreType` enum
- [x] `Book` class
- [x] `LoanResult` enum
- [x] `Library` class
- [x] `ConsoleMenu` class (all 7 options)
- [ ] `UserInteractions.cs`: 5 sample books, one borrowed in code
- [ ] All test cases passing

## Bonus ideas (optional)

- `Library.GetOldestBook()`: find the book with the smallest year
- Menu option 8, **Statistics:** total, available, borrowed, and number of classics
- Prevent duplicate books (same title **and** author) in `AddBook`
- Move the minimum year (1450) into `Book` as a public constant, so the menu and the class share one rule

---

## Concepts used

Variables · operators · `if` · `switch` · `do...while` / `foreach` loops · methods · string interpolation · ternary operator · `int.TryParse` · `out` parameters · `List<T>` · classes · constructors · constructor chaining · properties (`get`, `private set`) · computed properties · `static` members · `const` / `readonly` · enums · `ArgumentException` · dependency injection (constructor) · Single Responsibility Principle
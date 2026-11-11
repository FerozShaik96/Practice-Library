using Books;
using Practice_Library;

var library = new Library();
library.AddBook(new Book("Clean Code", "Robert Martin", GenreType.Technology, 2008));
library.AddBook(new Book("Dune", "Frank Herbert", GenreType.Fiction, 1965));
library.AddBook(new Book("Sapiens", "Yuval Noah Harari", GenreType.History, 2011));

Console.WriteLine(library.TotalBooks);
Console.WriteLine(library.BorrowBook(1));
Console.WriteLine(library.BorrowBook(1));
Console.WriteLine(library.BorrowBook(999));
Console.WriteLine(library.GetAvailableBooks().Count);
Console.WriteLine(library.FindByAuthor("martin").Count);
Console.WriteLine(library.CountByGenre(GenreType.Fiction));
Console.WriteLine(library.ReturnBook(1));
Console.WriteLine(library.ReturnBook(1));
Console.WriteLine(library.GetAvailableBooks().Count);


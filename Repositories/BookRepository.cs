using System;
using System.Collections.Generic;

public class BookRepository
{
	public List<Book> BookList { get; } = new List<Book>();

	public void AddBook(int isbn, string author, string title, int year)
	{
		BookList.Add(new Book(isbn, author, title, year));
	}

	public bool RemoveBook()
	{
		Console.Write("Enter the IBSN of the book to remove: ");
		if (!int.TryParse(Console.ReadLine(), out int isbn))
		{
			return false;
		}

		Book? bookToRemove = BookList.Find(book => book.IBSN == isbn);
		if (bookToRemove is null)
		{
			return false;
		}

		BookList.Remove(bookToRemove);
		return true;
	}

	public bool UpdateBook()
	{
		Console.Write("Enter the IBSN of the book to update: ");
		if (!int.TryParse(Console.ReadLine(), out int isbn))
		{
			return false;
		}

		Book? existingBook = BookList.Find(book => book.IBSN == isbn);
		if (existingBook is null)
		{
			return false;
		}

		Console.Write("Enter new author: ");
		string? author = Console.ReadLine();

		Console.Write("Enter new title: ");
		string? title = Console.ReadLine();

		Console.Write("Enter new year: ");
		if (!int.TryParse(Console.ReadLine(), out int year))
		{
			return false;
		}

		if (!string.IsNullOrWhiteSpace(author))
		{
			existingBook.Author = author;
		}

		if (!string.IsNullOrWhiteSpace(title))
		{
			existingBook.Title = title;
		}

		existingBook.Year = year;
		return true;
	}
}

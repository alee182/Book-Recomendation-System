using System;
using System.Collections.Generic;

public class BookRepository
{
	public List<Book> BookList { get; } = new List<Book>();

	public void AddBook(int isbn, string author, string title, int year)
	{
		BookList.Add(new Book(isbn, author, title, year));
	}

	public bool RemoveBook(int isbn)
	{
		Book? bookToRemove = BookList.Find(book => book.IBSN == isbn);
		if (bookToRemove is null)
		{
			return false;
		}

		BookList.Remove(bookToRemove);
		return true;
	}

	public bool UpdateBook(int isbn, string author, string title, int year)
	{
		Book? existingBook = BookList.Find(book => book.IBSN == isbn);
		if (existingBook is null)
		{
			return false;
		}

		existingBook.Author = author;
		existingBook.Title = title;
		existingBook.Year = year;
		return true;
	}
}

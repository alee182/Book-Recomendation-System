using System;
using System.Collections.Generic;

public class BookRepository : IBookRepository
{
	public List<Book> BookList { get; private set; } = new List<Book>();

	public int IsbnCount = 0;

	public BookRepository()
	{
	}

	public BookRepository(List<Book> bookList)
	{
		if (bookList is null)
		{
			throw new ArgumentNullException(nameof(bookList));
		}

		BookList = bookList;
		IsbnCount = bookList.Count > 0 ? bookList[^1].IBSN + 1 : 0;
	}

	public void AddBook(string author, string title, int year)
	{
		BookList.Add(new Book(IsbnCount, author, title, year));
		IsbnCount += 1;
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
	//isbn param only used for book identification not to be changed
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

	public Book? GetBookByID(int isbn)
	{
		Book? getBook = BookList.Find(book => book.IBSN == isbn);

		return getBook;
	}
}

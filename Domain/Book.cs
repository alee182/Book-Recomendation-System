using System;

public class Book
{
	public int IBSN { get; set; }
	public string Author { get; set; }
	public string Title { get; set; }
	public int Year { get; set; }

	public Book(int isbn, string author, string title, int year)
	{
		IBSN = isbn;
		Author = author;
		Title = title;
		Year = year;
	}

	public void DisplayInfo()
	{
		Console.WriteLine($"IBSN: {IBSN}, Author: {Author}, Title: {Title}, Year: {Year}");
	}
}

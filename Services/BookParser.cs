using System;
using System.Collections.Generic;

public class BookParser : Parser
{
	public List<Book> Books { get; } = new List<Book>();
	// First auto-generated ISBN value; increments by 1 for each parsed book.
	public int IsbnStartValue { get; set; } = 100;

	/// <summary>
	/// Converts ParsedFile into Book objects and stores them in the Books list.
	/// </summary>
	/// <returns>The number of books added to the Books list, or 0 if parsing fails.</returns>
	public override int Conversion()
	{
		// Reset previous results so each conversion reflects only current ParsedFile content.
		Books.Clear();

		if (string.IsNullOrWhiteSpace(ParsedFile))
		{
			return 0;
		}

		// Split text into lines while handling Windows/Unix newlines.
		string[] lines = ParsedFile.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		int currentIsbn = IsbnStartValue;

		foreach (string rawLine in lines)
		{
			string line = rawLine.Trim();
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}

			if (!TryParseBookLine(line, currentIsbn, out Book? parsedBook))
			{
				// Fail-fast on invalid input so no partial/uncertain data is kept.
				Books.Clear();
				return 0;
			}

			Books.Add(parsedBook!);
			currentIsbn++;
		}

		return Books.Count;
	}

	/// <summary>
	/// Validates and parses a single line of text into a Book object.
	/// </summary>
	/// <param name="line">A line in the format Author,Title,Year.</param>
	/// <param name="isbn">The ISBN value to assign to the created book.</param>
	/// <param name="book">The parsed Book object when parsing succeeds; otherwise null.</param>
	/// <returns>True when the line is valid and a Book is created; otherwise false.</returns>
	private static bool TryParseBookLine(string line, int isbn, out Book? book)
	{
		book = null;

		// Expected format: Author,Title,Year
		string[] parts = line.Split(',');
		if (parts.Length != 3)
		{
			return false;
		}

		string author = parts[0].Trim();
		string title = parts[1].Trim();
		string yearText = parts[2].Trim();

		if (string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(title))
		{
			return false;
		}

		if (!TryParseYear(yearText, out int year))
		{
			return false;
		}

		book = new Book(isbn, author, title, year);
		return true;
	}

	/// <summary>
	/// Parses a year value from either a single year or a year range.
	/// </summary>
	/// <param name="yearText">The year text to validate and parse.</param>
	/// <param name="year">The parsed year value when successful.</param>
	/// <returns>True when a valid year can be extracted; otherwise false.</returns>
	private static bool TryParseYear(string yearText, out int year)
	{
		year = 0;
		if (string.IsNullOrWhiteSpace(yearText))
		{
			return false;
		}

		// Accept either a single year (2003) or a range (2001-2008).
		string firstYear = yearText;
		int dashIndex = yearText.IndexOf('-');
		if (dashIndex > 0)
		{
			firstYear = yearText.Substring(0, dashIndex).Trim();
		}

		if (firstYear.Length != 4)
		{
			return false;
		}

		return int.TryParse(firstYear, out year);
	}
}

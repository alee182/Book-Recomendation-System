using System;

public class Processing : IProcessing
{
	public RatingParser RatingParse { get; set; } = new RatingParser();
	public BookParser BookParse { get; set; } = new BookParser();
	public int IdCount { get; set; }

	public IMemberRepository createMemberRepo()
	{
		return new MemberRepository();
	}

	public IRatingRepository createRatingRepo()
	{
		throw new NotImplementedException("Rating repository creation is not implemented yet.");
	}

	public IBookRepository createBookRepo()
	{
		if (BookParse is null || BookParse.Books is null)
		{
			throw new InvalidOperationException("BookParse.Books cannot be null when creating a BookRepository.");
		}

		return new BookRepository(BookParse.Books);
	}

    
}

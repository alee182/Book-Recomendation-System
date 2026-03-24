using System;

public class Processing : IProcessing
{
	public RatingParser RatingParse { get; set; }
	public BookParser BookParse { get; set; }
	public int IdCount { get; set; } = 1;
	private List<int> _memberIds = new List<int>();

	public Processing(string ratingFilePath, string bookFilePath)
	{
		RatingParse = new RatingParser();
		RatingParse.FileParse(ratingFilePath);
		RatingParse.Conversion();

		BookParse = new BookParser();
		BookParse.FileParse(bookFilePath);
		BookParse.Conversion();
	}

	public IMemberRepository createMemberRepo()
	{
		if (RatingParse is null || RatingParse.RatingDic.Count == 0)
		{
			throw new InvalidOperationException("RatingParse has no data. Ensure the ratings file was parsed first.");
		}

		// Build members from the names that were already parsed out of ratings.txt.
		// No re-parse needed — RatingDic was populated in the constructor.
		List<Member> members = new List<Member>();
		int currentId = IdCount;
		_memberIds.Clear();
		foreach (string name in RatingParse.RatingDic.Keys)
		{
			members.Add(new Member(name, currentId, false));
			_memberIds.Add(currentId);
			currentId++;
		}
		IdCount = currentId;
		return new MemberRepository(members, IdCount);
	}

	public IRatingRepository createRatingRepo()
	{
		if (RatingParse is null || RatingParse.RatingDic.Count == 0)
		{
			throw new InvalidOperationException("RatingParse has no data. Ensure the ratings file was parsed first.");
		}

		if (_memberIds.Count == 0)
		{
			throw new InvalidOperationException("A member repository must exist first.");
		}

		// Build the nested dictionary: memberId -> (bookISBN -> Rating).
		// _memberIds maps positionally to RatingDic.Keys — _memberIds[0] is the ID of the first member, etc.
		// Book ISBNs are positional: position i = IsbnStartValue + i (matches how BookParser assigned them).
		var ratings = new Dictionary<int, Dictionary<int, Rating>>();
		int memberIndex = 0;

		foreach (var kvp in RatingParse.RatingDic)
		{
			int memberId = _memberIds[memberIndex];
			var memberRatings = new Dictionary<int, Rating>();

			List<int> ratingValues = kvp.Value;
			for (int i = 0; i < ratingValues.Count; i++)
			{
				int rawValue = ratingValues[i];
				if (rawValue == 0) continue; // 0 means no rating was given

				int bookIsbn = BookParse.IsbnStartValue + i;
				RatingEnum ratingEnum = (RatingEnum)rawValue;
				memberRatings[bookIsbn] = new Rating(bookIsbn, memberId, ratingEnum);
			}

			ratings[memberId] = memberRatings;
			memberIndex++;
		}

		return new RatingRepository(ratings);
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

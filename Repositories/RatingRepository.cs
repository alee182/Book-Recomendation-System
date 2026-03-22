namespace DefaultNamespace;

public class RatingRepository : IRatingRepository
{
    
    private readonly Dictionary<int, Dictionary<int, Rating>> _ratings = new();

    public void AddRating(Rating rating)
    {
        int memberId = rating.Member.memberId;
        int bookId = rating.Book.ISBN;
        
        if (!_ratings.TryGetValue(MemberId, out var memberRatings))
        {
            memberRatings = new Dictionary<int, Rating>();
            _ratings[memberId] = memberRatings;
        }
    }

    public bool RemoveRating(Rating rating)
    {
        if (!_ratings.TryGetValue(rating.MemberId, out var memberRatings))
        {
            return false;
        }

        bool removed = memberRatings.Remove(rating.BookId);

        if (memberRatings.Count == 0)
        {
            _ratings.Remove(rating.MemberId);
        }

        return removed;

    }

    public Rating? GetRating(int memberId, int bookId)
    {
        if (_ratings.TryGetValue(memberId, out var memberRatings) && memberRatings.TryGetValue(bookId, out var rating))
        {
            return rating;
        }

        return null;
    }

    public List<Rating> GetAllForMember(int id)
    {
        if (_ratings.TryGetValue(memberId, out var memberRatings))
        {
            return memberRatings.Values.ToList();
        }

        return new List<Rating>();
    }
}
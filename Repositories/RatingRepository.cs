public class RatingRepository : IRatingRepository
{
    // memberId -> (ISBN -> rating)
    private readonly Dictionary<int, Dictionary<int, Rating>> _ratings;
        
    public RatingRepository(Dictionary<int, Dictionary<int, Rating>> ratings)
    {
        _ratings = ratings;
    }
    

    public void AddRating(Rating rating)
    {
        int memberId = rating.MemberId;
        int bookId = rating.BookId;

        //if member doesn't exist, create new dictionary for their rating to be added to.
        if (!_ratings.TryGetValue(memberId, out var memberRatings))
        {
            memberRatings = new Dictionary<int, Rating>();
            _ratings[memberId] = memberRatings;
        }

        // set if existing.
        memberRatings[bookId] = rating;
    }

    public bool RemoveRating(int memberId, int bookId)
    {
        //if member doesn't exist, return false.
        if (!_ratings.TryGetValue(memberId, out var memberRatings))
        {
            return false;
        }

        //remove rating.
        bool removed = memberRatings.Remove(bookId);
        
        return removed;
    }

    public Rating? GetRating(int memberId, int bookId)
    {
        //if memberId and bookId found, returns rating.
        if (_ratings.TryGetValue(memberId, out var memberRatings) &&
            memberRatings.TryGetValue(bookId, out var rating))
        {
            return rating;
        }

        return null;
    }

    public List<Rating> GetAllForMember(int memberId)
    {
        //if memberId found, return list of ratings
        if (_ratings.TryGetValue(memberId, out var memberRatings))
        {
            return memberRatings.Values.ToList();
        }

        //returns empty list
        return new List<Rating>();
    }
    //needed for usage in ratingservice, ADD TO UML
    public List<int> GetAllMemberIds()
    {
        return _ratings.Keys.ToList();
    }
}
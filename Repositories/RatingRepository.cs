/// <summary>
/// Implementation for the methods defined in the IRatingRepository interface.
/// Uses a nested dictionary where the keys are as follows:
/// memberId -> (bookId -> rating)
/// </summary>
public class RatingRepository : IRatingRepository
{
    //Storage for rating objects, KEYS: memberId -> (bookId -> rating)
    private readonly Dictionary<int, Dictionary<int, Rating>> _ratings;
    
    /// <summary>
    /// Constructor for initializing the repository.
    /// </summary>
    /// <param name="ratings">An existing dictionary that can be used to store rating objects.</param>
    public RatingRepository(Dictionary<int, Dictionary<int, Rating>> ratings)
    {
        _ratings = ratings;
    }
    
    /// <summary>
    /// Adds or updates a rating object in the repository.
    /// </summary>
    /// <param name="rating">The rating object to be added.</param>
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

        // set if existing
        memberRatings[bookId] = rating;
    }

    /// <summary>
    /// Removes a rating object from the repository based on keys in the dictionary.
    /// </summary>
    /// <param name="memberId">The member ID whose rating we are removing.</param>
    /// <param name="bookId">The book ID of the book that was rated.</param>
    /// <returns>Returns a boolean value depending on if rating removal was successful or not.</returns>
    public bool RemoveRating(int memberId, int bookId)
    {
        //if member doesn't exist, return false
        if (!_ratings.TryGetValue(memberId, out var memberRatings))
        {
            return false;
        }

        //remove rating
        bool removed = memberRatings.Remove(bookId);
        
        return removed;
    }

    /// <summary>
    /// Gets a rating object based on the keys of the nested dictionaries.
    /// </summary>
    /// <param name="memberId">The ID of the member who's rating we are getting.</param>
    /// <param name="bookId">The ID of the book that was rated.</param>
    /// <returns>Returns the rating object if found, otherwise returns null.</returns>
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

    /// <summary>
    /// Gets all ratings for a given member in the system.
    /// </summary>
    /// <param name="memberId">The ID of the member whose ratings we are getting.</param>
    /// <returns>Returns a list of all the given member's rating objects. Returns an empty list if no ratings there.</returns>
    public List<Rating> GetAllForMember(int memberId)
    {
        //if memberId found, return list of ratings
        if (_ratings.TryGetValue(memberId, out var memberRatings))
        {
            return memberRatings.Values.OrderBy(r => r.BookId).ToList();
        }

        //returns empty list
        return new List<Rating>();
    }
    
    /// <summary>
    /// Gets all member IDs that are used as keys in the outer dictionary. 
    /// </summary>
    /// <returns>Returns a list of all the member IDs in the outer dictionary as integers.</returns>
    public List<int> GetAllMemberIds()
    {
        return _ratings.Keys.ToList();
    }
}
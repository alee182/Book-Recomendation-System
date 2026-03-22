namespace DefaultNamespace;

using System.Collections.Generic;
using System.Linq;

public class RatingRepository : IRatingRepository
{
    // memberId -> (bookId -> rating)
    private readonly Dictionary<int, Dictionary<int, Rating>> _ratings = new();

    public void AddRating(Rating rating)
    {
        int memberId = rating.MemberId;
        int bookId = rating.BookId;

        if (!_ratings.TryGetValue(memberId, out var memberRatings))
        {
            memberRatings = new Dictionary<int, Rating>();
            _ratings[memberId] = memberRatings;
        }

        // Overwrite if exists (1 rating per member per book)
        memberRatings[bookId] = rating;
    }

    public bool RemoveRating(int memberId, int bookId)
    {
        if (!_ratings.TryGetValue(memberId, out var memberRatings))
        {
            return false;
        }

        bool removed = memberRatings.Remove(bookId);

        // Clean up empty member entry
        if (memberRatings.Count == 0)
        {
            _ratings.Remove(memberId);
        }

        return removed;
    }

    public Rating? GetRating(int memberId, int bookId)
    {
        if (_ratings.TryGetValue(memberId, out var memberRatings) &&
            memberRatings.TryGetValue(bookId, out var rating))
        {
            return rating;
        }

        return null;
    }

    public List<Rating> GetAllForMember(int memberId)
    {
        if (_ratings.TryGetValue(memberId, out var memberRatings))
        {
            return memberRatings.Values.ToList();
        }

        return new List<Rating>();
    }

    public List<Rating> GetAllRatings()
    {
        return _ratings.Values
            .SelectMany(memberRatings => memberRatings.Values)
            .ToList();
    }
}
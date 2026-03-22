namespace Book_Recomendation_System.Services;

using System.Collections.Generic;
using System.Linq;

public class RatingService : IRatingService
{
    private readonly int _memberId;
    private readonly IRatingRepository _ratingRepo;

    public RatingService(int memberId, IRatingRepository ratingRepo)
    {
        _memberId = memberId;
        _ratingRepo = ratingRepo;
    }

    public bool NewRating(int bookId, int memberId, RatingEnum rating)
    {
        // Optional: only allow the current member to create their own rating
        if (memberId != _memberId)
        {
            return false;
        }

        Rating newRating = new Rating(bookId, memberId, rating);
        _ratingRepo.AddRating(newRating);
        return true;
    }

    public List<Rating> ViewRating(int memberId)
    {
        return _ratingRepo.GetAllForMember(memberId);
    }

    public int CompareTo(int otherMemberId)
    {
        List<Rating> currentMemberRatings = _ratingRepo.GetAllForMember(_memberId);
        List<Rating> otherMemberRatings = _ratingRepo.GetAllForMember(otherMemberId);

        if (currentMemberRatings.Count == 0 || otherMemberRatings.Count == 0)
        {
            return 0;
        }

        Dictionary<int, RatingEnum> currentRatingsByBook = currentMemberRatings
            .ToDictionary(r => r.BookId, r => r.RatingValue);

        int similarityScore = 0;

        foreach (Rating otherRating in otherMemberRatings)
        {
            if (currentRatingsByBook.TryGetValue(otherRating.BookId, out RatingEnum currentRatingValue))
            {
                // Higher score when ratings are closer together
                int difference = System.Math.Abs((int)currentRatingValue - (int)otherRating.RatingValue);
                similarityScore += 5 - difference;
            }
        }

        return similarityScore;
    }

    public List<Rating> GenerateRecommendations()
    {
        List<Rating> allRatings = _ratingRepo.GetAllRatings();
        List<Rating> currentMemberRatings = _ratingRepo.GetAllForMember(_memberId);

        HashSet<int> booksAlreadyRated = currentMemberRatings
            .Select(r => r.BookId)
            .ToHashSet();

        HashSet<int> otherMemberIds = allRatings
            .Where(r => r.MemberId != _memberId)
            .Select(r => r.MemberId)
            .ToHashSet();

        int bestMatchId = -1;
        int bestScore = int.MinValue;

        foreach (int otherMemberId in otherMemberIds)
        {
            int score = CompareTo(otherMemberId);

            if (score > bestScore)
            {
                bestScore = score;
                bestMatchId = otherMemberId;
            }
        }

        if (bestMatchId == -1)
        {
            return new List<Rating>();
        }

        List<Rating> bestMatchRatings = _ratingRepo.GetAllForMember(bestMatchId);

        List<Rating> recommendations = bestMatchRatings
            .Where(r => !booksAlreadyRated.Contains(r.BookId))
            .Where(r => r.RatingValue > RatingEnum.Neutral)
            .ToList();

        return recommendations;
    }
}
namespace DefaultNamespace;

public interface IRatingService
{
    bool NewRating(int bookId, int memberId, RatingEnum rating);
    List<Rating> ViewRating(int memberId);
    int CompareTo(int otherMemberId);
    List<Rating> GenerateRecommendations();
}
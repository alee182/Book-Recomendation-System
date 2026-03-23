namespace DefaultNamespace;

public interface IRatingService
{
    bool NewRating(int bookId, int memberId, RatingEnum rating);
    List<Rating> ViewRatings(int memberId);
    int CompareTo(int otherMemberId);
    void GenerateRecommendations();
}
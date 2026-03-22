namespace DefaultNamespace;

public interface IRatingService
{
    int CompareTo(int id);
    bool NewRating(int isbn, RatingEnum rating, int memberId);
    void ViewRating();
    List<Book> GenerateRecommendations();
}
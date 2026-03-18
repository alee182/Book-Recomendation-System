namespace DefaultNamespace;

public interface IRatingRepository
{
    void AddRating(Rating rating);
    bool RemoveRating(Rating rating);
    bool UpdateRating(string author, string title, int year);
    List<Rating> GetAllForMember(int id);
}
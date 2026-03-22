namespace DefaultNamespace;

public interface IRatingRepository
{
    void AddRating(int memberId, int bookId, RatingEnum rating);
	bool RemoveRating(int memberId, int bookId);
	Rating? GetRating(int memberId, int bookId);
	List<Rating> GetAllForMember(int memberId);
}
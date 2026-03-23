public interface IRatingRepository
{
	void AddRating(Rating rating);
	bool RemoveRating(int memberId, int bookId);
	Rating? GetRating(int memberId, int bookId);
	List<Rating> GetAllForMember(int memberId);
	List<int> GetAllMemberIds();
}
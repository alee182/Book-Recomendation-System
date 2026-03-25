/// <summary>
/// Interface defining the methods that store and get rating objects and their keys.
/// </summary>
public interface IRatingRepository
{
	/// <summary>
	/// Adds a new rating object to the repository.
	/// </summary>
	/// <param name="rating">The Rating object to be added.</param>
	void AddRating(Rating rating);
	
	/// <summary>
	/// Removes a Rating object from the RatingRepository.
	/// </summary>
	/// <param name="memberId">The ID of the member whose rating is being removed.</param>
	/// <param name="bookId">The ID of the book the member had previously rated.</param>
	/// <returns>Returns a boolean value depending on if removal of the rating was successful or not.</returns>
	bool RemoveRating(int memberId, int bookId);
	
	/// <summary>
	/// Gets a rating object based on the ID of the member and the book.
	/// </summary>
	/// <param name="memberId">The ID of the member who rated a book.</param>
	/// <param name="bookId">The ID of the book the member rated.</param>
	/// <returns>Returns the rating object if found, otherwise null.</returns>
	Rating? GetRating(int memberId, int bookId);
	
	/// <summary>
	/// Gets all ratings that a given member has made.
	/// </summary>
	/// <param name="memberId">The ID of the member whose ratings we wish to get.</param>
	/// <returns>Returns a list of Rating objects that belong to the given member.</returns>
	List<Rating> GetAllForMember(int memberId);
	
	/// <summary>
	/// Gets a list of all the member IDs with ratings stored in the repository.
	/// </summary>
	/// <returns>Returns a list of integers that correspond to the member IDs stored in the repository.</returns>
	List<int> GetAllMemberIds();
}
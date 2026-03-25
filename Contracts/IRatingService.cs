/// <summary>
/// Interface defining methods for creating, viewing, and generating recommendations
/// based on ratings in the system.
/// </summary>
public interface IRatingService
{
    /// <summary>
    /// Creates a new rating and adds it to the system.
    /// </summary>
    /// <param name="bookId">The ID of the book being rated.</param>
    /// <param name="memberId">The ID of the member who is rating the book.</param>
    /// <param name="rating">The RatingEnum value the user is rating the book.</param>
    /// <returns>Returns a boolean if adding the rating was successful or not.</returns>
    bool NewRating(int bookId, int memberId, RatingEnum rating);
    
    /// <summary>
    /// Views the ratings belonging to a given user.
    /// </summary>
    /// <param name="memberId">The ID of the member whose ratings we are viewing.</param>
    /// <returns>Returns a list of the user's ratings.</returns>
    List<Rating> ViewRatings(int memberId);
    
    /// <summary>
    /// Compares the current user to another user in the system.
    /// </summary>
    /// <param name="otherMemberId">The ID of the member we are comparing the user with.</param>
    /// <returns>Returns a dot product value calculated by both users' respective ratings.</returns>
    int CompareTo(int otherMemberId);
    
    /// <summary>
    /// Generates a list of recommended books based on users with similar ratings, and prints that list.
    /// </summary>
    void GenerateRecommendations();
}
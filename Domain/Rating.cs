/// <summary>
/// Represents a rating given by a member to a book.
/// Stores the book ID, member ID, and the rating value.
/// </summary>
public class Rating
{
    // The ID of the book being rated.
    public int BookId { get; set; }
    
    //The ID of the member who the rating belongs to.
    public int MemberId { get; set; }
    //The RatingEnum value the member has rated the book.
    public RatingEnum RatingValue { get; set; }

    /// <summary>
    /// Constructor initializing the Rating object.
    /// </summary>
    /// <param name="bookId">The ID of the book being rated.</param>
    /// <param name="memberId">The ID of the member who the rating belongs to.</param>
    /// <param name="rating">The RatingEnum value the member has rated the book.</param>
    public Rating(int bookId, int memberId, RatingEnum rating)
    {
        BookId = bookId;
        MemberId = memberId;
        RatingValue = rating;
    }

    /// <summary>
    /// Displays the information associated with the rating.
    /// </summary>
    /// <param name="book">The book object associated with the rating.</param>
    /// <param name="member">The member object associated with the rating.</param>
    public void DisplayInfo(Book book, Member member)
    {
        Console.WriteLine($"Book: {book.Title}, Member: {member.Name}, Rating: {RatingValue}");
    }

    /// <summary>
    /// Updates the RatingEnumValue of the rating.
    /// </summary>
    /// <param name="newRating">The new RatingEnum value of the rating.</param>
	public void UpdateRating(RatingEnum newRating)
    {
        RatingValue = newRating;
    }
}
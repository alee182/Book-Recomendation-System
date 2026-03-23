public class Rating
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public RatingEnum RatingValue { get; set; }

    public Rating(int bookId, int memberId, RatingEnum rating)
    {
        BookId = bookId;
        MemberId = memberId;
        RatingValue = rating;
    }

    public void DisplayInfo(Book book, Member member)
    {
        Console.WriteLine($"Book: {book.Title}, Member: {member.Name}, Rating: {RatingValue}");
    }

	public void UpdateRating(RatingEnum newRating)
    {
        RatingValue = newRating;
    }
}
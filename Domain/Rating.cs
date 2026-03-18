namespace DefaultNamespace;

public class Rating
{
    public Book Book { get; set; }
    public Member Member { get; set; }
    public RatingEnum RatingValue { get; set; }

    public Rating(Book book, Member member, RatingEnum rating)
    {
        Book = book;
        Member = member;
        RatingValue = rating;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Book: {Book.Title}, Member: {Member.Name}, Rating: {RatingValue}");
    }
}
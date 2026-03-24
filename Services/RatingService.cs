

public class RatingService : IRatingService
{
    private readonly int _memberId;
    private readonly IRatingRepository _ratingRepo;
    private readonly IBookRepository _bookRepo;

    //
    public RatingService(int memberId, IRatingRepository ratingRepo, IBookRepository bookRepo)
    {
        _memberId = memberId;
        _ratingRepo = ratingRepo;
        _bookRepo = bookRepo;
    }
    //
    public bool NewRating(int bookId, int memberId, RatingEnum rating)
    {
        if (memberId != _memberId)
        {
            return false;
        }

        Rating newRating = new Rating(bookId, memberId, rating);
        _ratingRepo.AddRating(newRating);
        return true;
    }
    //
    public List<Rating> ViewRatings(int memberId)
    {
        return _ratingRepo.GetAllForMember(memberId);
    }

    public int CompareTo(int otherMemberId)
    {
        //current user's ratings
        List<Rating> currentMemberRatings = _ratingRepo.GetAllForMember(_memberId);
        //Who we are comparing's ratings
        List<Rating> otherMemberRatings = _ratingRepo.GetAllForMember(otherMemberId);
        int dotProduct = 0;
        //if either empty, return 0
        if (currentMemberRatings.Count == 0 || otherMemberRatings.Count == 0)
        {
            return 0;
        }

        //dot product logic.
        for(int i =  0; i < currentMemberRatings.Count; i++)
        {
            dotProduct += ((int)currentMemberRatings[i].RatingValue * (int)otherMemberRatings[i].RatingValue);
        }

        return dotProduct;
    }
// for GenerateRecommendations, use compareTo inside for each member to get dot product, before returning best dot product
    public void GenerateRecommendations()
    {
        List<Rating> currentMemberRatings = _ratingRepo.GetAllForMember(_memberId);
        List<int> memberIds = _ratingRepo.GetAllMemberIds();

        int? mostSimilarMember = null;
        int? bestDotProduct = null;

        foreach (int memberId in memberIds )
        {
            if (memberId == _memberId)
            {
                continue;
            }
            

            int similarity = CompareTo(memberId);

            if (similarity > bestDotProduct)
            {
                bestDotProduct = similarity;
                mostSimilarMember = memberId;
            }
        }

        //INCORRECT SYNTAX AND INCOMPLETE - needs ratingRepo reference, not bookRepo
        //List<Book> bestMatchRatings = _bookRepo.GetAllForMember(mostSimilarMember);
        
        //list books where they really liked (5):
        Console.WriteLine("Here are the books they really liked:");
        //for each book in the list, if rating == 5, consolewriteline book info.
        
        
        //list books where they liked (3);
        Console.WriteLine("Here are the books they liked:");
        //for each book in the list, if rating == 3, consolewriteline book info.
    }
}
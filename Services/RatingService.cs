

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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public List<Rating> ViewRatings(int memberId)
    {
        return _ratingRepo.GetAllForMember(memberId);
    }

    public int CompareTo(int otherMemberId)
    {
        List<Rating> currentRatings = _ratingRepo.GetAllForMember(_memberId);
        List<Rating> otherRatings = _ratingRepo.GetAllForMember(otherMemberId);

        int dotProduct = 0;

        foreach (Rating current in currentRatings)
        {
            foreach (Rating other in otherRatings)
            {
                if (current.BookId == other.BookId)
                {
                    dotProduct += (int)current.RatingValue * (int)other.RatingValue;
                }
            }
        }

        return dotProduct;
    }
// for GenerateRecommendations, use compareTo inside for each member to get dot product, before returning best dot product
    /// <summary>
    /// 
    /// </summary>
    public void GenerateRecommendations()
    {
        List<int> memberIds = _ratingRepo.GetAllMemberIds();

        //storing most similar member and the respective dot product
        int? mostSimilarMemberId = null;
        int bestDotProduct = int.MinValue;

        //finding the best match
        foreach (int memberId in memberIds)
        {
            if (memberId == _memberId)
                continue;

            int dotProduct = CompareTo(memberId);

            if (dotProduct > bestDotProduct)
            {
                bestDotProduct = dotProduct;
                mostSimilarMemberId = memberId;
            }
        }
        //if no match
        if (mostSimilarMemberId == null || bestDotProduct <= 0)
        {
            Console.WriteLine("No recommendations available.");
            return;
        }
        
        // Ratings for our user, and the best match
        List<Rating> currentRatings = _ratingRepo.GetAllForMember(_memberId);
        List<Rating> bestMatchRatings = _ratingRepo.GetAllForMember(mostSimilarMemberId.Value);
        
        //Listing their highest rated books.
        Console.WriteLine("Books they really liked:");

        foreach (Rating bestRating in bestMatchRatings)
        {
            //check if current user rated it.
            bool alreadyRated = false;

            foreach (Rating current in currentRatings)
            {
                if (current.BookId == bestRating.BookId)
                {
                    alreadyRated = true;
                    break;
                }
            }

            // then display book info if not and rated VeryPositive by best match.
            if (!alreadyRated && bestRating.RatingValue == RatingEnum.VeryPositive)
            {
                Book? book = _bookRepo.GetBookById(bestRating.BookId);

                book.DisplayInfo();
            }
        }
        
        //Do the same for Books rated Positive.
        Console.WriteLine("Books they liked:");

        foreach (Rating bestRating in bestMatchRatings)
        {
            bool alreadyRated = false;

            foreach (Rating current in currentRatings)
            {
                if (current.BookId == bestRating.BookId)
                {
                    alreadyRated = true;
                    break;
                }
            }

            if (!alreadyRated && bestRating.RatingValue == RatingEnum.Positive)
            {
                Book? book = _bookRepo.GetBookById(bestRating.BookId);
                
                book.DisplayInfo();
            }
        }
    }
}
/// <summary>
/// Implementation for the methods defined in the IRatingService interface.
/// </summary>
public class RatingService : IRatingService
{
    //Stores the current user's memberId.
    private readonly int _memberId;
    //stores an instance of ratingRepository.
    private readonly IRatingRepository _ratingRepo;
    //Stores an instance of BookRepository.
    private readonly IBookRepository _bookRepo;
    //Stores an instance of MemberRepository.
    private readonly IMemberRepository _memberRepo;

    /// <summary>
    /// Constructor for initializing RatingService.
    /// </summary>
    /// <param name="memberId">The ID of the current user.</param>
    /// <param name="ratingRepo">An instance of the rating repository, used to get rating info.</param>
    /// <param name="bookRepo">An instance of the book repository, used to get book info.</param>
    /// <param name="memberRepo">An instance of the member repository, used to get member info.</param>
    public RatingService(int memberId, IRatingRepository ratingRepo, IBookRepository bookRepo,  IMemberRepository memberRepo)
    {
        _memberId = memberId;
        _ratingRepo = ratingRepo;
        _bookRepo = bookRepo;
        _memberRepo = memberRepo;
    }
    
    /// <summary>
    /// Creates a new Rating Object, and adds it to the repository.
    /// </summary>
    /// <param name="bookId">The ID of the book object being rated.</param>
    /// <param name="memberId">The ID of the current user who is making a rating.</param>
    /// <param name="rating">The RatingEnum value of the new rating.</param>
    /// <returns>Returns a Boolean value if creation was successful or not.</returns>
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
    /// Gets all ratings for a given member.
    /// </summary>
    /// <param name="memberId">The ID of the given member whose ratings we are viewing.</param>
    /// <returns>Returns a list of all the ratings for a given member.</returns>
    public List<Rating> ViewRatings(int memberId)
    {
        return _ratingRepo.GetAllForMember(memberId);
    }
    
    /// <summary>
    /// Calculates the dot product using ratings from the current user and another given user.
    /// Only uses the books that both members have rated.
    /// </summary>
    /// <param name="otherMemberId">The ID of the member who we are comparing the current user to.</param>
    /// <returns>Returns the dot product.</returns>
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
    
    // for GenerateRecommendations,
    // use compareTo inside for each member to get dot product,
    // before checking if best dot product, then storing it.
    /// <summary>
    /// Prints a list of recommended books after finding the most similar user in the system using the
    /// CompareTo() method. The highest dot product output  from CompareTo is stored along with that user's
    /// memberId, which is used to find their highest rated books, and then recommends the ones that the
    /// user has not yet rated.
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
        
        //assigning member object based on matched id.
        Member? match = _memberRepo.GetMemberByID((int)mostSimilarMemberId);
        if (match == null)
        {
            Console.WriteLine("No recommendations available.");
            return;
        }
        Console.WriteLine($"You have similar taste in books as {match.Name}!");
        
        // Ratings for our user, and the best match
        List<Rating> currentRatings = _ratingRepo.GetAllForMember(_memberId);
        List<Rating> bestMatchRatings = _ratingRepo.GetAllForMember((int)mostSimilarMemberId);
        
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

            // then display book info if not yet rated, and rated VeryPositive by best match.
            if (!alreadyRated && bestRating.RatingValue == RatingEnum.VeryPositive)
            {
                Book? book = _bookRepo.GetBookByID(bestRating.BookId);

                if (book != null)
                {
                    book.DisplayInfo();
                }
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
                Book? book = _bookRepo.GetBookByID(bestRating.BookId);
                if (book != null)
                {
                    book.DisplayInfo();
                }
            }
        }
    }
}
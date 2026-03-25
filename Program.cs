using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks.Dataflow;
using System.Xml;
using System.Xml.XPath;

public class Program {
     public static void Main(string[] args)
    {
        IProcessing process = new Processing("ratings.txt", "books.txt");
        IMemberRepository memRepo = process.createMemberRepo();
        IBookRepository bookRepo = process.createBookRepo();
        IMemberService memService = new MemberService(memRepo);
        IRatingRepository rateRepo = process.createRatingRepo();
        
        


        Console.WriteLine("Welcome to the Book Recommendation Program.");

        
        

        int logoutSelection = -1;
        int loginSelection = -1;


        while (logoutSelection != 4)
        {
            if (memService.CurrentUser == null)
            {
                Console.WriteLine(@"************** MENU **************
* 1. Add a new member            *
* 2. Add a new book              *
* 3. Login                       *
* 4. Quit                        *
**********************************");

                logoutSelection = int.Parse(Console.ReadLine() ?? "");

                switch(logoutSelection)
                {
                    case 1:
                        Console.WriteLine("Enter the name of the new member:");
                        memService.NewMember(Console.ReadLine() ?? "");
                        
                        break;

                    case 2:
                        Console.WriteLine("Enter the author of the new book:");
                        string newBookAuthor = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter the title of the new book:");
                        string newBookTitle = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter the year (or range of years) of the new book:");
                        int newBookYear = int.Parse(Console.ReadLine() ?? "");

                        bookRepo.AddBook(newBookAuthor, newBookTitle, newBookYear);
                        

                        break;

                    case 3:
                        Console.WriteLine("Enter member account: ");
                        memService.Login(int.Parse(Console.ReadLine() ?? ""));
                        Console.WriteLine($"{memService.CurrentUser.Name}, you are logged in!");


                        break;

                    case 4:
                        break;
                    
                    default:
                        Console.WriteLine("Invalid Option! Please Select From The Given Options!");
                        break;
                }

                continue;
            }

            Console.WriteLine(@"************** MENU **************
* 1. Add a new member            *
* 2. Add a new book              *
* 3. Rate book                   *
* 4. View ratings                *
* 5. See recommendations         *
* 6. Logout                      *
**********************************");

            loginSelection = int.Parse(Console.ReadLine() ?? "");

            IRatingService rateService = new RatingService(memService.CurrentUser.ID, rateRepo, bookRepo, memRepo);


            switch(loginSelection)

                {
                    case 1:
                        Console.WriteLine("Enter the name of the new member:");
                        memService.NewMember(Console.ReadLine() ?? "");
                        break;

                    case 2:
                        Console.WriteLine("Enter the author of the new book:");
                        string newBookAuthor = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter the title of the new book:");
                        string newBookTitle = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter the year (or range of years) of the new book:");
                        int newBookYear = int.Parse(Console.ReadLine() ?? "");

                        bookRepo.AddBook(newBookAuthor, newBookTitle, newBookYear);
                        
                        break;

                    case 3:
                        Console.WriteLine("Enter the ISBN for the book you'd like to rate:");
                        int ratingBookISBN = int.Parse(Console.ReadLine() ?? "");
                        Console.WriteLine("Enter your rating:");
                        int bookRating = int.Parse(Console.ReadLine() ?? "");
                        
                        Book? getBook = bookRepo.GetBookByID(ratingBookISBN);

                        if (getBook == null)
                        {
                            Console.WriteLine("Invalid ISBN. Book not found.");
                            break;
                        }

                        rateService.NewRating(ratingBookISBN, memService.CurrentUser.ID, (RatingEnum)bookRating);
                        Console.WriteLine($"Your new rating for {ratingBookISBN}, {getBook.Author}, {getBook.Title}, {getBook.Year} => rating: {bookRating}");
                        break;

                    case 4:
                        List<Rating> ratings = rateService.ViewRatings(memService.CurrentUser.ID);
                        Console.WriteLine($"{memService.CurrentUser.Name}'s ratings...");

                        foreach (Rating rating in ratings)
                        {
                            Book ratedBook = bookRepo.GetBookByID(rating.BookId);

                            Console.WriteLine($"{rating.BookId}, {ratedBook.Author}, {ratedBook.Title}, {ratedBook.Year} => Rating: {(int)rating.RatingValue}");
                                
                        }
                        break;

                    case 5:
                        rateService.GenerateRecommendations();
                        break;

                    case 6:
                        memService.Logout();
                        break;
                    
                    default:
                        Console.WriteLine("Invalid Option! Please Select From The Given Options!");
                        break;
                }
        }


        Console.WriteLine("Thank you for using the Book Recommendation Program!");



        
    }






}
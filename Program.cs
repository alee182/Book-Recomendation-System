using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.XPath;

public class Program {
     public static void Main(string[] args)
    {
        IProcessing process = new Processing("ratings.txt", "books.txt");

        IBookRepository BookRepo = new BookRepository();
        IMemberService memService = new MemberService(process.createMemberRepo());
        //IRatingService rateService = new RatingService(process.createRatingRepo());

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

                logoutSelection = int.Parse(Console.ReadLine());

                switch(logoutSelection)
                {
                    case 1:
                        Console.WriteLine("Enter the name of the new member:");
                        memService.NewMember(Console.ReadLine());
                        
                        break;

                    case 2:

                        break;

                    case 3:
                        Console.WriteLine("Enter member account: ");
                        memService.Login(int.Parse(Console.ReadLine()));
                        Console.WriteLine($"{memService.CurrentUser.Name}");
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

            loginSelection = int.Parse(Console.ReadLine());

            switch(loginSelection)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:
                        

                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        memService.Logout(memService.CurrentUser.ID);
                        break;
                    
                    default:
                        Console.WriteLine("Invalid Option! Please Select From The Given Options!");
                        break;
                }
        }






        
    }






}
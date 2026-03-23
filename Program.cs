public class Program {
    public static void Main(string[] args)
    {
        IBookRepository BookRepo = new BookRepository();
        IMemberService memService = new MemberService();

        Console.WriteLine("Welcome to the Book Recommendation Program.");

        int logoutSelection = -1;
        int loginSelection = -1;

        while (logoutSelection != 4)
        {
            if (memService.CurrentUser == null)
            {
                Console.WriteLine("");
            }
        }
    }
}
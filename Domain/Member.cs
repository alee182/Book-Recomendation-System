public class Member
{
    public string Name {get; set;}

    public int ID {get; set;}

    public bool IsLoggedIn {get; set;}

    public Member(string name, int ID, bool isLoggedIn)
    {
        Name = name;
        this.ID = ID;
        IsLoggedIn = isLoggedIn;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Member Name: {Name}\nMember ID: {ID}\n Is Member Logged In: {IsLoggedIn}");
    }
}
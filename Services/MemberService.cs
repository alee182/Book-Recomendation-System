public class MemberService : IMemberService
{

    private readonly IMemberRepository _memberRepositroy;

    public int memberID = 0;

    public Member? CurrentUser {get; private set;}

    public MemberService(IMemberRepository memberRepositroy)
    {
        _memberRepositroy = memberRepositroy;
    }

    //Logs the user in given their ID by setting the current user to them.
    public void Login(int account)
    {
        CurrentUser = _memberRepositroy.GetMemberByID(account);
    }

    //Logs the user out by setting the current user to null.
    public void Logout()
    {
        CurrentUser = null;
    }

    //Creates a new user given their name.
    public void NewMember(String name)
    {
        memberID += 1;

        _memberRepositroy.AddMember(name, memberID);


    }
}
public class MemberService : IMemberService
{

    private readonly IMemberRepository _memberRepositroy;

    public int memberID = 0;

    public Member? CurrentUser {get; private set;}

    public MemberService(IMemberRepository memberRepositroy)
    {
        _memberRepositroy = memberRepositroy;
    }


    public void Login(int account)
    {
        CurrentUser = _memberRepositroy.GetMemberByID(account);
    }

    public void Logout(int account)
    {
        CurrentUser = null;
    }

    public void NewMember(String name)
    {
        memberID += 1;

        _memberRepositroy.AddMember(name, memberID);


    }
}
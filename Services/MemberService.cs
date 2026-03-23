public class MemberService : IMemberService
{

    private readonly IMemberRepository _memberRepositroy;

    private int memberID = 0;

    public Member? CurrentUser; 

    public MemberService(IMemberRepository memberRepositroy)
    {
        _memberRepositroy = memberRepsitory;
    }

    public void Loggin(int account)
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
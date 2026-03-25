public interface IMemberService
{
    public Member? CurrentUser {get;}

    
    void Login(int account);
    void Logout();
    void NewMember(String name);


}
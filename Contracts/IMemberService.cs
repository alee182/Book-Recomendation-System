public interface IMemberService
{
    public Member? CurrentUser {get;}
    void Login(int account);
    void Logout(int account);
    void NewMember(String name);


}
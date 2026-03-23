public interface IMemberRepository
{
    void AddMember(string name, int ID);
    bool RemoveMember(int ID);
    bool UpdateMember(string name, int ID);
    Member? GetMemberByID(int ID);
    Member? GetMemberID(string username);
}
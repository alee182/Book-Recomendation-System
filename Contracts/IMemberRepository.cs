public interface IMemberRepository
{
    void AddMember(string name, int ID);
    void RemoveMember(int ID);
    void UpdateMember(string name, int ID);
    Member? GetMemberByID(int ID);
}
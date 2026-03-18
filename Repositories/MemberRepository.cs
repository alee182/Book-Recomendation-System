public class MemberRepository : IMemberRepository
{
    public List<Member> Members {get;} = new List<Member>();
    public void AddMember(string name, int ID)
    {
        Members.Add(new Member(name, ID, false));
    }

    public void RemoveMember(int ID)
    {
        Members.RemoveAll(member => member.ID == ID);

    }

    public void UpdateMember(string name, int ID)
    {
        foreach (Member member in Members)
        {
            if (member.ID == ID)
            {
                member.Name = name;
            }
        }
    }

    public Member? GetMemberByID(int ID)
    {
        foreach (Member member in Members)
        {
            if (member.ID == ID)
            {
                return member;
            }
        }

        return null;
    }
}
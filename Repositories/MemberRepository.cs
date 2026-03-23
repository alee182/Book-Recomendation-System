public class MemberRepository : IMemberRepository
{
    public List<Member> Members {get;} = new List<Member>();

    int IdCount = 0;

    public MemberRepository(List<Member> members, int idcount)
    {
        Members = members;
        IdCount = idcount;
    }

    public void AddMember(string name, int ID)
    {
        Members.Add(new Member(name, ID, false));
        IdCount += 1;
    }

    public bool RemoveMember(int ID)
    {
        Member? removeMember = Members.Find(member => member.ID == ID);
        if (removeMember == null)
        {
            return false;
        }

        Members.Remove(removeMember);

        return true;

    }

    public bool UpdateMember(string name, int ID)
    {
        Member? updateMember = Members.Find(member => member.ID == ID);
        if (updateMember == null)
        {
            return false;
        }

        updateMember.Name = name;

        return true;

    }

    public Member? GetMemberByID(int ID)
    {
        Member? getMember = Members.Find(member => member.ID == ID);

        return getMember;
    }

    public Member? GetMemberID(string username)
    {
        Member? getMember = Members.Find(member => member.Name == username);

        return getMember;

    }
}
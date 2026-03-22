public interface IProcessing
{
	IMemberRepository createMemberRepo();
	//create rating repo can only be called after member repo is created
	IRatingRepository createRatingRepo(int[] ids);
	IBookRepository createBookRepo();
}

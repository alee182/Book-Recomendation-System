public interface IProcessing
{
	IMemberRepository createMemberRepo();
	DefaultNamespace.IRatingRepository createRatingRepo();
	IBookRepository createBookRepo();
}

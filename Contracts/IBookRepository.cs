public interface IBookRepository
{
	void AddBook(string author, string title, int year);
	bool RemoveBook(int isbn);
	bool UpdateBook(int isbn, string author, string title, int year);
	Book? GetBookByID(int isbn);
}

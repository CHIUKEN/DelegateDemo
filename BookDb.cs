namespace Test01;
public class BookDb
{
    private readonly List<Book> books = new List<Book>();
    public delegate int ProcessBookCallback(Book book);
    public Action<Book>? Notify;
    public BookDb()
    {
        books.Add(new Book("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 1200, true));
        books.Add(new Book("The Unicode Standard 2.0", "The Unicode Consortium", 500, true));
        books.Add(new Book("The MS-DOS Encyclopedia", "Ray Duncan", 30, false));
        books.Add(new Book("Dogbert's Clues for the Clueless", "Scott Adams", 12, true));
    }


    public int GetBookCountByPrice(ProcessBookCallback? callback)
    {
        int count = 0;
        if (callback != null)
        {
            foreach (var book in books)
            {
                count += callback.Invoke(book);
            }
        }
        return count;
    }

    public void GetBookCount(Action<Book> action)
    {
        foreach (var book in books)
        {
            action.Invoke(book);
        }
    }
    public void SetNotify(Action<Book> action)
    {
        Notify = action;
    }
    public void AddNewBook(Book book)
    {
        books.Add(book);
        Notify?.Invoke(book);
    }

    public int GetBookCount(Func<Book, bool> predicate)
    {
        var sum = 0;
        foreach (var book in books)
        {
            if (predicate(book))
            {
                sum ++;
                return sum;
            }
        }
        return sum;
    }
}
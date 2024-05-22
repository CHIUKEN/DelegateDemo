using Microsoft.Data.SqlClient;

namespace Test01;

class Program
{
    static void Main(string[] args)
    {
        // 方法一：具名委派
        BookDb.ProcessBookCallback callback = new BookDb.ProcessBookCallback(SumBookPrice);
        var bookDb = new BookDb();
        var num1 = bookDb.GetBookCountByPrice(callback);
        Console.WriteLine($"Num1:{num1}");

        // 方法二：
        var num2 = bookDb.GetBookCountByPrice(SumBookPrice);
        Console.WriteLine($"Num2:{num2}");
        // 方法三：匿名委派
        var num3 = bookDb.GetBookCountByPrice(delegate (Book book)
        {
            // 書價大於100的書籍數量
            if (book.Price > 100)
            {
                return 1;
            }
            return 0;
        });
        Console.WriteLine($"Num3:{num3}");
        // 方法四：lambda
        var num4 = bookDb.GetBookCountByPrice(x =>
        {
            if (x.Price > 100)
            {
                return 1;
            }
            return 0;
        });
        Console.WriteLine($"Num4:{num4}");

        bookDb.GetBookCount(x =>
        {
            Console.WriteLine(x.Author);
        });

        bookDb.SetNotify(x =>
        {
            Console.WriteLine("Action called");
            Console.WriteLine(x.Title);
        });
        
        bookDb.AddNewBook(new Book("title", "", 300, false));

        var num5 = bookDb.GetBookCount(x =>
        {
            return x.Price > 100;
        });
        Console.WriteLine($"Num5:{num5}");

        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "Abcd0330";
            builder.InitialCatalog = "Test";
            builder.TrustServerCertificate = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");

                connection.Open();

                string sql = "SELECT * FROM BOOKS";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4}", reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3), reader.GetBoolean(4));
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        Console.WriteLine("\nDone. Press enter.");
    }

    static int SumBookPrice(Book book)
    {
        if (book.Price > 100)
        {
            return 1;
        }
        return 0;
    }


    public record Model(int x)
    {
        private readonly int _x = x;
    }
    public static (int, string, int) Test()
    {
        return (100, "test", 10);
    }
}

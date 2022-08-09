using Microsoft.Data.SqlClient;
namespace AcessoDados
{
    public class Program
    {
        public static void Main(string[]args)
        {
           const string connectionString = "Server=TARGETN107\\SQLEXPRESS; Database=balta; User ID=sa; Password=123456; Encrypt=False; TrustServerCertificate=False;"; 
          
           using(var connection = new SqlConnection(connectionString))
           {
                connection.Open();
                System.Console.WriteLine("Conectado!");
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT [Id], [Title] FROM [Category]";
                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        System.Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                    }
                }               
           }  

        }
    }
}
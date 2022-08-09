using Microsoft.Data.SqlClient;
using System.Data;
namespace AdoNet
{
    class Program
    {
        public static void Main(string[]args)
        {
            const string strConnection = "Server=TARGETN107\\SQLEXPRESS; Database=balta; User ID=sa; Password=123456; Encrypt=False;";
            using(var connection = new SqlConnection(strConnection))
            {
                connection.Open();
                System.Console.WriteLine("Conectado...");
                using(var comando = new SqlCommand())
                {
                    comando.Connection = connection; 
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "SELECT TOP 10 * FROM vwListaCursosComCategoria";
                    var reader = comando.ExecuteReader(); 
                    int i = 0; 
                    while(reader.Read())
                    {
                        System.Console.WriteLine($"--------------- Curso {++i} ---------------");
                        System.Console.WriteLine($"ID: {reader.GetGuid(0)}");
                        System.Console.WriteLine($"Curso: {reader.GetString(1)}");
                        System.Console.WriteLine($"Categoria: {reader.GetString(2)}");
                        System.Console.WriteLine("---------------------------------------");
                    }
                    System.Console.WriteLine("Desconectado...");
                }
            }
        }
    }
}
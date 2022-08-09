using Microsoft.Data.SqlClient;
using Dapper;
using CursoAcessoDados.Models;

namespace AcessoDapper
{
    class Program 
    {
        public static void Main(string[]args)
        {
            const string strConnection = "Server=TARGETN107\\SQLEXPRESS; Database=balta; User ID=sa; Password=123456; Encrypt=False;";
                        
            var newCategory = new Category()
            {
                Id = Guid.NewGuid(),
                Title = "Amazon AWS",
                Url = "amazon",
                Description = "Categoria destinada a serviços do AWS", 
                Order = 8,
                Summary = "AWS Cloud",
                Featured = false
            };

            var insertSQL = @"
                                INSERT INTO [Category] 
                                VALUES
                                (
                                   @Id, 
                                   @Title,
                                   @Url,
                                   @Description,
                                   @Order,
                                   @Summary,
                                   @Featured     
                                )
                            ";
            
            
            using(var connection = new SqlConnection(strConnection))
            {
                var rowsAffected = connection.Execute(insertSQL, new 
                {
                    newCategory.Id,
                    newCategory.Title,
                    newCategory.Url,
                    newCategory.Description,
                    newCategory.Order,
                    newCategory.Summary,
                    newCategory.Featured
                });
                System.Console.WriteLine($"{rowsAffected} linhas inseridas.");

                var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");     
                foreach(var category in categories)
                {
                    System.Console.WriteLine($"{category.Id} - {category.Title}." );
                }  
            }
        }
    }
}
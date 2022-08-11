using Microsoft.Data.SqlClient;
using Dapper;
using CursoAcessoDados.Models;
using System.Data;

namespace AcessoDapper
{
    class Program 
    {
        public static void Main(string[]args)
        {
            const string strConnection = "Server=TARGETN107\\SQLEXPRESS; Database=balta; User ID=sa; Password=123456; Encrypt=False;";
                       
            using(var connection = new SqlConnection(strConnection))
            {
                // CreateCategory(connection);
                // CreateManyCategories(connection);
                // UpdateCategory(connection);
                // ListCategories(connection);  
                ExecuteProcedure(connection);              
            }
        }

        private static void CreateCategory(SqlConnection connection)
        {
            var newCategory = new Category()
            {
                Id = Guid.NewGuid(),
                Title = $"Curso Aleatório {Guid.NewGuid().ToString().Substring(0,5)}",
                Url = "aleatorio",
                Description = "Curso aleatorio destinado a programação", 
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

        }
    
        private static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");     
            foreach(var category in categories)
            {
                System.Console.WriteLine($"{category.Id} - {category.Title}." );
            }  
        }

        private static void UpdateCategory(SqlConnection connection)
        {
            var strUpdate = @"UPDATE [Category] SET [Title] = @newTitle WHERE [Id] = @id";
            var updatedRows = connection.Execute(strUpdate, new 
            {
                id = "03d21ec7-cfa6-4fbb-b560-368a473c3de8",
                newTitle = "Título da categoria alterado"        
            });
            System.Console.WriteLine($"{updatedRows} linhas alteradas. ");
        }
        
        private static void CreateManyCategories(SqlConnection connection)
        {
            var strInsert = @" INSERT INTO [CATEGORY] 
                               VALUES 
                               ( 
                                  @Id, 
                                  @Title, 
                                  @Url, 
                                  @Summary, 
                                  @Order, 
                                  @Description, 
                                  @Featured
                               )";

             var categoriaUm = new Category()
             {
                Id = Guid.NewGuid(),
                Title = "Many Category One",
                Description = "Example of create many categories",
                Url = "manycategories",
                Featured = false,
                Order = 1,
                Summary = "Many Categories"
             };

             var categoriaDois= new Category()
             {
                Id = Guid.NewGuid(),
                Title = "Many Category Two",
                Description = "Example of create many categories",
                Url = "manycategories",
                Featured = false,
                Order = 1,
                Summary = "Many Categories"
             };

             var rowsInserted = connection.Execute(strInsert, new[]
             {
                new 
                {
                  categoriaUm.Id,
                  categoriaUm.Title,
                  categoriaUm.Url,
                  categoriaUm.Summary,
                  categoriaUm.Order,
                  categoriaUm.Description,
                  categoriaUm.Featured
                },
                new 
                {
                  categoriaDois.Id,
                  categoriaDois.Title,
                  categoriaDois.Url,
                  categoriaDois.Summary,
                  categoriaDois.Order,
                  categoriaDois.Description,
                  categoriaDois.Featured
                }
             });
             System.Console.WriteLine($"{rowsInserted} linhas inseridas.");
        }

        private static void ExecuteProcedure(SqlConnection connection)
        {
            var strProcedure = "[spExcluiContaAluno]";
            var pars = new { StudentId = "7232054c-0fdd-4969-9524-b56e609e8e57"};  
            var rowsAffected = connection.Execute(strProcedure, pars, commandType: CommandType.StoredProcedure);
            System.Console.WriteLine($"{rowsAffected} registros excluídos.");
        }

    }
}
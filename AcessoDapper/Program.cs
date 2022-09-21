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
                // ExecuteProcedure(connection); 
                // ReadProcedureData(connection); 
                // ExecuteScalar(connection);     
                // ReadView(connection);
                // OneToOne(connection);   
                // OneToMany(connection);
                // QueryMultiple(connection);
                // SelectIN(connection);
                // SelectLIKE(connection);
                Transaction(connection);
                Console.ReadKey();   
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
                            
             var rowsAffected = connection.Execute(insertSQL, 
                new 
                {
                    newCategory.Id,
                    newCategory.Title,
                    newCategory.Url,
                    newCategory.Description,
                    newCategory.Order,
                    newCategory.Summary,
                    newCategory.Featured
                }
              );
                
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

        private static void ReadProcedureData(SqlConnection connection)
        {
            var procedure = "[spGetCoursesByCategory]";
            var parameter = new { CategoryId = "af3407aa-11ae-4621-a2ef-2028b85507c4" }; 
            var frontEndCourses = connection.Query
            (
              procedure,
              parameter,
              commandType: CommandType.StoredProcedure      
            ); 
            foreach(var course in frontEndCourses)
            {
                System.Console.WriteLine(course.Title);
            }
        }

        private static void ExecuteScalar(SqlConnection connection)
        {
            var strInsert = @"INSERT INTO [Author]
                            OUTPUT inserted.[Id]                            
                            VALUES
                            (NEWID(), @Name, @Title, @Image, @Bio, @Url, @Email, @Type)";

            var parametros = new 
                             {                               
                                Name = "Novo autor inserido", 
                                Title = "Microsoft MVP",
                                Image = "https://baltaio.blob.core.windows.net/static/images/authors/andrebaltieri.jpg",
                                Bio = "N/A", 
                                Url = "teste-insercao", 
                                Email = "andre@balta.io",
                                Type = 1
                             };

            var newAuthorID = connection.ExecuteScalar<Guid>(strInsert,parametros,commandType: CommandType.Text);

            System.Console.WriteLine($"Autor inserido: {newAuthorID}.");

        }
   
        private static void ReadView(SqlConnection connection)
        {
            var strSelect = @"SELECT * FROM [vwCourses]";
            var courses = connection.Query<Course>(strSelect);
            foreach(var course in courses)
            {
                System.Console.WriteLine($"{course.Title} ({course.DurationInMinutes} min).");
            }
        }
   
        private static void OneToOne(SqlConnection connection)
        {
            var sql = @" SELECT
                            *
                         FROM 
                            CareerItem CI 
                         INNER JOIN 
                            Course C
                         ON CI.CourseId = C.Id";
               
            var items = connection.Query<CarrerItem, Course, CarrerItem>
                        (sql, (carrerItem, course) => {
                            carrerItem.Course = course;
                            return carrerItem;
                        }, splitOn: "Id"); 


            foreach(var item in items)
            {
                System.Console.WriteLine($"Carreira: {item.Title} - Curso: {item.Course.Title}.");                
            }
        }
    
        private static void OneToMany(SqlConnection connection)
        {
            var sql = @"
                        SELECT
                            C.Id,
                            C.Title,
                            CI.CareerId,
                            CI.Title
                        FROM 
                            Career C
                        INNER JOIN 
                            CareerItem CI ON CI.CareerId = C.Id
                        ORDER BY 
                            C.Title
                      ";    
            var careers = new List<Carrer>();
            var items = connection.Query<Carrer, CarrerItem, Carrer>
                        (sql, (carrer, carrerItem) => 
                        {
                            var careerExists = careers.Where(x => x.Id  == carrer.Id).FirstOrDefault();
                            if(careerExists == null)
                            {               
                                careerExists = carrer;                  
                                careerExists.Items.Add(carrerItem);
                                careers.Add(careerExists);     
                            }
                            else
                            {
                                careerExists.Items.Add(carrerItem);    
                            }
                            return carrer;
                        }, splitOn: "CareerId");

            foreach(var career in careers)
            {
                System.Console.WriteLine($"Carreira: {career.Title}");
                foreach(var item in career.Items)
                {
                    System.Console.WriteLine($"  - Item: {item.Title}");
                }
            }

        }
    
        private static void QueryMultiple(SqlConnection connection)
        {
            var sql = "SELECT * FROM [Category]; SELECT * FROM [Course]";
            using(var multi = connection.QueryMultiple(sql))
            {
                var categories = multi.Read<Category>(); 
                var courses = multi.Read<Course>();

                System.Console.WriteLine("Categorias.....................:");
                foreach(var category in categories)
                {
                    System.Console.WriteLine($"Categoria: {category.Title}");
                }
                System.Console.WriteLine("Cursos.........................:");
                foreach(var course in courses)
                {
                    System.Console.WriteLine($"Curso: {course.Title}");
                }
            }
        }
    
        private static void SelectIN(SqlConnection connection)
        {
            var query = "SELECT * FROM Career WHERE [Id] IN @Id";
            
            var items = connection.Query<Carrer>(query, new
            {
                 Id = new []
                 {
                    "01AE8A85-B4E8-4194-A0F1-1C6190AF54CB",
                    "E6730D1C-6870-4DF3-AE68-438624E04C72"
                 }
            });
            
            foreach(var item in items)
            {
                System.Console.WriteLine(item.Title);
            }

        }
    
        private static void SelectLIKE(SqlConnection connection)
        {

            var termo = "git";

            var query = "SELECT * FROM [Course] WHERE Title LIKE @Title"; 
            
            var courses = connection.Query<Course>(query, new 
            {
                Title = $"%{termo}%"
            });

            foreach(var curso in courses)
            {
                System.Console.WriteLine(curso.Title);
            }
        }

        private static void Transaction(SqlConnection connection)
        {
            var newCategory = new Category()
            {
                Id = Guid.NewGuid(),
                Title = $"Não quero gravar {Guid.NewGuid().ToString().Substring(0,5)}",
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

             connection.Open();         
             using(var transaction = connection.BeginTransaction())
             {
                var rowsAffected = connection.Execute(insertSQL, 
                new 
                {
                    newCategory.Id,
                    newCategory.Title,
                    newCategory.Url,
                    newCategory.Description,
                    newCategory.Order,
                    newCategory.Summary,
                    newCategory.Featured
                }, transaction);
              transaction.Rollback();
              //transaction.Commit();
              System.Console.WriteLine($"{rowsAffected} linhas inseridas.");
             }
                
            
        }
    }
}
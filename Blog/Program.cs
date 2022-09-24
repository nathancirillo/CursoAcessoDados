using Blog.Models;
using CursoAcessoDados.Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace CursoAcessoDados.Blog
{       public class Program
        {           
            public static void Main(string []args)
            {      
                    var strConnection = $"Server=TARGETN107\\SQLEXPRESS;Database=BlogDoBalta;User ID=sa;Password=123456;Encrypt=False;";

                    var conn = new SqlConnection(strConnection);      

                    conn.Open();
                    
                    CreateUser(conn);

                    ReadUsers(conn);

                    ReadUsersWithRoles(conn);
                 
                    conn.Close();
                    
                    Console.ReadKey();
                
            }

            private static void CreateUser(SqlConnection conn)
            {
                var repository = new Repository<User>(conn);
                var newUser = new User()
                {
                    Name = "Vanessa Fontes Cirillo",
                    Bio = "Bio da Vanessa",
                    Email = $"vanfontes{Guid.NewGuid().ToString().Substring(0,4)}@hotmail.com",
                    Image = "vanessa.png",
                    PasswordHash = "password",
                    Slug = $"vanessa-nutricao {Guid.NewGuid().ToString().Substring(0,4)}"
                };            
                repository.Create(newUser);
                Console.WriteLine($"Usuário {newUser.Name} cadastrado com sucesso.");
            } 

            private static void ReadUser(SqlConnection conn)
            {
                var repository = new Repository<User>(conn);
                var user = repository.Get(1);
                System.Console.WriteLine($"Usuário: " + user.Name);
            }

            private static void ReadUsers(SqlConnection conn)
            {
                var repository = new Repository<User>(conn);    
                var users = repository.Get();          
                foreach (var user in users)                           
                    System.Console.WriteLine($"User: {user.Name}");                          
            }

            private static void ReadUsersWithRoles(SqlConnection conn)
            {
                var repository = new UserRepository(conn);    
                var users = repository.GetWithRoles();          
                foreach (var user in users) 
                {              
                    System.Console.WriteLine($"User: {user.Name}");   
                    foreach(var role in user.Roles)                    
                        System.Console.WriteLine($"User Role: {role.Name}");                    
                }            
            }

            
           
            private static void UpdateUser(SqlConnection conn)
            {
                var repository = new Repository<User>(conn);
                var user = new User { Id = 1, Name = "Paulo Almeida" };
                repository.Update(user);
            }

            private static void DeleteUser(SqlConnection conn)
            {
                var repository = new Repository<User>(conn);

                var userToBeDeleted = new User()
                {
                    Id = 2
                };

                repository.Delete(userToBeDeleted);

                System.Console.WriteLine($"Usuário apagado com sucesso...");
             }

            private static void ReadRole(SqlConnection conn)
            {
                var repository = new Repository<Role>(conn);
                var role = repository.Get(1);
                System.Console.WriteLine($"{role.Id} - {role.Name} - {role.Slug}");
            }

            private static void ReadRoles(SqlConnection conn)
            {
                var repository = new Repository<Role>(conn);
                var roles = repository.Get();
                foreach (var role in roles)
                    System.Console.WriteLine($"{role.Id} - {role.Name} - {role.Slug}"); 
            }
         
            private static void UpdateRole(SqlConnection conn)
            {
                var repository = new Repository<Role>(conn);

                var role = new Role { Id = 1, Name="Autor", Slug="autor" };

                repository.Update(role);
            }
            
            private static void CreateTag(SqlConnection conn)
            {
                var repository = new Repository<Tag>(conn);
                
                var newTag = new Tag 
                {
                    Name = "Struct Query Language",
                    Slug = $"SQL BD Programming {Guid.NewGuid().ToString().Substring(0,4)}"
                };

                repository.Create(newTag);

                System.Console.WriteLine($"Tag {newTag.Name} criada!");

            }

            private static void ReadTags(SqlConnection conn)
            {
                var repository = new Repository<Tag>(conn);
                var tags = repository.Get();
                foreach(var tag in tags)                
                    System.Console.WriteLine($"{tag.Id} | {tag.Name} | {tag.Slug}");                
            }

            private static void UpdateTag(SqlConnection conn)
            {
                var repository = new Repository<Tag>(conn);
                
                var updatedTag = new Tag {Id = 2, Name = "SQL Updated", Slug = "SQL Updated"};

                var wasUpdated = repository.Update(updatedTag);

                System.Console.WriteLine(wasUpdated ? "Tag atualizada!" : "Tag não encontrada!");

            }

            private static void DeleteTag(SqlConnection conn)
            {
                var repository = new Repository<Tag>(conn);
                var tagDeleted = repository.Delete(2); 
                System.Console.WriteLine(tagDeleted ? "Tag excluída!" : "Tag não encontrada!");                
            }
               
        }
    
}
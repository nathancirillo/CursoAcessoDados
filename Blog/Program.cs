using Blog.Models;
using CursoAcessoDados.Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace CursoAcessoDados.Blog
{       public class Program
        {           
            public static void Main(string []args)
            {                   
                    ReadUsers(); 
                    // ReadUser(conn);
                    // CreateUser(conn);
                    // UpdateUser(conn);
                    // DeleteUser(conn);
                    Console.ReadKey();
                
            }

            private static void ReadUsers()
            {
                var repository = new UserRepository();    
                var users = repository.Get();          
                foreach (var user in users)
                {
                    System.Console.WriteLine(user.Name);
                }
            }

            private static void ReadUser(SqlConnection conn)
            {
                var user = conn.Get<User>(1);
                System.Console.WriteLine($"Somente um usuário: " + user.Name);
            }

            private static void CreateUser(SqlConnection conn)
            {
                var newUser = new User()
                {
                    Name = "Vanessa Fontes Cirillo",
                    Bio = "Bio da Vanessa",
                    Email = "vanfontes2010@hotmail.com",
                    Image = "vanessa.png",
                    PasswordHash = "password",
                    Slug = "vanessa-nutricao"
                };
                conn.Insert<User>(newUser);
                System.Console.WriteLine($"Usuário {newUser.Name} cadastrado com sucesso.");
            }
        
            private static void UpdateUser(SqlConnection conn)
            {
                // no caso do update necessário passar o id da entidade
                var userToBeUpdated = new User()
                {
                    Id = 2,
                    Name = "Vanessa Cirillo",
                    Bio = "Bio da Vanessa",
                    Email = "vanfontes2010@hotmail.com",
                    Image = "vanessa.png",
                    PasswordHash = "password",
                    Slug = "vanessa-nutricao"
                };
                conn.Update<User>(userToBeUpdated);
                System.Console.WriteLine($"Usuário atualizado com sucesso...");
             }

            private static void DeleteUser(SqlConnection conn)
            {
                // no caso do delete necessário passar o id da entidade
                var userToBeDeleted = new User()
                {
                    Id = 2
                };
                conn.Delete<User>(userToBeDeleted);
                System.Console.WriteLine($"Usuário apagado com sucesso...");
             }
        
        
        }
    
}
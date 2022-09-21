using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace CursoAcessoDados.Blog.Repositories
{
    public class UserRepository
    {
          private SqlConnection _connection;

          public UserRepository()
          {
             _connection = new SqlConnection("Server=TARGETN107\\SQLEXPRESS; Database=BlogDoBalta; User ID=sa; Password=123456; Encrypt=False;");
          }

          public IEnumerable<User> Get()                      
            => _connection.GetAll<User>();           
          
          public User Get(int id)                  
            => _connection.Get<User>(id);           
          
          public void Insert(User user)                   
            => _connection.Insert<User>(user);
    }
}
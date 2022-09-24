using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace CursoAcessoDados.Blog.Repositories
{
    public class Repository<TModel> where TModel : class
    {
        private readonly SqlConnection _connection;

        public Repository(SqlConnection connection)
        => _connection = connection;

        public void Create(TModel model)
        => _connection.Insert<TModel>(model);

        public IEnumerable<TModel> Get()
        => _connection.GetAll<TModel>();

        public TModel Get(int id)
        => _connection.Get<TModel>(id);   

        public bool Update(TModel model)
        => _connection.Update<TModel>(model);
       
        public void Delete(TModel model)
        => _connection.Delete<TModel>(model);

        public bool Delete(int id)
        {
            var obj = _connection.Get<TModel>(id);
            return _connection.Delete<TModel>(obj);
        }
        

    }
}
using System.Data.Common;
using Npgsql;

namespace Infrastructure.DataContext;

public class Context:IContext
{
    readonly string connectionString=
        "Server=localhost; Port = 5432; Database = SimpleShop; User Id = postgres; Password = 832111;";
    

    public DbConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}

public interface IContext
{
    public DbConnection Connection();
}
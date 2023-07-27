using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HelloWorld.Data;

public class DataContextDapper
{
    private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true";

    // IDbConnection is from System.Data
    // SqlConnection is from Microsoft.Data.SqlClient

    public IEnumerable<T> LoadData<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_connectionString);

        return dbConnection.Query<T>(sql);
    }
    
    public T LoadDataSingle<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_connectionString);

        return dbConnection.QuerySingle<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
        int rowCount = ExecuteSqlWithRowCount(sql);

        return rowCount > 0;
    }
    
    public int ExecuteSqlWithRowCount(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_connectionString);

        return dbConnection.Execute(sql);
    }
}
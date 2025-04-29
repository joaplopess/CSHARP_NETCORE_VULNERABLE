using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data;

public class SqlInjectionController : Controller
{
    private readonly string _connectionString;

    public SqlInjectionController(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public IActionResult Login(string username, string password)
    {
        // Vulnerable SQL query - direct concatenation
        var query = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
        
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            var reader = command.ExecuteReader();
            
            if (reader.HasRows)
            {
                return View("LoginSuccess");
            }
            return View("LoginFailed");
        }
    }

    public IActionResult Search(string term)
    {
        var results = new List<string>();
        var query = $"SELECT * FROM Products WHERE Name LIKE '%{term}%'";
        
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                results.Add(reader["Name"].ToString());
            }
        }
        
        return View("SearchResults", results);
    }
}
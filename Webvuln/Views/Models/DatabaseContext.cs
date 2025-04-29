namespace Webvuln.Models;
using Microsoft.Data.Sqlite;

public class DatabaseContext : IDisposable
{
    private readonly SqliteConnection _connection;

    public DatabaseContext(IConfiguration config)
    {
        _connection = new SqliteConnection(config.GetConnectionString("DefaultConnection"));
        _connection.Open();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        var command = _connection.CreateCommand();

        // Enable foreign key support
        command.CommandText = "PRAGMA foreign_keys = ON;";
        command.ExecuteNonQuery();

        // Create tables if they don't exist
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Email TEXT
            );

            CREATE TABLE IF NOT EXISTS Products (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Price REAL NOT NULL,
                Description TEXT
            );
        ";
        command.ExecuteNonQuery();

        // Insert sample data if tables are empty
        command.CommandText = "SELECT COUNT(*) FROM Users;";
        var userCount = Convert.ToInt64(command.ExecuteScalar());

        if (userCount == 0)
        {
            command.CommandText = @"
                INSERT INTO Users (Username, Password, Email) VALUES 
                ('admin', 'password123', 'admin@example.com'),
                ('user1', 'letmein', 'user1@example.com');
                
                INSERT INTO Products (Name, Price, Description) VALUES
                ('Product 1', 19.99, 'First product'),
                ('Product 2', 29.99, 'Second product');
            ";
            command.ExecuteNonQuery();
        }
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
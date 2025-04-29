using Microsoft.Data.Sqlite;

public class DatabaseContext
{
    private readonly string _connectionString;

    public DatabaseContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT,
                    Password TEXT,
                    Email TEXT
                );
                
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Price REAL,
                    Description TEXT
                );
                
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
}
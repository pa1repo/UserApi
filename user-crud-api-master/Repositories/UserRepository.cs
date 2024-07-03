namespace WebApi.Repositories;

using Dapper;
using WebApi.Entities;
using WebApi.Helpers;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(int id);
    Task<User> GetByEmail(string email);
    Task<User> GetByUserName(string username);
    Task Create(User user);
    Task Update(User user);
    Task Delete(int id);
}

public class UserRepository : IUserRepository
{
    private DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Users
        """;
        return await connection.QueryAsync<User>(sql);
    }

    public async Task<User> GetById(int user_id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Users 
            WHERE user_id = @user_id
        """;
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { user_id });
    }

    public async Task<User> GetByEmail(string email)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Users 
            WHERE Email = @email
        """;
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { email });
    }

    public async Task<User> GetByUserName(string username)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Users 
            WHERE user_name = @username
        """;
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { username });
    }

    public async Task Create(User user)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO Users (user_name, first_name, last_name, email, user_status, department)
            VALUES (@user_name, @first_name, @last_name, @email, @user_status, @department)
        """;
        await connection.ExecuteAsync(sql, user);
    }

    public async Task Update(User user)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            UPDATE Users 
            SET user_name = @user_name,
                first_name = @first_name,
                last_name = @last_name, 
                email = @email, 
                user_status = @user_status, 
                department = @department
            WHERE user_id = @user_id
        """;
        await connection.ExecuteAsync(sql, user);
    }

    public async Task Delete(int user_id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            DELETE FROM Users 
            WHERE user_id = @user_id
        """;
        await connection.ExecuteAsync(sql, new { user_id });
    }
}
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Database;

public class RoleAccessManager(ConnectionConfig config)
{
    private readonly Connection _connectionHandler = new(config);
    private const string DefaultPassword = "defaultpassword";

    public async Task<ConnectionConfig> GetAccessConfig(string username)
    {
        var userExists = await IsUserExists(username);
        if (!userExists) throw new Exception($"User {username} does not exist.");
        
        config.User = username;
        config.Password = DefaultPassword;
        return config;
    }
    
    public async Task<bool> IsUserExists(string username)
    {
        const string query = $"SELECT 1 FROM pg_roles WHERE rolname = @rolename";

        await _connectionHandler.OpenConnectionAsync();
        try
        {
            var parameters = new Dictionary<string, object>
            {
                { "@rolename", username }
            };
            var result = await _connectionHandler.RawExecuteQueryAsync(query, parameters);
            return result.Rows.Count > 0;
        }
        catch (Exception e)
        {
            throw new Exception($"Error checking user existence: {e.Message}", e);
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }

    public async Task CreateUser(string username)
    {
        var query = $"CREATE ROLE {username} LOGIN PASSWORD '{DefaultPassword}'";

        await _connectionHandler.OpenConnectionAsync();
        try
        {
            await _connectionHandler.BeginTransactionAsync();
            var parameters = new Dictionary<string, object>
            {
            };
            await _connectionHandler.RawExecuteNonQueryAsync(query, parameters);
            await _connectionHandler.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _connectionHandler.RollbackTransactionAsync();
            throw new Exception($"Error creating user: {e.Message}", e);
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }

    public async Task DropUser(string username)
    {
        var query = new List<string>
        {
            $"GRANT {username} TO postgres",
            $"REASSIGN OWNED BY {username} TO postgres",
            $"DROP OWNED BY {username}",
            $"DROP ROLE IF EXISTS {username}",
        };

        await _connectionHandler.OpenConnectionAsync();
        try
        {
            await _connectionHandler.BeginTransactionAsync();
            var parameters = new Dictionary<string, object>
            { };
            foreach (var q in query)
            {
                await _connectionHandler.RawExecuteNonQueryAsync(q, parameters);
            }
            await _connectionHandler.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _connectionHandler.RollbackTransactionAsync();
            throw new Exception($"Error dropping user: {e.Message}", e);
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }

    public async Task GrantAccess(string username, List<AccessTypes> roles, List<string> tables)
    {
        await _connectionHandler.OpenConnectionAsync();
        try
        {
            await _connectionHandler.BeginTransactionAsync();

            foreach (var query in tables.SelectMany(table => roles, (table, role) => role switch
                     {
                         AccessTypes.Read => $"GRANT SELECT ON \"{table}\" TO {username}",
                         AccessTypes.Write => $"GRANT INSERT, UPDATE ON \"{table}\" TO {username}",
                         AccessTypes.Delete => $"GRANT DELETE ON \"{table}\" TO {username}",
                         AccessTypes.All => $"GRANT ALL PRIVILEGES ON \"{table}\" TO {username}",
                         _ => null!
                     }).Where(query => !string.IsNullOrEmpty(query)))
            {
                await _connectionHandler.RawExecuteNonQueryAsync(query, new Dictionary<string, object>());
            }

            await _connectionHandler.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _connectionHandler.RollbackTransactionAsync();
            throw new Exception($"Error granting access: {e.Message}", e);
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }
    
    public async Task RemoveAccess(string username, List<AccessTypes> roles, List<string> tables)
    {
        await _connectionHandler.OpenConnectionAsync();
        try
        {
            await _connectionHandler.BeginTransactionAsync();

            foreach (var query in tables.SelectMany(table => roles, (table, role) => role switch
                     {
                         AccessTypes.Read => $"REVOKE SELECT ON \"{table}\" FROM {username}",
                         AccessTypes.Write => $"REVOKE INSERT, UPDATE ON \"{table}\" FROM {username}",
                         AccessTypes.Delete => $"REVOKE DELETE ON \"{table}\" FROM {username}",
                         AccessTypes.All => $"REVOKE ALL PRIVILEGES ON \"{table}\" FROM {username}",
                         _ => null!
                     }).Where(query => !string.IsNullOrEmpty(query)))
            {
                await _connectionHandler.RawExecuteNonQueryAsync(query, new Dictionary<string, object>());
            }

            await _connectionHandler.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _connectionHandler.RollbackTransactionAsync();
            throw new Exception($"Error removing access: {e.Message}", e);
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }
}
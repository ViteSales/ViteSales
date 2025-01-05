using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Services;

public class RoleAccessService : IRoleAccessManager
{
    private readonly ConnectionConfig _config;
    private readonly Connection _connectionHandler;
    private const string DefaultPassword = "defaultpassword";
    private readonly ILogger<RoleAccessService> _logger;

    public RoleAccessService(IOptions<ConnectionConfig> cfg, ILogger<RoleAccessService> log)
    {
        ArgumentNullException.ThrowIfNull(cfg.Value);
        ArgumentNullException.ThrowIfNull(log);

        _logger = log;
        _connectionHandler = new Connection(cfg.Value);
        _config = cfg.Value;
    }

    public async Task<ConnectionConfig> GetAccessConfig(string username)
    {
        _logger.LogDebug("Getting access configuration for user {Username}", username);
        var userExists = await IsUserExists(username);
        if (!userExists)
        {
            _logger.LogWarning("User {Username} does not exist.", username);
            throw new Exception($"User {username} does not exist.");
        }

        _config.User = username;
        _config.Password = DefaultPassword;
        _logger.LogDebug("Access configuration for user {Username} retrieved successfully.", username);
        return _config;
    }

    public async Task<bool> IsUserExists(string username)
    {
        _logger.LogDebug("Checking if user {Username} exists.", username);
        const string query = "SELECT 1 FROM pg_roles WHERE rolname = @rolename";

        await _connectionHandler.OpenConnectionAsync();
        try
        {
            var parameters = new Dictionary<string, object>
            {
                { "@rolename", username }
            };
            var result = await _connectionHandler.RawExecuteQueryAsync(query, parameters);
            var exists = result.Rows.Count > 0;
            _logger.LogInformation("User {Username} existence check completed. Exists: {Exists}", username, exists);
            return exists;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error checking user existence for {Username}.", username);
            throw new Exception($"Error checking user existence: {e.Message}", e);
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }

    public async Task CreateUser(string username)
    {
        _logger.LogDebug("Creating user {Username}.", username);
        var query = $"CREATE ROLE {username} LOGIN PASSWORD '{DefaultPassword}'";

        await _connectionHandler.OpenConnectionAsync();
        try
        {
            await _connectionHandler.BeginTransactionAsync();
            var parameters = new Dictionary<string, object> { };
            await _connectionHandler.RawExecuteNonQueryAsync(query, parameters);
            await _connectionHandler.CommitTransactionAsync();
            _logger.LogInformation("User {Username} created successfully.", username);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating user {Username}.", username);
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
        _logger.LogDebug("Dropping user {Username}.", username);
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
            var parameters = new Dictionary<string, object> { };
            foreach (var q in query)
            {
                _logger.LogDebug("Executing query: {Query}", q);
                await _connectionHandler.RawExecuteNonQueryAsync(q, parameters);
            }
            await _connectionHandler.CommitTransactionAsync();
            _logger.LogInformation("User {Username} dropped successfully.", username);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error dropping user {Username}.", username);
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
        _logger.LogDebug("Granting access to user {Username}. Roles: {Roles}, Tables: {Tables}", username, roles, tables);
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
                _logger.LogDebug("Executing query: {Query}", query);
                await _connectionHandler.RawExecuteNonQueryAsync(query, new Dictionary<string, object>());
            }

            await _connectionHandler.CommitTransactionAsync();
            _logger.LogInformation("Access granted to user {Username}.", username);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error granting access to user {Username}.", username);
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
        _logger.LogDebug("Removing access from user {Username}. Roles: {Roles}, Tables: {Tables}", username, roles, tables);
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
                _logger.LogDebug("Executing query: {Query}", query);
                await _connectionHandler.RawExecuteNonQueryAsync(query, new Dictionary<string, object>());
            }

            await _connectionHandler.CommitTransactionAsync();
            _logger.LogInformation("Access removed from user {Username}.", username);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error removing access from user {Username}.", username);
            await _connectionHandler.RollbackTransactionAsync();
            throw new Exception($"Error removing access: {e.Message}", e);
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }
}
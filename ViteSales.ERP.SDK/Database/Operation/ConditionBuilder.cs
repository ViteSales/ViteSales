using NpgsqlTypes;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Utils;

namespace ViteSales.ERP.SDK.Database.Operation;

public class ConditionBuilder
{
    private readonly List<string> _conditions = new();
    private readonly Dictionary<string, WhereClause> _parameters = new();
    /// <summary>
    /// Escape column name to follow PostgreSQL syntax (wraps column name in double quotes).
    /// </summary>
    private string EscapeIdentifier(string column)
    {
        return $"\"{column}\"";
    }

    /// <summary>
    /// Adds an "AND" clause.
    /// Example: "Age" > @Age
    /// </summary>
    public ConditionBuilder And(string column, string operation, object value)
    {
        if (!string.IsNullOrWhiteSpace(column) && !string.IsNullOrWhiteSpace(operation))
        {
            var param = $"{column}_{Utility.GetUniqueId()}";
            var condition = $"{EscapeIdentifier(column)} {operation} @{param}";
            
            _parameters.Add(param, new WhereClause()
            {
                DbType = value.ToNpgsqlDbType(),
                Value = value
            });
            
            if (_conditions.Count > 0)
            {
                _conditions.Add("AND");
            }

            _conditions.Add(condition);
        }
        return this;
    }

    /// <summary>
    /// Adds an "OR" clause.
    /// Example: "Age" > @Age
    /// </summary>
    public ConditionBuilder Or(string column, string operation, object value)
    {
        if (!string.IsNullOrWhiteSpace(column) && !string.IsNullOrWhiteSpace(operation))
        {
            var param = $"{column}_{Utility.GetUniqueId()}";
            var condition = $"{EscapeIdentifier(column)} {operation} @{param}";
            
            _parameters.Add(param, new WhereClause()
            {
                DbType = value.ToNpgsqlDbType(),
                Value = value
            });
            
            if (_conditions.Count > 0)
            {
                _conditions.Add("OR");
            }

            _conditions.Add(condition);
        }
        return this;
    }

    /// <summary>
    /// Adds a grouped set of conditions.
    /// Example: ( "Age" > @Age AND "Salary" < @Salary )
    /// </summary>
    public ConditionBuilder Group(Action<ConditionBuilder> nestedGroup)
    {
        var groupBuilder = new ConditionBuilder();
        nestedGroup.Invoke(groupBuilder);

        var (groupConditions,_) = groupBuilder.Build();
        if (!string.IsNullOrWhiteSpace(groupConditions))
        {
            if (_conditions.Count > 0)
            {
                _conditions.Add("AND");
            }

            _conditions.Add($"({groupConditions})");
        }
        return this;
    }

    /// <summary>
    /// Adds a condition for filtering JSON fields.
    /// Example: JSON field query: "Data"->'key' = @Value
    /// </summary>
    public ConditionBuilder JsonClause(string jsonColumn, string jsonKey, string operation, object value)
    {
        if (!string.IsNullOrWhiteSpace(jsonColumn) && !string.IsNullOrWhiteSpace(jsonKey) && !string.IsNullOrWhiteSpace(operation))
        {
            var param = $"{jsonColumn}_{jsonKey}_{Utility.GetUniqueId()}";
            _parameters.Add(param, new WhereClause()
            {
                DbType = value.ToNpgsqlDbType(),
                Value = value
            });
            var condition = $"{EscapeIdentifier(jsonColumn)}->{EscapeIdentifier(jsonKey)} {operation} @{param}";
            if (_conditions.Count > 0)
            {
                _conditions.Add("AND");
            }

            _conditions.Add(condition);
        }
        return this;
    }

    /// <summary>
    /// Adds a condition for filtering JSON object values.
    /// Example: JSON object value query: "Data"->>'key' = @Value
    /// </summary>
    public ConditionBuilder JsonValueClause(string jsonColumn, string jsonKey, string operation, object value)
    {
        if (!string.IsNullOrWhiteSpace(jsonColumn) && !string.IsNullOrWhiteSpace(jsonKey) && !string.IsNullOrWhiteSpace(operation))
        {
            var param = $"{jsonColumn}_{jsonKey}_{Utility.GetUniqueId()}";
            _parameters.Add(param, new WhereClause()
            {
                DbType = value.ToNpgsqlDbType(),
                Value = value
            });
            var condition = $"{EscapeIdentifier(jsonColumn)}->>'{jsonKey}' {operation} @{param}";
            if (_conditions.Count > 0)
            {
                _conditions.Add("AND");
            }

            _conditions.Add(condition);
        }
        return this;
    }

    /// <summary>
    /// Compiles and returns the WHERE clause as a string.
    /// </summary>
    public (string, Dictionary<string, WhereClause>) Build()
    {
        return (string.Join(" ", _conditions), _parameters);
    }
}

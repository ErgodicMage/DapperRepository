using System.Data.Common;
using System.Reflection;

namespace ErgodicMage.DapperRepository;

internal static class Builders
{
    public static string? BuildFullTableName(TableMapper table)
    {
        if (table is null) return null;
        if (string.IsNullOrEmpty(table.Schema) && string.IsNullOrEmpty(table.Alias))
            return table.TableName;
        else
        {
            StringBuilder sb = new();
            if (!string.IsNullOrEmpty(table.Schema))
            {
                sb.Append(table.Schema);
                sb.Append('.');
            }
            sb.Append(table.TableName);
            if (!string.IsNullOrEmpty(table.Alias))
            {
                sb.Append(" AS ");
                sb.Append(table.Alias);
            }
            return sb.ToString();
        }
    }

    public static string? BuildSelectColumns(TableMapper table, IList<ColumnMapper> columns)
    {
        if (table is null || columns is null) return null;

        StringBuilder sb = new();

        var selectColumns = columns.Where(c => (c.Attributes & ColumnAttributes.NotMapped) != ColumnAttributes.NotMapped && 
                                                (c.Attributes & ColumnAttributes.IgnoreSelect) != ColumnAttributes.IgnoreSelect);
        bool first = true;

        foreach (ColumnMapper column in selectColumns)
        {
            if (!first) sb.Append(',');
            BuildSelectColumn(sb, column, table);
            first = false;
        }

        return sb.ToString();
    }

    private static void BuildSelectColumn(StringBuilder sb, ColumnMapper column, TableMapper table)
    {
        HandleAlias(sb, table);

        if (!string.IsNullOrEmpty(column.ColumnName))
        {
            sb.Append(column.ColumnName);
            sb.Append(" AS ");
        }

        sb.Append(column.ClassName);
    }

    public static string? BuildWhereId(TableMapper table, IList<ColumnMapper> columns)
    {
        if (columns is null) return null;

        var idColumns = columns.Where(c => (c.Attributes & ColumnAttributes.Key) == ColumnAttributes.Key);
        if (!idColumns.Any()) return null;

        StringBuilder sb = new();

        bool first = true;
        foreach (var column in idColumns)
        {
            if (!first) sb.Append(" AND ");

            BuildIdEqual(sb, table, column);

            first = false;
        }

        return sb.ToString();
    }

    private static void BuildIdEqual(StringBuilder sb, TableMapper table, ColumnMapper column)
    {
        HandleAlias(sb, table);

        if (!string.IsNullOrEmpty(column.ColumnName))
            sb.Append(column.ColumnName);
        else
            sb.Append(column.ClassName);
        sb.Append('=');
        HandleAtToColumnName(sb, column);
    }

    public static string? BuildInsertStatement(TableMapper table, IList<ColumnMapper> columns)
    {
        if (columns is null) return null;

        StringBuilder sb = new();

        sb.Append("INSERT INTO ");
        sb.Append(BuildFullTableName(table));
        sb.Append(" (");
        BuildInsertColumns(sb, table, columns);
        sb.Append(") VALUES (");
        BuildInsertValues(sb, table, columns);
        sb.Append(')');

        return sb.ToString();
    }

    public static IList<Where>? BuildWhere(object? whereConditions, IList<ColumnMapper> columns)
    {
        if (whereConditions is null) return null;

        PropertyInfo[] properties = whereConditions.GetType().GetProperties();
        if (properties is null || properties.Length == 0) return null;

        List<Where> allWheres = new();

        bool first = true;
        foreach (PropertyInfo property in properties)
        {
            var foundColumn = columns.Where(c => c.ClassName == property.Name).FirstOrDefault() ?? 
                              columns.Where(c => c.ColumnName == property.Name).FirstOrDefault();
            if (foundColumn is null) continue;

            Where where = first ? new(foundColumn, WhereOperator.Equals) : new(WhereAndOrNot.And, foundColumn, WhereOperator.Equals);
            allWheres.Add(where);

            first = false;
        }

        return allWheres;
    }

    private static void BuildInsertColumns(StringBuilder sb, TableMapper table, IList<ColumnMapper> columns)
    {
        if (table is null || columns is null) return;

        bool first = true;
        foreach (var column in columns.Where(c => CanInsertColumn(c)))
        {
            if (!first) sb.Append(", ");
            HandleAlias(sb, table);
            if (string.IsNullOrEmpty(column.ColumnName))
                sb.Append(column.ClassName);
            else
                sb.Append(column.ColumnName);
            first = false;
        }
    }

    public static void BuildInsertValues(StringBuilder sb, TableMapper table, IList<ColumnMapper> columns)
    {
        if (table is null || columns is null) return;

        bool first = true;
        foreach (var column in columns.Where(c => CanInsertColumn(c)))
        {
            if (!first) sb.Append(", ");
            HandleAtToColumnName(sb, column);
            first = false;
        }
    }

    public static void HandleAlias(StringBuilder sb, TableMapper table)
    {
        if (string.IsNullOrEmpty(table.Alias)) return;
        sb.Append(table.Alias);
        sb.Append('.');
    }

    public static void HandleAtToColumnName(StringBuilder sb, ColumnMapper column)
    {
        sb.Append('@');
        sb.Append(column.ClassName);
    }

    public static bool CanInsertColumn(ColumnMapper column)
    {
        if ((column.Attributes & ColumnAttributes.Required) == ColumnAttributes.Required) return true;

        if ((column.Attributes & ColumnAttributes.Key) == ColumnAttributes.Key) return false;
        if ((column.Attributes & ColumnAttributes.ReadOnly) == ColumnAttributes.ReadOnly) return false;
        if ((column.Attributes & ColumnAttributes.IgnoreInsert) == ColumnAttributes.IgnoreInsert) return false;
        if ((column.Attributes & ColumnAttributes.NotMapped) == ColumnAttributes.NotMapped) return false;

        return true;
    }
}

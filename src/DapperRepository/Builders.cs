﻿using System.ComponentModel;

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
            if (string.IsNullOrEmpty(table.Schema))
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

        var selectColumns = columns.Where(c => (c.Attributes & ColumnAttributes.NotMapped) != 0 || 
                                                (c.Attributes & ColumnAttributes.IgnoreSelect) != 0);
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

        var idColumns = columns.Where(c => (c.Attributes & ColumnAttributes.Key) != 0);
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
        sb.Append(") ");
        BuildInsertColumns(sb, table, columns);
        sb.Append(" VALUES (");
        sb.Append(')');

        return sb.ToString();
    }

    private static void BuildInsertColumns(StringBuilder sb, TableMapper table, IList<ColumnMapper> columns)
    {
        if (table is null || columns is null) return;

        var insertColumns = columns.Where(c => CanInsertColumn(c));

        bool first = true;
        foreach (var column in columns)
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

    private static void BuildInsertValues(StringBuilder sb, TableMapper table, IList<ColumnMapper> columns)
    {
        if (table is null || columns is null) return;

        var insertColumns = columns.Where(c => CanInsertColumn(c));

        bool first = true;
        foreach (var column in columns)
        {
            if (!first) sb.Append(", ");
            HandleAtToColumnName(sb, column);
            sb.Append(column.ClassName);
            first = false;
        }
    }

    private static void HandleAlias(StringBuilder sb, TableMapper table)
    {
        if (string.IsNullOrEmpty(table.Alias)) return;
        sb.Append(table.Alias);
        sb.Append('.');
    }
    private static void HandleAtToColumnName(StringBuilder sb, ColumnMapper column)
    {
        sb.Append('@');
        sb.Append(column.ColumnName);
    }

    private static bool CanInsertColumn(ColumnMapper column)
    {
        if ((column.Attributes & ColumnAttributes.Required) != 0) return true;

        if ((column.Attributes & ColumnAttributes.Key) != 0) return false;
        if ((column.Attributes & ColumnAttributes.ReadOnly) != 0) return false;
        if ((column.Attributes & ColumnAttributes.IgnoreInsert) != 0) return false;
        if ((column.Attributes & ColumnAttributes.NotMapped) != 0) return false;

        return true;
    }
}

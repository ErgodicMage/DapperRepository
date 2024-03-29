﻿namespace DapperRepository;

internal static class PropertiesHelper
{
    public static PropertyInfo[] GetAllProperties<T>(T? entity) where T : class
    {
        if (entity is null) return Enumerable.Empty<PropertyInfo>().ToArray();

        return entity.GetType().GetProperties();
    }

    public static PropertyInfo[] GetScaffoldableProperties<T>() where T : class
        =>  typeof(T).GetProperties().Where(p => p.PropertyType.IsSimpleType()).ToArray();

    public static PropertyInfo[] GetIdProperties(object entity) => GetIdProperties(entity.GetType());

    public static PropertyInfo[] GetIdProperties(Type? type)
    {
        if (type is null) return Enumerable.Empty<PropertyInfo>().ToArray();

        var ids = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(attr =>
            (attr.GetType().Name == nameof(KeyAttribute)) || (attr.GetType().Name == nameof(NonAutoKeyAttribute))));

        if (!ids.Any())
            ids = type.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        return ids.ToArray();
    }

    public static PropertyInfo[] GetUpdateableProperties<T>() where T : class
    {
        var updateableProperties = BuilderCache<T>.ScaffoldProperties.AsEnumerable();
        //remove ones with ID
        updateableProperties = updateableProperties.Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        //remove ones with key attribute
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr =>
            attr.GetType().Name == nameof(KeyAttribute)));
        //remove ones that are readonly
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr =>
            (attr.GetType().Name == nameof(ReadOnlyAttribute)) && IsReadOnly(p)));
        //remove ones with IgnoreUpdate attribute
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr =>
            attr.GetType().Name == nameof(IgnoreUpdateAttribute)));
        //remove ones that are not mapped
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr =>
            attr.GetType().Name == nameof(NotMappedAttribute)));

        return updateableProperties.ToArray();
    }

    public static PropertyInfo[] GetLargeProperties<T>() where T : class
        => BuilderCache<T>.ScaffoldProperties.Where(p => HasLargeProperties(p)).ToArray();

    public static bool HasLargeProperties(PropertyInfo? property)
    {
        dynamic? columnAttribute = property?.GetCustomAttributes().
            FirstOrDefault(attr => attr.GetType().Name == nameof(ColumnAttribute));
        return columnAttribute is not null && columnAttribute.Length > 0;
    }

    public static bool IsReadOnly(PropertyInfo pi)
        => pi.GetCustomAttributes(false).Any(x => x.GetType().Name == nameof(ReadOnlyAttribute));

}

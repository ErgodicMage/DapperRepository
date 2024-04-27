namespace ErgodicMage.DapperRepository;

internal static class CustomAttributesHelper
{
    internal static dynamic? TableAttribute(Type type)
        => type.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;

    internal static dynamic[]? AllCustomAttributes(PropertyInfo property)
        => property.GetCustomAttributes(true) as dynamic[];

    internal static dynamic? ColumnAttribute(PropertyInfo property)
        => property?.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == nameof(ColumnAttribute)) as dynamic;
}

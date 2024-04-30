namespace DapperRepositoryTests;

public class SqlBuilderWhereTests
{
    [Fact]
    public void PersonCountWhereId()
    {
        DapperRepositorySettings settings = new();
        ClassMapper<Person> mapper = new(settings);

        SqlCountBuilder builder = new(settings, mapper);
        string? sql = builder.WhereId().Build();
        Assert.False(string.IsNullOrEmpty(sql));

        Assert.False(string.IsNullOrEmpty(builder.SqlStatement));
        Assert.Equal(sql, builder.SqlStatement);
    }

    [Fact]
    public void PersCountErgodicMage()
    {
        DapperRepositorySettings settings = new();
        ClassMapper<Person> mapper = new(settings);
        mapper.SetTable();
        mapper.SetColumns();

        var firstNameColumn = mapper.Columns?.Where(c => c.ClassName == "FirstName").FirstOrDefault();
        Assert.NotNull(firstNameColumn);
        var lastNameColumn = mapper.Columns?.Where(c => c.ClassName == "LastName").FirstOrDefault();
        Assert.NotNull(lastNameColumn);

        string? sql = SqlCountBuilder.CreateCountBuilder(settings, mapper).
            WhereEqual(firstNameColumn).
            WhereEqual(lastNameColumn).
            Build();
        Assert.False(string.IsNullOrEmpty(sql));
    }
}

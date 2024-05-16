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

        string expected = "SELECT COUNT(1) FROM People AS Person WHERE Person.Id=@Id";
        Assert.Equal(expected, sql);

        Assert.False(string.IsNullOrEmpty(builder.SqlStatement));
        Assert.Equal(expected, builder.SqlStatement);
    }

    [Fact]
    public void PersonCountWhereFirstandLastName()
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

        string expected = "SELECT COUNT(1) FROM People AS Person WHERE First_Name=@FirstName AND Last_Name=@LastName";
        
        Assert.Equal(expected, sql);
    }

    [Fact]
    public void PersonCountWhereConditions()
    {
        DapperRepositorySettings settings = new();
        ClassMapper<Person> mapper = new(settings);

        string? sql = SqlCountBuilder.CreateCountBuilder(settings, mapper).
            Where(new { FirstName = "Ergodic", LastName = "Mage" }).
            Build();

        string expected = "SELECT COUNT(1) FROM People AS Person WHERE First_Name=@FirstName AND Last_Name=@LastName";

        Assert.Equal(expected, sql);
    }
}

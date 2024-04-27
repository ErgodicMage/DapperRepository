namespace DapperRepositoryTests;

public class BuildTests
{
    [Fact]
    public void BuildTableStatement()
    {
        ClassMapper<Person> mapper = new();
        string? tableName = mapper.GetTableName();
        Assert.False(string.IsNullOrEmpty(tableName));
        Assert.Equal("People AS Person", tableName);
    }

    [Fact]
    public void BuildSelectColumns()
    {
        ClassMapper<Person> mapper = new();

        string? selectColumns = mapper.GetSelectColumns();
        Assert.False(string.IsNullOrWhiteSpace(selectColumns));
        string expected = "Person.Id,Person.First_Name AS FirstName,Person.Middle_Name AS MiddleName,Person.Last_Name AS LastName," +
            "Person.SSN AS SocialSecurityNumber,Person.DateOfBirth,Person.MotherId,Person.FatherId";
        Assert.Equal(expected, selectColumns);
    }

    [Fact]
    public void BuildWhereId()
    {
        ClassMapper<Person> mapper = new();

        string? whereId = mapper.GetWhereId();
        Assert.False(string.IsNullOrEmpty(whereId));
        Assert.Equal("Person.Id=@Id", whereId);
    }

    [Fact]
    public void BuildInsertStatement()
    {
        ClassMapper<Person> mapper = new();

        string? insert = mapper.GetInsertStatement();
        Assert.False(string.IsNullOrEmpty(insert));
        string expected = "INSERT INTO People AS Person (Person.First_Name, Person.Middle_Name, Person.Last_Name, Person.SSN, " +
            "Person.DateOfBirth, Person.MotherId, Person.FatherId) " +
            "VALUES (@FirstName, @MiddleName, @LastName, @SocialSecurityNumber, @DateOfBirth, @MotherId, @FatherId)";
        Assert.Equal(expected, insert);
    }
}

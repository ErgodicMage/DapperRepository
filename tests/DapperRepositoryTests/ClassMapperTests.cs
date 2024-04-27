namespace DapperRepositoryTests;

public class ClassMapperTests
{
    [Fact]
    public void PersonMappedManually()
    {
        ClassMapper mapper = new();
        mapper.AddTable("People", "Person");
        mapper.AddColumn("Id", ColumnAttributes.Key);
        mapper.AddColumn("FirstName", "First_Name", ColumnAttributes.Required);
        mapper.AddColumn("MiddleName", "Middle_Name", ColumnAttributes.Required);
        mapper.AddColumn("LastName", "Last_Name", ColumnAttributes.Required);
        mapper.AddColumn("SocialSecurityNumber", "SSN", ColumnAttributes.Required | ColumnAttributes.ReadOnly);
        mapper.AddColumn("DateOfBirth", ColumnAttributes.Required | ColumnAttributes.ReadOnly);
        mapper.AddColumn("MotherId", ColumnAttributes.Required | ColumnAttributes.ReadOnly);
        mapper.AddColumn("FatherId", ColumnAttributes.Required | ColumnAttributes.ReadOnly);

        Assert.NotNull(mapper.Table);
        Assert.NotNull(mapper.Columns);
        Assert.Equal(8, mapper.Columns.Count);
    }

    [Fact]
    public void PersonMappedAutomatically()
    {
        ClassMapper<Person> mapper = new();

        mapper.SetTable();
        mapper.SetColumns();

        Assert.NotNull(mapper.Table);
        Assert.NotNull(mapper.Columns);
        Assert.Equal(8, mapper.Columns.Count);
    }
}
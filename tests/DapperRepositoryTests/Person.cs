namespace DapperRepositoryTests;

[Table("People")]
internal class Person
{
    [Key]
    public int Id { get; set; }

    [Column("First_Name")]
    [Required]
    public string? FirstName { get; set; }

    [Column("Middle_Name")]
    public string? MiddleName { get; set; }

    [Column("Last_Name")]
    [Required]
    public string? LastName { get; set; }

    [Column("SSN")]
    [ReadOnly]
    [Required]
    public string? SocialSecurityNumber { get; set; }

    [ReadOnly]
    [Required]
    public DateOnly? DateOfBirth { get; set; }

    [ReadOnly]
    [Required]
    public int? MotherId { get; set; }

    [ReadOnly]
    [Required]
    public int? FatherId { get; set; }
}

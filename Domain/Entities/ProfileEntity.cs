public class ProfileEntity : BaseEntity
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Source { get; set; }
    public Name? Name { get; set; }    
    public string PictureUrl { get; internal set; }
    public string? PlatformId { get; set; }
}

public record Name(string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}

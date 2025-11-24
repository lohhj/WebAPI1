namespace WebAPI1.Application.Commands;

public abstract class FreelancerCommandBase
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public List<string> Skillsets { get; set; } = new();
    public List<string> Hobbies { get; set; } = new();
}
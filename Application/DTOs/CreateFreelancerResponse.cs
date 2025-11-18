namespace WebAPI1.Application.DTOs;

public class CreateFreelancerResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool Archived { get; set; }
    public List<string> Skillsets { get; set; } = new();
    public List<string> Hobbies { get; set; } = new();

}


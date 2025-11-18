using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI1.Domain.Entities;

[Table("hobbies")]
public class Hobbies
{
    [Column("id")]
    public int Id { get; set; }

    [Column("freelancer_id")]
    public int FreelancerId { get; set; }

    [Column("hobby_name")]
    public string Hobby { get; set; } = string.Empty;
}

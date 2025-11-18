using System.ComponentModel.DataAnnotations.Schema;
using WebAPI1.Domain.Entities;

namespace Domain.Entities
{
    [Table("freelancers")]
    public class Freelancer
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public required string Username { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("phone_number")]
        public required string PhoneNumber { get; set; }

        [Column("archived")]
        public bool Archived { get; set; }

        public List<Skillset> Skillsets { get; set; } = new();
        public List<Hobbies> Hobbies { get; set; } = new();
    }
}

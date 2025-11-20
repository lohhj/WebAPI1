using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI1.Domain.Entities
{
    [Table("skillsets")]
    public class Skillset
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("freelancer_id")]
        public int FreelancerId { get; set; }

        [Column("skill_name")]
        public string Skill { get; set; } = string.Empty;
    }
}

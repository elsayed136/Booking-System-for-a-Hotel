using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Location { get; set; }
    }
}

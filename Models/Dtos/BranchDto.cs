using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class BranchDto
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Location { get; set; }
    }
}

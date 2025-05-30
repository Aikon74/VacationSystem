using System.ComponentModel.DataAnnotations;

namespace VacationSystem.Models
{
    public class User
    {
        
        public int Role { get; set; } = 0; // 0 = vanlig bruker, 1 = admin
        [Key]
        public int UserId { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}




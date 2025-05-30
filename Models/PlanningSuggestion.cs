using System.ComponentModel.DataAnnotations;

namespace VacationSystem.Models
{
    public class PlanningSuggestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Institution { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // false = ikke godkjent ennå, true = godkjent av admin
        public bool Approved { get; set; } = false;
    }
}

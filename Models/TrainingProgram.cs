using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        // علاقة One-to-Many
        public ICollection<WorkoutDay> Days { get; set; } = new List<WorkoutDay>();
    }
}

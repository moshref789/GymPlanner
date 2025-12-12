using System.ComponentModel.DataAnnotations;
namespace GymPlanner.Models
{
    public class WorkoutDay
    {
        public int Id { get; set; }

        [Required]
        public string DayName { get; set; }

        // Foreign Key
        public int TrainingProgramId { get; set; }

        public TrainingProgram? TrainingProgram { get; set; }

        public ICollection<Exercise>? Exercises { get; set; }
    }
}
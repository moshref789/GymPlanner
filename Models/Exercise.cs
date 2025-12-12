using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Weight { get; set; }

        [Required]
        public int Sets { get; set; }

        [Required]
        public int Reps { get; set; }

        // Foreign Key
        public int WorkoutDayId { get; set; }

        // Navigation Property - Make it nullable
        public WorkoutDay? WorkoutDay { get; set; }
    }
}
namespace GymPlanner.Models;

public class WorkoutDay
{
    public int Id { get; set; }
    public string DayName { get; set; } = string.Empty;

    public int TrainingProgramId { get; set; }
    public TrainingProgram? TrainingProgram { get; set; }

    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}

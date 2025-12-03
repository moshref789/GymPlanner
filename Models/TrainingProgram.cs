namespace GymPlanner.Models;

public class TrainingProgram
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<WorkoutDay> Days { get; set; } = new List<WorkoutDay>();
}

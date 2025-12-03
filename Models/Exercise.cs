namespace GymPlanner.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public double Weight { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }

    public int WorkoutDayId { get; set; }
    public WorkoutDay? WorkoutDay { get; set; }
}

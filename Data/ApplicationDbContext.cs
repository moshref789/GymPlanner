using GymPlanner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TrainingProgram> TrainingPrograms { get; set; }
    public DbSet<WorkoutDay> WorkoutDays { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
}

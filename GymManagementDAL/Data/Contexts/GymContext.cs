using System.Reflection;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Data.Contexts;

public class GymContext : IdentityDbContext<ApplicationUser> // Default is IdentityUser
{
    public GymContext(DbContextOptions<GymContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<ApplicationUser>(user =>
        {
            user.Property(e => e.FirstName).HasColumnType("nvarchar").HasMaxLength(50);
            user.Property(e => e.LastName).HasColumnType("nvarchar").HasMaxLength(50);
        });
    }
    
    #region DbSets
    public DbSet<Member> Members { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
    #endregion
}

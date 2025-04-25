using Web.Models;
using Microsoft.EntityFrameworkCore;
using Task = Web.Models.Task;
using System;
using System.Xml;

namespace Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<Operation> Operations { get; set; } = null!;
        public DbSet<Machine> Machines { get; set; } = null!;
        public DbSet<MachineWeeklyUpTime> MachineWeeklyUpTimes { get; set; } = null!;
        public DbSet<Job> Jobs { get; set; } = null!;
        public DbSet<TaskMachine> TaskMachines { get; set; } = null!;

        public DbSet<TaskSchedule> TaskSchedules { get; set; } = null!;

        public DbSet<MachineDownSchedule> MachineDownSchedules { get; set; } = null!;
        public DbSet<MachineOpName> MachineOpNames { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite key for TaskMachine (optional but recommended)
            modelBuilder.Entity<TaskMachine>()
                .HasKey(tm => new { tm.TaskId, tm.MachineId });

            // Configure relationships (EF Core usually infers these, but it's good to be explicit)
            modelBuilder.Entity<TaskMachine>()
                .HasOne(tm => tm.Task)
                .WithMany(t => t.TaskMachines)
                .HasForeignKey(tm => tm.TaskId);

            modelBuilder.Entity<TaskMachine>()
                .HasOne(tm => tm.Machine)
                .WithMany(m => m.TaskMachines)
                .HasForeignKey(tm => tm.MachineId);

            modelBuilder.Entity<TaskMachine>()
                .HasOne(tm => tm.OperationCategory)
                .WithMany()
                .HasForeignKey(tm => tm.OperationCategoryId)
                .IsRequired(false);

            // Configure Operation to MachineOpName relationship
            modelBuilder.Entity<Operation>()
                .HasOne(o => o.MachineOpName)
                .WithMany(m => m.Operations)
                .HasForeignKey(o => o.MachineOpNameId);


            modelBuilder.Entity<MachineOperationPriority>()
                .HasKey(tm => new { tm.MachineId, tm.OperationCategoryId });

            modelBuilder.Entity<MachineOperationPriority>()
                .HasOne(tm => tm.Machine)
                .WithMany(t => t.MachineOperationPrioritys)
                .HasForeignKey(tm => tm.MachineId);

            modelBuilder.Entity<MachineOperationPriority>()
                .HasOne(tm => tm.OperationCategory)
                .WithMany(m => m.MachineOperationPrioritys)
                .HasForeignKey(tm => tm.OperationCategoryId);


            base.OnModelCreating(modelBuilder);
        }

    }
}


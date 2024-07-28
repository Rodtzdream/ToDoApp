using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Context;

public class ToDoContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public DbSet<User> Users { get; set; }

    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<ToDoItem>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<User>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<User>()
            .HasMany(t => t.ToDoItems)
            .WithOne(t => t.Assignee)
            .HasForeignKey(t => t.AssigneeId);

        modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "To do" },
                new Status { Id = 2, Name = "In progress" },
                new Status { Id = 3, Name = "Done" }
            );

        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<User>().HasData(
        //    new User
        //    {
        //        Id = 1,
        //        Name = "John Doe"
        //    }
        //);

        //modelBuilder.Entity<ToDoItem>().HasData(
        //    new ToDoItem
        //    {
        //        Id = 1,
        //        Description = "Buy milk",
        //        DueDate = DateTime.Now.AddDays(1),
        //        IsDone = false,
        //        UserId = 1
        //    },
        //    new ToDoItem
        //    {
        //        Id = 2,
        //        Description = "Buy bread",
        //        DueDate = DateTime.Now.AddDays(1),
        //        IsDone = false,
        //        UserId = 1
        //    }
        //);
    }
}
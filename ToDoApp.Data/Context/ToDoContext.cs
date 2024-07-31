using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Context;

public class ToDoContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public DbSet<Board> Boards { get; set; }

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

        modelBuilder.Entity<Board>()
        .HasKey(b => b.Id);

        modelBuilder.Entity<Board>()
            .HasMany(b => b.ToDoItems)
            .WithOne(t => t.Board)
            .HasForeignKey(t => t.BoardId);

        modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "To do" },
                new Status { Id = 2, Name = "In progress" },
                new Status { Id = 3, Name = "Done" }
            );

        base.OnModelCreating(modelBuilder);        
    }
}
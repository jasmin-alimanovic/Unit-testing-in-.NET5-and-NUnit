using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;

namespace TodoAPI.Data
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public DbSet<TodoCategory> TodoCategory { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ///todo
            ///
            modelBuilder.Entity<Todo>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Todo>()
                .Property(t => t.FinishedAt)
                .IsRequired(false);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.TodoCategory)
                .WithMany(td => td.Todos);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Todos);

            ///todo category

            modelBuilder.Entity<TodoCategory>()
                .HasKey(tc => tc.Id);

            ///status
            ///
            modelBuilder.Entity<Status>().HasKey(s => s.Id);

            modelBuilder.Entity<Status>()
                .Property(s => s.Name)
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TasksManagementApp.Domain.TaskItems;
using TasksManagementApp.Domain.Users;
using TasksManagementApp.Utils;

namespace TasksManagementApp.Infrastructure
{
    public class TasksManagementContext : DbContext
    {
        public TasksManagementContext(DbContextOptions<TasksManagementContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUpTables(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var passwordUser1 = PasswordHash.CreatePasswordHash("123456789012").Value;
            var passwordUser2 = PasswordHash.CreatePasswordHash("210987654321").Value;

            modelBuilder.Entity<User>().HasData(
                new
                {
                    Id = 1,
                    Email = Email.Create("baranauskas.aidas@gmail.com").Value,
                    Role = Role.Admin,
                    Name = "Baranauskas Aidas",
                    PasswordHash = passwordUser1.Hash,
                    PasswordSalt = passwordUser1.Salt
                },
                new
                {
                    Id = 2,
                    Email = Email.Create("aidas.baranauskas@yahoo.com").Value,
                    Role = Role.User,
                    Name = "Aidas Baranauskas",
                    PasswordHash = passwordUser2.Hash,
                    PasswordSalt = passwordUser2.Salt
                });

            modelBuilder.Entity<TaskItem>().HasData(
                new { Id = 1, Name = "Manage team tasks", IsCompleted = false, UserId = 1 },
                new { Id = 2, Name = "Track progress", IsCompleted = true, UserId = 1 },
                new { Id = 3, Name = "Buy product", IsCompleted = false, UserId = 2 },
                new { Id = 4, Name = "Sell product", IsCompleted = false, UserId = 2 },
                new { Id = 5, Name = "Manage transportation", IsCompleted = false, UserId = 2 });
        }

        private void SetUpTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x =>
            {
                x.ToTable("Users").HasKey(k => k.Id);
                x.Property(p => p.Id)
                    .HasColumnName("UserId")
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                x.Property(p => p.Email)
                    .HasConversion(p => p.Value, p => Email.Create(p).Value)
                    .HasMaxLength(50)
                    .IsRequired();
                x.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsRequired();
                x.Property(p => p.Role)
                    .HasConversion(p => p.Value, p => (Role)p)
                    .HasMaxLength(10)
                    .IsRequired();
                x.Property(p => p.PasswordHash)
                    .HasMaxLength(88)
                    .IsFixedLength()
                    .IsRequired();
                x.Property(p => p.PasswordSalt)
                   .HasMaxLength(172)
                   .IsFixedLength()
                   .IsRequired();
                x.HasMany(p => p.Tasks)
                    .WithOne(p => p.User)
                    .OnDelete(DeleteBehavior.Cascade);
                x.HasIndex(i => i.Email).IsUnique();
            });

            modelBuilder.Entity<TaskItem>(x =>
            {
                x.ToTable("Tasks").HasKey(k => k.Id);
                x.Property(p => p.Id)
                    .HasColumnName("TaskId")
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                x.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();
                x.Property(p => p.IsCompleted)
                    .IsRequired();
                x.HasOne(p => p.User)
                    .WithMany(p => p.Tasks)
                    .IsRequired();
            });
        }

        public void ResetDataBase()
        {
            Database.EnsureDeleted();
            Database.Migrate();
        }
    }
}

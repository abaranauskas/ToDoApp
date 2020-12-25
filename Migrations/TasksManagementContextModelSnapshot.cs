﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TasksManagementApp.Infrastructure;

namespace TasksManagementApp.Migrations
{
    [DbContext(typeof(TasksManagementContext))]
    partial class TasksManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TasksManagementApp.Domain.TaskItems.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TaskId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsCompleted = false,
                            Name = "Manage team tasks",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            IsCompleted = true,
                            Name = "Track progress",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            IsCompleted = false,
                            Name = "Buy product",
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            IsCompleted = false,
                            Name = "Sell product",
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            IsCompleted = false,
                            Name = "Manage transportation",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("TasksManagementApp.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("char(88)")
                        .IsFixedLength(true)
                        .HasMaxLength(88);

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("char(172)")
                        .IsFixedLength(true)
                        .HasMaxLength(172);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "baranauskas.aidas@gmail.com",
                            PasswordHash = "L90X7/zHi4tcoqOAM26ytTlBEjIRajbOhU7QBthtfhEGNxez/jCLB+x5l9r+3CE764arcCt66iYxNwucdkHgwg==",
                            PasswordSalt = "P56Q8ZeG8tgr3nis5Xs/FJMj+YoFmZLZkVw7pvMJKG0s9SdYGQGanFftIks4DxKXZK50RF188MFBaX429p4FuwbVoleB3RtOvbnU9mqyCfLFEynarHE1R4AoWmLisSff1gnAy9gBB5KLTd3MiD+FwUNpB7dQbicMnO+bGyASlEg=",
                            Role = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "aidas.baranauskas@yahoo.com",
                            PasswordHash = "3Sy4BZbEJ+U1UkZF4pWQcH9pxASJuMHuZXCQmZYjNIyHOshob99ZwgFe0LEe50igCh/tq1ghWX6N+t7ksobiOg==",
                            PasswordSalt = "y70wnWyMJSP5L0Jc+/r0QtykcG285FE/XjIBX7pKr05cM/bFrU8dtIJnjt8oODSTUZJp6zQrhAPmIi6tpECvKDzsNxSW4/Qqo4Wq5GWtpY5e759uoPTMdM0YKxQLHdiJrduedbBl1KgoP38TWfd1SNx7cxfDKz3wJ5aDoCCObDg=",
                            Role = "user"
                        });
                });

            modelBuilder.Entity("TasksManagementApp.Domain.TaskItems.TaskItem", b =>
                {
                    b.HasOne("TasksManagementApp.Domain.Users.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerAvalonia.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnswerOption> AnswerOptions { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<SessionMode> SessionModes { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAnswer> UserAnswers { get; set; }

    public virtual DbSet<UserAnswerOption> UserAnswerOptions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswerOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("answer_options_pkey");

            entity.ToTable("answer_options");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .HasColumnName("text");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.AnswerOptions)
                .HasForeignKey(d => d.IdTask)
                .HasConstraintName("answer_options_id_task_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdTeacher).HasColumnName("id_teacher");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.IdTeacher)
                .HasConstraintName("courses_id_teacher_fkey");
        });

        modelBuilder.Entity<SessionMode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_mode_pkey");

            entity.ToTable("session_mode");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Mode)
                .HasMaxLength(200)
                .HasColumnName("mode");
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statistics_pkey");

            entity.ToTable("statistics");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CorrectAnswersCount).HasColumnName("correct_answers_count");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IncorrectAnswersCount).HasColumnName("incorrect_answers_count");
            entity.Property(e => e.SuccessRate).HasColumnName("success_rate");
            entity.Property(e => e.TotalTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("total_time");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Statistics)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("statistics_id_user_fkey");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_course_pkey");

            entity.ToTable("student_course");

            entity.HasIndex(e => new { e.IdStudent, e.IdCourse }, "student_course_id_student_id_course_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IdCourse).HasColumnName("id_course");
            entity.Property(e => e.IdStudent).HasColumnName("id_student");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.IdCourse)
                .HasConstraintName("student_course_id_course_fkey");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.IdStudent)
                .HasConstraintName("student_course_id_student_fkey");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tasks_pkey");

            entity.ToTable("tasks");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdTest).HasColumnName("id_test");
            entity.Property(e => e.Media).HasColumnName("media");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.IdTestNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdTest)
                .HasConstraintName("tasks_id_test_fkey");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tests_pkey");

            entity.ToTable("tests");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdCourse).HasColumnName("id_course");
            entity.Property(e => e.IdSessionMode).HasColumnName("id_session_mode");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdCourse)
                .HasConstraintName("tests_courses_fk");

            entity.HasOne(d => d.IdSessionModeNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdSessionMode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tests_session_mode_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BirthDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birth_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("users_id_role_fkey");
        });

        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_answer_pkey");

            entity.ToTable("user_answer");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AnswerTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("answer_time");
            entity.Property(e => e.IdStatistics).HasColumnName("id_statistics");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");

            entity.HasOne(d => d.IdStatisticsNavigation).WithMany(p => p.UserAnswers)
                .HasForeignKey(d => d.IdStatistics)
                .HasConstraintName("user_answer_id_statistics_fkey");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.UserAnswers)
                .HasForeignKey(d => d.IdTask)
                .HasConstraintName("user_answer_id_task_fkey");
        });

        modelBuilder.Entity<UserAnswerOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_answer_option_pkey");

            entity.ToTable("user_answer_option");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IdAnswerOption).HasColumnName("id_answer_option");
            entity.Property(e => e.IdUserAnswer).HasColumnName("id_user_answer");

            entity.HasOne(d => d.IdAnswerOptionNavigation).WithMany(p => p.UserAnswerOptions)
                .HasForeignKey(d => d.IdAnswerOption)
                .HasConstraintName("user_answer_option_id_answer_option_fkey");

            entity.HasOne(d => d.IdUserAnswerNavigation).WithMany(p => p.UserAnswerOptions)
                .HasForeignKey(d => d.IdUserAnswer)
                .HasConstraintName("user_answer_option_id_user_answer_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_role_pkey");

            entity.ToTable("user_role");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

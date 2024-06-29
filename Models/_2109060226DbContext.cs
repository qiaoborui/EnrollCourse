using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace EnrollCourse.Models;

public partial class _2109060226DbContext : DbContext
{
    public _2109060226DbContext()
    {
    }

    public _2109060226DbContext(DbContextOptions<_2109060226DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Selectedcourse> Selectedcourses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=rm-bp1tg219t7o5j5ex8fo.rwlb.rds.aliyuncs.com;port=3306;database=2109060226_db;user id=2109060226;password=2109060226", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PRIMARY");

            entity.ToTable("course");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("courseID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("courseName");
            entity.Property(e => e.Credit).HasColumnName("credit");
        });

        modelBuilder.Entity<Selectedcourse>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.StudentId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("selectedcourse");

            entity.HasIndex(e => e.StudentId, "selectedcourse_ibfk_2");

            entity.Property(e => e.CourseId).HasColumnName("courseID");
            entity.Property(e => e.StudentId).HasColumnName("studentID");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("createDate");

            entity.HasOne(d => d.Course).WithMany(p => p.Selectedcourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("selectedcourse_ibfk_1");

            entity.HasOne(d => d.Student).WithMany(p => p.Selectedcourses)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("selectedcourse_ibfk_2");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PRIMARY");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("studentID");
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .HasColumnName("class");
            entity.Property(e => e.InitialPassword)
                .HasMaxLength(50)
                .HasColumnName("initialPassword");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName("studentName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

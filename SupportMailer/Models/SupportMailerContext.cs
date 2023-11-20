using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SupportMailer.Models;

public partial class SupportMailerContext : DbContext
{
    public SupportMailerContext()
    {
    }

    public SupportMailerContext(DbContextOptions<SupportMailerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaseUser> BaseUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=SupportMailer;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__BaseUser__B9BE370FED9BF071");

            entity.ToTable("BaseUser");

            entity.HasIndex(e => e.Email, "UQ__BaseUser__AB6E6164F9CBF494").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__BaseUser__F3DBC5729C87DDDC").IsUnique();

            entity.HasIndex(e => e.Email, "idx_BaseUser_email");

            entity.HasIndex(e => e.Username, "idx_BaseUser_username");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("is_active");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auth_Box.Models;

public partial class AuthboxDbContext : DbContext
{
    public AuthboxDbContext(DbContextOptions<AuthboxDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<user_provider> user_providers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("users_pkey");

            entity.HasIndex(e => e.email, "users_email_key").IsUnique();

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.full_name).HasMaxLength(100);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.phone_number).HasMaxLength(20);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<user_provider>(entity =>
        {
            entity.HasKey(e => e.id).HasName("user_providers_pkey");

            entity.HasIndex(e => new { e.provider, e.provider_user_id }, "uq_provider_user").IsUnique();

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.provider).HasMaxLength(50);
            entity.Property(e => e.provider_user_id).HasMaxLength(255);

            entity.HasOne(d => d.user).WithMany(p => p.user_providers)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("fk_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

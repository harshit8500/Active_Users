using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SampleActiveUsers.NewtrsyModel;

namespace SampleActiveUsers.Data;

public partial class SmplActurContext : DbContext
{
    
    /*public SmplActurContext()
    {
    }*/

    public SmplActurContext(DbContextOptions<SmplActurContext> options)
        : base(options)
    {
    }
    public DbSet<UserActivity> UserActivities { get; set; }
    //public virtual DbSet<UserActivity> UserActivity { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Lenovo;Database=Smpl_Actur;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserActi__3214EC0721B2699A");

            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.LoginTime).HasColumnType("datetime");
            entity.Property(e => e.LogoutTime).HasColumnType("datetime");
            entity.Property(e => e.UserAgent).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

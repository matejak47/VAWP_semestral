using System;
using System.Collections.Generic;
using FM_VAWP_Jiranek_Semestralka.Model;
using Microsoft.EntityFrameworkCore;

namespace FM_VAWP_Jiranek_Semestralka.Data;

public partial class VAPW_PS_DrivesContext : DbContext
{
    private readonly String Connect;
    public VAPW_PS_DrivesContext(String Connect)
    {
        this.Connect = Connect;
    }

    public VAPW_PS_DrivesContext(DbContextOptions<VAPW_PS_DrivesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DriveData> DriveData { get; set; }

    public virtual DbSet<Recordings> Recordings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Connect);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriveData>(entity =>
        {
            entity.Property(e => e.AxCalibrationStatusInfo).HasMaxLength(32);
            entity.Property(e => e.AyCalibrationStatusInfo).HasMaxLength(32);
            entity.Property(e => e.AzCalibrationStatusInfo).HasMaxLength(32);
            entity.Property(e => e.GxCalibrationStatusInfo).HasMaxLength(32);
            entity.Property(e => e.GyCalibrationStatusInfo).HasMaxLength(32);
            entity.Property(e => e.GzCalibrationStatusInfo).HasMaxLength(32);
            entity.Property(e => e.ImuStatusInfo).HasMaxLength(32);

            entity.HasOne(d => d.Recording).WithMany(p => p.DriveData).HasForeignKey(d => d.RecordingId);
        });

        modelBuilder.Entity<Recordings>(entity =>
        {
            entity.Property(e => e.SensorsDeviceName).HasDefaultValue("");
            entity.Property(e => e.UIID)
                .HasMaxLength(32)
                .HasDefaultValue("");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasForeignKey(d => d.ParentId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QBTourDuLich.Models;

public partial class QbdulichContext : DbContext
{
    public QbdulichContext()
    {
    }

    public QbdulichContext(DbContextOptions<QbdulichContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DiaDiemTour> DiaDiemTours { get; set; }

    public virtual DbSet<DiemThamQuan> DiemThamQuans { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<KhachSan> KhachSans { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DEV\\SQLEXPRESS;Initial Catalog=QBDULICH;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiaDiemTour>(entity =>
        {
            entity.HasKey(e => new { e.MaDd, e.MaTour }).HasName("PK__DiaDiemT__D3C0D318E232201B");

            entity.ToTable("DiaDiemTour");

            entity.Property(e => e.MaDd)
                .HasMaxLength(50)
                .HasColumnName("MaDD");
            entity.Property(e => e.MaTour).HasMaxLength(50);
            entity.Property(e => e.ViTriAnh).HasMaxLength(50);

            entity.HasOne(d => d.MaDdNavigation).WithMany(p => p.DiaDiemTours)
                .HasForeignKey(d => d.MaDd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiaDiemTou__MaDD__38996AB5");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.DiaDiemTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiaDiemTo__MaTou__398D8EEE");
        });

        modelBuilder.Entity<DiemThamQuan>(entity =>
        {
            entity.HasKey(e => e.MaDd).HasName("PK__DiemTham__272586659852785E");

            entity.ToTable("DiemThamQuan");

            entity.Property(e => e.MaDd)
                .HasMaxLength(50)
                .HasColumnName("MaDD");
            entity.Property(e => e.MoTa).HasMaxLength(4000);
            entity.Property(e => e.TenFileAnh).HasMaxLength(50);
            entity.Property(e => e.Tendiadiem).HasMaxLength(100);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.MaSk).HasName("PK__Event__27250811C22DBD68");

            entity.ToTable("Event");

            entity.Property(e => e.MaSk)
                .HasMaxLength(50)
                .HasColumnName("MaSK");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MoTa).HasMaxLength(4000);
            entity.Property(e => e.NoiDung).HasMaxLength(4000);
            entity.Property(e => e.TenFileAnh).HasMaxLength(50);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__MaNV__2D27B809");
        });

        modelBuilder.Entity<KhachSan>(entity =>
        {
            entity.HasKey(e => e.MaKs).HasName("PK__KhachSan__2725CF1327A6AD4E");

            entity.ToTable("KhachSan");

            entity.Property(e => e.MaKs)
                .HasMaxLength(50)
                .HasColumnName("MaKS");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.TenFileAnh)
                .HasMaxLength(50)
                .HasColumnName("TenFIleAnh");
            entity.Property(e => e.TenKs)
                .HasMaxLength(100)
                .HasColumnName("TenKS");
            entity.Property(e => e.XepHangKs).HasColumnName("XepHang_KS");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.KhachSans)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhachSan__MaNV__4222D4EF");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.MaTin).HasName("PK__News__31490335102F51EF");

            entity.Property(e => e.MaTin).HasMaxLength(50);
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MoTa).HasMaxLength(4000);
            entity.Property(e => e.NoiDung)
                .HasMaxLength(4000)
                .HasColumnName("Noi_dung");
            entity.Property(e => e.TenFileAnh).HasMaxLength(50);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.News)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__News__MaNV__32E0915F");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A8355BBA5");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__UserNa__2A4B4B5E");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__TaiKhoan__C9F28457C86B47AE");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.ConfirmPassword).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.MaTour).HasName("PK__Tour__4E5557DE1D01784A");

            entity.ToTable("Tour");

            entity.Property(e => e.MaTour).HasMaxLength(50);
            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MoTa).HasMaxLength(4000);
            entity.Property(e => e.TenTour).HasMaxLength(100);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour__MaNV__35BCFE0A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

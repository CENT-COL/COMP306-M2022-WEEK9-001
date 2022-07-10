using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CityInfoLibrary.Models
{
    public partial class CityInfoDBContext : DbContext
    {
        public CityInfoDBContext()
        {
        }

        public CityInfoDBContext(DbContextOptions<CityInfoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CityInfo> CityInfo { get; set; }
        public virtual DbSet<PointOfInterest> PointsOfInterest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=mssqlserver-comp306-summer2022.c0tp8qgq2ue5.us-east-1.rds.amazonaws.com;Initial Catalog=CityInfoDB;Persist Security Info=True;User ID=admin;Password=mssqlserver4api");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CityInfo>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__CityInfo__F2D21B7624939503");

                entity.ToTable("CityInfo");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<PointOfInterest>(entity =>
            {
                entity.ToTable("PointsOfInterest");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameofPoi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NameofPOI");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PointsOfInterest)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_PointsOfInterest_Cities_CityId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

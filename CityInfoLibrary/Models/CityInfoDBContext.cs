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

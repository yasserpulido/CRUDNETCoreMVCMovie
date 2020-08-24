using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RandomMovie.Models
{
    public partial class RDBContext : DbContext
    {
        public RDBContext()
        {
        }

        public RDBContext(DbContextOptions<RDBContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Tmovie> Tmovie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tmovie>(entity =>
            {
                entity.HasKey(e => e.Idmovie);

                entity.ToTable("TMovie");

                entity.Property(e => e.Idmovie).HasColumnName("IDMovie");

                entity.Property(e => e.PickDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

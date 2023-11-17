using react.api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace react.api.DBContext;
public partial class ApiDdContext : DbContext
{
        public ApiDdContext()
        {
        }

        public ApiDdContext(DbContextOptions<ApiDdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Usuario");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Nombre).HasMaxLength(40).HasColumnName("Nombre");
                entity.Property(e => e.Coreo).HasMaxLength(50).HasColumnName("Coreo");
            });
        }


}


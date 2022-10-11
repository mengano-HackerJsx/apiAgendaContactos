using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactDiary.Models
{
    public partial class AgendaTelefonicaContext : DbContext
    {
        public AgendaTelefonicaContext()
        {
        }

        public AgendaTelefonicaContext(DbContextOptions<AgendaTelefonicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacto> Contactos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5IQVCHO; Database=AgendaTelefonica; Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.IdContacto)
                    .HasName("PK__contacto__099A52B8FD045F20");

                entity.ToTable("contacto");

                entity.Property(e => e.IdContacto).HasColumnName("id_contacto");

                entity.Property(e => e.DescripcionContacto)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_contacto");

                entity.Property(e => e.NombreContacto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_contacto");

                entity.Property(e => e.TelContacto)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("tel_contacto");

                entity.Property(e => e.Usuario).HasColumnName("usuario");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK__contacto__usuari__267ABA7A");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__usuario__D2D14637CC1CF75D");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.ApellidoUser)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido_user");

                entity.Property(e => e.Clave).HasColumnName("clave");

                entity.Property(e => e.NombreUser)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_user");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

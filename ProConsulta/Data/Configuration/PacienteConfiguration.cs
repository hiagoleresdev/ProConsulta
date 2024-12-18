using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProConsulta.Models;

namespace ProConsulta.Data.Configuration
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");

            builder.HasKey(x=> x.Id);

            builder.Property(x => x.Nome)
                .IsRequired(true)
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Documento)
                .IsRequired(true)
                .HasColumnType("VARCHAR(11)");

            builder.Property(x => x.Email)
               .IsRequired(true)
               .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Celular)
               .IsRequired(true)
               .HasColumnType("VARCHAR(12)");

            builder.HasIndex(x => x.Documento)
               .IsUnique();

            builder.HasMany(a => a.Agendamento)
                .WithOne(p => p.Paciente)
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

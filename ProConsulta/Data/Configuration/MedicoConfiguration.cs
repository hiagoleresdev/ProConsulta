using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProConsulta.Models;

namespace ProConsulta.Data.Configuration
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medicos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired(true)
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Documento)
                .IsRequired(true)
                .HasColumnType("VARCHAR(11)");

            builder.Property(x => x.Crm)
                .IsRequired(true)
                .HasColumnType("NVARCHAR(11)");

            builder.Property(x => x.Celular)
                .IsRequired(true)
                .HasColumnType("NVARCHAR(12)");

            builder.HasIndex(x => x.Documento)
               .IsUnique();

            builder.HasMany(a => a.Agendamento)
                .WithOne(m => m.Medico)
                .HasForeignKey(a => a.MedicoId);
        }
    }
}

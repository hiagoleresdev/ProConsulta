using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProConsulta.Models;

namespace ProConsulta.Data.Configuration
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("Agendamentos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Observacao)
                .IsRequired(true)
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.PacienteId)
                .IsRequired(true);

            builder.Property(x => x.MedicoId)
                .IsRequired(true);

        }
    }
}

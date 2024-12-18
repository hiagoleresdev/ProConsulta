using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProConsulta.Models;

namespace ProConsulta.Data.Configuration
{
    public class EspecialidadeConfiguration : IEntityTypeConfiguration<Especialidade>
    {
        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {
            builder.ToTable("Especialidades");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired(true)
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Descricao)
               .IsRequired(true)
               .HasColumnType("VARCHAR(150)");

            builder.HasMany(m => m.Medicos)
                .WithOne(e => e.Especialidade)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

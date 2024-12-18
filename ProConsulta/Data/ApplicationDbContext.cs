using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProConsulta.Models;
using System.Reflection;

namespace ProConsulta.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Mantenha isso antes para evitar problemas com Identity

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            new DbInitializer(builder).Seed();
        }
    }
}

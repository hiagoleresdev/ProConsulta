using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProConsulta.Models;

namespace ProConsulta.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;
        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        internal void Seed()
        {
            _modelBuilder.Entity<IdentityRole>().HasData
            (
                new IdentityRole
                {
                    Id = "8fd76f7d-c1be-4d65-8c9a-3a3f5babc7f1",
                    Name = "Atendente",
                    NormalizedName = "ATENDENTE"
                },
                new IdentityRole
                {
                    Id = "4e13f9a2-81d9-4a0e-8450-c16c42e8b74d",
                    Name = "Medico",
                    NormalizedName = "MEDICO"
                }

            );

            var hasher = new PasswordHasher<IdentityUser>();
            _modelBuilder.Entity<Atendente>().HasData
            (
                new Atendente
                {
                    Id = "5b093c3e-7df6-411d-89e7-3b3c92449d8a",
                    Nome = "João da Silva",
                    UserName = "joao.silva@clinica.com",
                    NormalizedUserName = "JOAO.SILVA@CLINICA.COM",
                    Email = "joao.silva@clinica.com",
                    PasswordHash = hasher.HashPassword(null, "Senha123!"),
                    EmailConfirmed = true,
                    NormalizedEmail = "JOAO.SILVA@CLINICA.COM",
                }
            );

            _modelBuilder.Entity<IdentityUserRole<string>>().HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "8fd76f7d-c1be-4d65-8c9a-3a3f5babc7f1",
                    UserId = "5b093c3e-7df6-411d-89e7-3b3c92449d8a"
                }
                
            );

            _modelBuilder.Entity<Especialidade>().HasData
            (
                new Especialidade { Id = 1, Nome = "Cardiologia", Descricao = "Especialidade médica que cuida do coração" },
                new Especialidade { Id = 2, Nome = "Dermatologia", Descricao = "Especialidade que trata de doenças e condições da pele." },
                new Especialidade { Id = 3, Nome = "Ortopedia", Descricao = "Especialidade focada no tratamento de ossos, músculos ." },
                new Especialidade { Id = 4, Nome = "Pediatria", Descricao = "Área médica voltada ao cuidado da saúde infantil." },
                new Especialidade { Id = 5, Nome = "Ginecologia", Descricao = "Especialidade que trata da saúde do sistema reprodutor feminino." },
                new Especialidade { Id = 6, Nome = "Neurologia", Descricao = "Área da medicina que trata do sistema nervoso." },
                new Especialidade { Id = 7, Nome = "Psiquiatria", Descricao = "Especialidade que cuida da saúde mental e transtornos psicológicos." },
                new Especialidade { Id = 8, Nome = "Oftalmologia", Descricao = "Especialidade dedicada ao cuidado da visão e dos olhos." },
                new Especialidade { Id = 9, Nome = "Endocrinologia", Descricao = "Especialidade que trata das glândulas e hormônios do corpo." },
                new Especialidade { Id = 10, Nome = "Gastroenterologia", Descricao = "Área médica que cuida do sistema digestivo." }
            );
        }

    }
}

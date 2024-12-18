using ProConsulta.Models;

namespace ProConsulta.Repositories.Medicos
{
    public interface IMedicoRepository
    {
        Task AddAsync(Medico medico);
        Task UpdateAsync(Medico medico);
        Task<List<Medico>> GetAllAsync();
        Task DeleteByIdAsync(int id);
        Task<Medico?> GetByIdAsync(int id);
    }
}

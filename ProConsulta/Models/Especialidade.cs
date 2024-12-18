namespace ProConsulta.Models
{
    public class Especialidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();
    }
}

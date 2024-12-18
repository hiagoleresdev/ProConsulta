namespace ProConsulta.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Crm { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Celular { get; set; }
        public int EspecialidadeId { get; set; }
        public Especialidade Especialidade { get; set; }
        public ICollection<Agendamento> Agendamento { get; set; } = new List<Agendamento>();
    }
}

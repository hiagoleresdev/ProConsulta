namespace ProConsulta.Models
{
    public class Paciente
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public DateTime DataNascimento { get; set; }

        public ICollection<Agendamento> Agendamento { get; set; } = new List<Agendamento>();
    }
}

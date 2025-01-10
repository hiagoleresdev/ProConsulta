using ProConsulta.Models;
using System.ComponentModel.DataAnnotations;

namespace ProConsulta.Components.Pages.Agendamentos
{
    public class AgendamentoInputModel
    {
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public string? Observacao { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public int MedicoId { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public TimeSpan HoraConsulta { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public DateTime DataConsulta { get; set; }

    }
}

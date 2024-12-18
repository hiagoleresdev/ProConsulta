using System.ComponentModel.DataAnnotations;

namespace ProConsulta.Components.Pages.Pacientes
{
    public class PacienteInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o documento")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        [StringLength(50, ErrorMessage = "O email deve ter no máximo 150 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o celular")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}

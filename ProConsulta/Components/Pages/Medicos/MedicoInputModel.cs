using System.ComponentModel.DataAnnotations;

namespace ProConsulta.Components.Pages.Medicos
{
    public class MedicoInputModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public string Crm { get; set; }
        public DateTime DataCadastro { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "{0} deve ser fornecido!")]
        public int EspecialidadeId { get; set; }
    }
}

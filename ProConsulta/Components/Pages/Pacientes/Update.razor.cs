using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ProConsulta.Extentions;
using ProConsulta.Models;
using ProConsulta.Repositores.Pacientes;

namespace ProConsulta.Components.Pages.Pacientes
{
    public class UpdatePaciente : ComponentBase
    {
        [Inject]
        public IPacienteRepository repository { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Parameter]
        public int PacienteId { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        public PacienteInputModel InputModel { get; set; } = new PacienteInputModel();
        private Paciente? Current {  get; set; }
        public DateTime DataNascimento { get; set; } = DateTime.Today;
        public DateTime? MaxDate { get; set; } = DateTime.Today;
        protected override async Task OnInitializedAsync()
        {
            Current = await repository.GetByIdAsync(PacienteId);
            if (Current == null)
                return;

            InputModel = new PacienteInputModel()
            {
                Id = Current.Id,
                Nome = Current.Nome,
                Celular = Current.Celular,
                DataNascimento = Current.DataNascimento,    
                Email = Current.Email,
                Documento = Current.Documento,
            };

            DataNascimento = Current.DataNascimento;
        }
        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if(editContext.Model is PacienteInputModel model)
                {
                    Current.Nome = model.Nome;
                    Current.Documento = model.Documento.SomenteCaracteres();
                    Current.Celular = model.Celular.SomenteCaracteres();
                    Current.Email = model.Email;
                    Current.DataNascimento = DataNascimento;

                    await repository.UpdateAsync(Current);
                    Snackbar.Add($"Paciente {Current.Nome} atualizado com sucesso", Severity.Success);
                    navigationManager.NavigateTo("/pacientes");
                }
            }catch(Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}

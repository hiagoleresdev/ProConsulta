using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ProConsulta.Extentions;
using ProConsulta.Models;
using ProConsulta.Repositores.Pacientes;

namespace ProConsulta.Components.Pages.Pacientes
{
    public class CreatePacientePage : ComponentBase
    {
        [Inject]
        public IPacienteRepository repository { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public PacienteInputModel InputModel { get; set; } = new PacienteInputModel();

        public DateTime DataNascimento { get; set; } = DateTime.Today;

        public DateTime? MaxDate { get; set; }= DateTime.Today;

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                Snackbar.Add("Submissão iniciada!", Severity.Info);
                if (editContext.Model is PacienteInputModel model)
                {
                    Snackbar.Add($"Dados recebidos: Nome: {model.Nome}, Documento: {model.Documento}", Severity.Info);

                    var paciente = new Paciente
                    {
                        Nome = model.Nome,
                        Documento = model.Documento.SomenteCaracteres(),
                        Celular = model.Celular.SomenteCaracteres(),
                        Email = model.Email,
                        DataNascimento = model.DataNascimento,
                    };

                    Snackbar.Add("Adicionando paciente no banco...", Severity.Info);

                    await repository.AddAsync(paciente);

                    Snackbar.Add("Paciente adicionado com sucesso!", Severity.Success);

                    navigationManager.NavigateTo("/pacientes");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
            }
        }
    }
}

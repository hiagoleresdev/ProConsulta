using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using ProConsulta.Models;
using ProConsulta.Repositores.Pacientes;

namespace ProConsulta.Components.Pages.Pacientes
{
    public class IndexPage : ComponentBase
    {
        [Inject]
        public IPacienteRepository repository {  get; set; }

        [Inject]
        public IDialogService Dialog {  get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public List<Paciente> Pacientes { get; set; }
        public bool HideButtons { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> Authentication { get; set; }
        public async Task DeletePaciente(Paciente paciente)
        {
            try
            {
                var result = await Dialog.ShowMessageBox
                (
                  "Atenção",
                  $"Deseja excluir o paciente {paciente.Nome}? ",
                  yesText: "Sim",
                  cancelText: "Não"
                );

                if (result is true)
                {
                    await repository.DeleteByIdAsync(paciente.Id);
                    Snackbar.Add($"Paciente {paciente.Nome} excluido com sucess!", Severity.Success);
                    await OnInitializedAsync();
                }
            }catch ( Exception ex )
            {
                Snackbar.Add(ex.Message, Severity.Error);   
            }
        }

        public void GoToUpdate(int id)
        {
            navigationManager.NavigateTo($"/pacientes/update/{id}");
        }

        protected override async  Task OnInitializedAsync()
        {
            var auth = await Authentication;
            Pacientes = await repository.GetAllAsync();
            HideButtons = !auth.User.IsInRole("Atendente");
            Pacientes = await repository.GetAllAsync();
        }
    }
}

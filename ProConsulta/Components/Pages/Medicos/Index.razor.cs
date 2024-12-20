using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProConsulta.Models;
using ProConsulta.Repositores.Pacientes;
using ProConsulta.Repositories.Medicos;

namespace ProConsulta.Components.Pages.Medicos
{
    public class IndexMedicoPage: ComponentBase
    {
        [Inject]
        public IMedicoRepository repository { get; set; }
        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; } = null!;
        public List<Medico> Medicos { get; set; } = new List<Medico>();

        public async Task DeletePaciente(Medico medico)
        {
            try
            {
                var result = await Dialog.ShowMessageBox
                (
                  "Atenção",
                  $"Deseja excluir o médico {medico.Nome}? ",
                  yesText: "Sim",
                  cancelText: "Não"
                );

                if (result is true)
                {
                    await repository.DeleteByIdAsync(medico.Id);
                    Snackbar.Add($"Médico {medico.Nome} excluido com sucesso!", Severity.Success);
                    await OnInitializedAsync();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public void GoToUpdate(int id)
        {
            navigationManager.NavigateTo($"/medicos/update/{id}");
        }

        protected override async Task OnInitializedAsync()
        {
            Medicos = await repository.GetAllAsync();
        }

    }
}

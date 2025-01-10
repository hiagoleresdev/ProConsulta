using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using ProConsulta.Models;
using ProConsulta.Repositories.Agendamentos;
using ProConsulta.Repositories.Medicos;

namespace ProConsulta.Components.Pages.Agendamentos
{
    public class IndexAgendamentoPage : ComponentBase
    {
        [Inject]
        public IAgendamentoRepository repository { get; set; } = null!;
        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
        public bool HideButtons { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> Authentication { get; set; }
        public async Task DeleteAgendamento(Agendamento agendamento)
        {
            try
            {
                var result = await Dialog.ShowMessageBox
                (
                  "Atenção",
                  $"Deseja excluir este agendamento ? ",
                  yesText: "Sim",
                  cancelText: "Não"
                );

                if (result is true)
                {
                    await repository.DeleteByIdAsync(agendamento.Id);
                    Snackbar.Add($"Agendamento excluido com sucesso!", Severity.Success);
                    await OnInitializedAsync();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var auth = await Authentication;
            Agendamentos = await repository.GetAllAsync();
            HideButtons = !auth.User.IsInRole("Atendente");

        }
    }
}

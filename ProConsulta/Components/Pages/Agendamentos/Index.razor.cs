using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProConsulta.Models;
using ProConsulta.Repositories.Agendamentos;

namespace ProConsulta.Components.Pages.Agendamentos
{
    public class IndexAgendamentoPage : ComponentBase
    {
        [Inject]
        private IAgendamentoRepository AgendamentoRepository { get; set; } = null!;

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        public List<Agendamento> Agendamentos { get; set;} = new List<Agendamento>();

        public async Task DeleteAgendamento(Agendamento agendamento)
        {
            try
            {
                var result = await Dialog.ShowMessageBox
                (
                    "Atenção",
                    "Deseja excluir esse agendamento?",
                    yesText: "SIM",
                    cancelText: "No"
                );

                if(result is true)
                {
                    await AgendamentoRepository.DeleteByIdAsync (agendamento.Id);
                    Snackbar.Add("Agendamento excluído com sucesso!", Severity.Success);
                    await OnInitializedAsync(); 
                }
            }catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            Agendamentos = await AgendamentoRepository.GetAllAsync();   
        }
    }
}

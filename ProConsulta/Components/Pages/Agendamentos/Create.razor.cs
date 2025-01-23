using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ProConsulta.Components.Pages.Medicos;
using ProConsulta.Extentions;
using ProConsulta.Models;
using ProConsulta.Repositores.Pacientes;
using ProConsulta.Repositories.Agendamentos;
using ProConsulta.Repositories.Especialidades;
using ProConsulta.Repositories.Medicos;
using ProConsulta.Repositories.Pacientes;
using System.Net.Mail;
using System.Text;

namespace ProConsulta.Components.Pages.Agendamentos
{
    public class CreateAgendamentoPage : ComponentBase
    {
        [Inject]
        public IAgendamentoRepository repository { get; set; } = null!;

        [Inject]
        public IMedicoRepository medicoRepository { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public NavigationManager navigationManager { get; set; } = null!;
        [Inject]
        public IPacienteRepository pacienteRepository { get; set; } = null!;
        public AgendamentoInputModel InputModel { get; set; } = new AgendamentoInputModel();
        public List<Medico> Medicos { get; set; } = new List<Medico>();
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();

        public TimeSpan? time = new TimeSpan(09,0, 0);  
        public DateTime? date { get; set; } = DateTime.Now.Date;
        public DateTime? MinDate { get; set; } = DateTime.Now.Date;

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if (editContext.Model is AgendamentoInputModel model)
                {
                    var agendamento = new Agendamento
                    {
                       Observacao = model.Observacao,
                       PacienteId = model.PacienteId,
                       MedicoId = model.MedicoId,
                       HoraConsulta = time!.Value,
                       DataConsulta = date!.Value,
                    };

                    await repository.AddAsync(agendamento);
                    Snackbar.Add("Agendamento cadastrado com sucesso!", Severity.Success);
                    EnviarEmailConfirmação(agendamento);
                    navigationManager.NavigateTo("/agendamentos");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public async void EnviarEmailConfirmação(Agendamento agendamento)
        {
            var paciente = await pacienteRepository.GetByIdAsync(agendamento.PacienteId);
            if (paciente == null)
            {
                Snackbar.Add("Paciente não encontrado!", Severity.Error);
            }


            var medico = await medicoRepository.GetByIdAsync(agendamento.MedicoId);
            if (medico == null)
            {
                Snackbar.Add("Medico não encontrado!", Severity.Error);
            }

            await EmailSender.EnviarEmailAgendamento(agendamento, paciente, medico);
        }

        
        protected override async Task OnInitializedAsync()
        {
            Medicos = await medicoRepository.GetAllAsync();
            Pacientes = await pacienteRepository.GetAllAsync();
        }
    }
}

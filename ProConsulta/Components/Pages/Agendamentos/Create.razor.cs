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
                    await SendEmailAsync(agendamento);
                    navigationManager.NavigateTo("/agendamentos");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public async Task<bool> SendEmailAsync(Agendamento agendamento)
        {
            // Obtenha o paciente
            var paciente = await pacienteRepository.GetByIdAsync(agendamento.PacienteId);
            if (paciente == null)
            {
                Snackbar.Add("Paciente não encontrado.", Severity.Error);
                return false;
            }

            // Obtenha o médico
            var medico = await medicoRepository.GetByIdAsync(agendamento.MedicoId);
            if (medico == null)
            {    
                Snackbar.Add("Médico não encontrado.", Severity.Error);
                return false;
            }

            // Configuração do SMTP e criação da mensagem
            string smtpServer = "smtp.gmail.com";
            string to = paciente.Email;
            string from = "proconsulta4@gmail.com";
            string? password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD"); // Utilize variáveis de ambiente para segurança

            if (string.IsNullOrEmpty(password))
            {
                Snackbar.Add("A senha do e-mail não foi configurada corretamente.", Severity.Error);
                return false;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Prezado(a) {paciente.Nome},");
            sb.AppendLine();
            sb.AppendLine("Esperamos que esta mensagem o(a) encontre bem. Estamos confirmando sua consulta médica conforme os detalhes abaixo:");
            sb.AppendLine();
            sb.AppendLine($"- Data: {agendamento.DataConsulta:dd/MM/yyyy}");
            sb.AppendLine($"- Horário: {agendamento.HoraConsulta.ToString(@"hh\:mm")}");
            sb.AppendLine($"- Profissional: Dr(a). {medico.Nome}");
            sb.AppendLine();
            sb.AppendLine("Agradecemos por escolher nossa equipe para cuidar da sua saúde.");
            sb.AppendLine();
            sb.AppendLine("Atenciosamente,");
            sb.AppendLine("ProConsulta");

            using var message = new MailMessage(from, to)
            {
                Subject = "Confirmação de Consulta Médica",
                Body = sb.ToString(),
                IsBodyHtml = false, // Caso seja texto puro
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8
            };

            using var client = new SmtpClient(smtpServer)
            {
                Port = 587, // Porta para TLS
                Credentials = new System.Net.NetworkCredential(from, password),
                EnableSsl = true
            };

            try
            {
                // Envia o e-mail de forma assíncrona
                await client.SendMailAsync(message);
                Snackbar.Add("Enviamos um e-mail para confirmar a consulta!", Severity.Success);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o e-mail: {ex.Message}");
                Snackbar.Add("Ocorreu um erro ao tentar enviar o e-mail. Por favor, tente novamente.", Severity.Error);
                return false;
            }
        }
        protected override async Task OnInitializedAsync()
        {
            Medicos = await medicoRepository.GetAllAsync();
            Pacientes = await pacienteRepository.GetAllAsync();
        }
    }
}

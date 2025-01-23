using ProConsulta.Models;
using System.Net.Mail;
using System.Text;

namespace ProConsulta.Extentions
{
    public static class EmailSender
    {
        public static async Task<bool> EnviarEmail(string to, string subject, string body)
        {
            string smtpServer = "smtp.gmail.com";
            string from = "proconsulta4@gmail.com";
            string? password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Erro: variável de ambiente EMAIL_PASSWORD não configurada.");
                return false;
            }

            using var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };

            using var client = new SmtpClient(smtpServer)
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(from, password),
                EnableSsl = true
            };

            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o e-mail: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> EnviarEmailAgendamento(Agendamento agendamento, Paciente paciente, Medico medico)
        {
            string smtpServer = "smtp.gmail.com";
            string to = paciente.Email;
            string from = "proconsulta4@gmail.com";
            string? password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD"); //Utilizar senha de aplicativo para teste

            if (string.IsNullOrEmpty(password))
            {
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
                IsBodyHtml = false,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8
            };

            using var client = new SmtpClient(smtpServer)
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(from, password),
                EnableSsl = true
            };

            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o e-mail: {ex.Message}");
                return false;
            }
        }
    }
}

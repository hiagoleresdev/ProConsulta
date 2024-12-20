using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ProConsulta.Components.Pages.Pacientes;
using ProConsulta.Extentions;
using ProConsulta.Models;
using ProConsulta.Repositories.Especialidades;
using ProConsulta.Repositories.Medicos;

namespace ProConsulta.Components.Pages.Medicos
{
    public class UpdateMedicoPage : ComponentBase
    {
        [Parameter]
        public int MedicoId { get; set; }

        [Inject]
        private IMedicoRepository repository { get; set; } = null!;

        [Inject]
        private IEspecialidadeRepository especialidadeRepository { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        public Medico? CurrentMedico { get; set; }
        public MedicoInputModel InputModel { get; set; } = new MedicoInputModel();
        public List<Especialidade> Especialidades { get; set; } = new List<Especialidade>();

        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if (editContext is null) return;

                if (editContext.Model is MedicoInputModel model)
                {
                    CurrentMedico!.Id = model.Id;
                    CurrentMedico.Nome = model.Nome;
                    CurrentMedico.Documento = model.Documento.SomenteCaracteres();
                    CurrentMedico.Crm = model.Crm.SomenteCaracteres();
                    CurrentMedico.Celular = model.Celular.SomenteCaracteres();
                    CurrentMedico.EspecialidadeId = model.EspecialidadeId;

                    await repository.UpdateAsync(CurrentMedico);
                    Snackbar.Add("Medico atualizado com sucesso", Severity.Success);
                    Navigation.NavigateTo($"/medicos");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            CurrentMedico = await repository.GetByIdAsync(MedicoId);
            Especialidades = await especialidadeRepository.GetAllAsync();

            if (CurrentMedico is null) return;

            InputModel = new MedicoInputModel
            {
                Nome = CurrentMedico.Nome,
                Crm = CurrentMedico.Crm,
                Documento = CurrentMedico.Documento,
                Celular = CurrentMedico.Celular,
                EspecialidadeId = CurrentMedico.EspecialidadeId,
            };
        }
    }

}

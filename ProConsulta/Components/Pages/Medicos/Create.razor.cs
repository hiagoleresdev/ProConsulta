using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ProConsulta.Extentions;
using ProConsulta.Models;
using ProConsulta.Repositores.Pacientes;
using ProConsulta.Repositories.Especialidades;
using ProConsulta.Repositories.Medicos;

namespace ProConsulta.Components.Pages.Medicos
{
    public class CreateMedicoPage : ComponentBase
    {
        [Inject]
        private IEspecialidadeRepository EspecialidadeRepository { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public IMedicoRepository repository { get; set; }

        public List<Especialidade> Especialidades { get; set; } = new List<Especialidade>();
        public MedicoInputModel InputModel { get; set; } = new MedicoInputModel();

        
        protected override async Task OnInitializedAsync()
        {
            Especialidades = await EspecialidadeRepository.GetAllAsync();
        }
        public async Task OnValidSubmitAsync(EditContext editContext)
        {
            try
            {
                if(editContext.Model is MedicoInputModel model)
                {
                    var medico = new Medico
                    {
                        Nome = model.Nome,
                        Documento = model.Documento.SomenteCaracteres(),
                        Celular = model.Celular.SomenteCaracteres(),
                        Crm = model.Crm.SomenteCaracteres(),
                        EspecialidadeId = model.EspecialidadeId,
                        DataCadastro = model.DataCadastro,
                    };

                    await repository.AddAsync(medico);
                    Snackbar.Add("Médico cadastrado com sucesso!", Severity.Success);
                    navigationManager.NavigateTo("/medicos");
                }
            }catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}

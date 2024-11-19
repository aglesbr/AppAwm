using AppAwm.Util;
using AppDocManager.Models;
using AppDocManager.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace AppDocManager
{
    public partial class UI02FrmRejeitar : UI00FrmTemplate
    {
        private Anexo anexo;
        DialogResult dialog;
        public UI02FrmRejeitar(Anexo _anexo)
        {
            InitializeComponent();
            anexo = _anexo;
        }

        private void UI02FrmRejeitar_Load(object sender, EventArgs e)
        {
            IconBtnCancelar.DialogResult = DialogResult.Cancel;
            IconBtnRejeitar.DialogResult = DialogResult.OK;
            
            txtNome.Text = anexo.Nome;
            txtDescricao.Text = anexo.Descricao;   
            txtTipoDocumento.Text = Utility.ListaTipoAnexo.Find(s => s.Key == anexo.TipoAnexo).Value.ToString();
        }

        private void txtMotivoRejeicao_TextChanged(object sender, EventArgs e)
        {
            lblcout.Text = $"{txtMotivoRejeicao.Text.Length}/100";
            IconBtnRejeitar.Enabled = txtMotivoRejeicao.Text.Length > 10;
        }

        private void IconBtnRejeitar_Click(object sender, EventArgs e)
        {
            string msg = $"Essa ação irá Rejeitar o documento {txtNome.Text},você confirma essa alteração?";
            dialog = MessageBox.Show(msg, "Rejeitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (dialog == DialogResult.Yes)
            {
                lblTitle.Text = "Registrando rejeição...";
                panelForm.Enabled = false;
                lblTitle.Update();

                var content = new MultipartFormDataContent
                {
                    { new StringContent(anexo.Cd_Anexo.ToString()), "Id" },
                    { new StringContent("4"), "enumStatus" },
                    { new StringContent(Utility.Usuario.Nome), "usuarioAnalista" },
                    { new StringContent($"{txtMotivoRejeicao.Text}"), "motivo" }
                };

                var response = ServiceAwm.Put("Operacao/UpdateStatus", content);
                var resposta = response.Result.EnsureSuccessStatusCode();

                if (!resposta.IsSuccessStatusCode)
                {
                    dialog = DialogResult.No;
                    return;
                }

                response = ServiceAwm.Get($"Operacao/GetUserInfAll?nameUser={anexo.Cd_UsuarioCriacao.Split('-')[0]}&idFunc={anexo.Cd_Funcionario_Id}&idEmp={anexo.Cd_Empresa_Id}");
                resposta = response.Result.EnsureSuccessStatusCode();

                if (resposta.IsSuccessStatusCode)
                {
                    string streamRejeito = response.Result.Content.ReadAsStringAsync().Result;
                    Rejeito rejeito = JsonConvert.DeserializeObject<Rejeito>(streamRejeito);
                    anexo.Descricao = txtMotivoRejeicao.Text;
                    anexo.Status =  Models.Enum.EnumStatusDocs.Rejeitado;

                    rejeito.Anexo = anexo;
                    lblTitle.Text = "Enviando e-mail para o solicitante...";
                    lblTitle.Update();

                    Utility.EnviarEmail(rejeito);
                }

                if (dialog == DialogResult.No) 
                {
                    MessageBox.Show($"Ocorreu um erro ao tentar Rejeitar do documento {anexo.Nome}\n favor contate do administrador do sistema", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                panelForm.Enabled = true;

            }
        }
    }
}

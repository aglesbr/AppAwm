using AppAwm.Util;
using AppDocManager.Models;
using AppDocManager.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace AppDocManager
{
    public partial class UI02FrmResalva : UI00FrmTemplate
    {
        private Anexo anexo;
        DialogResult dialog;
        public UI02FrmResalva(Anexo _anexo)
        {
            InitializeComponent();
            anexo = _anexo;
        }

        private void UI02FrmResalva_Load(object sender, EventArgs e)
        {
            IconBtnCancelar.DialogResult = DialogResult.Cancel;
            IconBtnAprovar.DialogResult = DialogResult.OK;
            
            txtNome.Text = anexo.Nome;
            txtDescricao.Text = anexo.Descricao;   
            txtTipoDocumento.Text = Utility.ListaTipoAnexo.Find(s => s.Key == anexo.TipoAnexo).Value.ToString();
        }

        private void txtMotivoResalva_TextChanged(object sender, EventArgs e)
        {
            lblcout.Text = $"{txtMotivoResalva.Text.Length}/100";
            IconBtnAprovar.Enabled = txtMotivoResalva.Text.Length > 10;
        }

        private void IconBtnAprovar_Click(object sender, EventArgs e)
        {
            
            lblTitle.Text = "Registrando aprovação...";
            panelForm.Enabled = false;
            lblTitle.Update();

            var content = new MultipartFormDataContent
            {
                { new StringContent(anexo.Cd_Anexo.ToString()), "Id" },
                { new StringContent("5"), "enumStatus" },/// documento com resalvas
                { new StringContent(Utility.Usuario.Nome), "usuarioAnalista" },
                { new StringContent($"{txtMotivoResalva.Text}"), "motivo" }
            };

            var response = ServiceAwm.Put("Operacao/UpdateStatus", content);
            var resposta = response.Result.EnsureSuccessStatusCode();

            if (!resposta.IsSuccessStatusCode)
            {
                dialog = DialogResult.No;
                return;
            }

            response = ServiceAwm.Get($"Operacao/GetUserInfAll?nameUser={anexo.Cd_UsuarioCriacao}&idFunc={anexo.Cd_Funcionario_Id}&idEmp={anexo.Cd_Empresa_Id}");
            resposta = response.Result.EnsureSuccessStatusCode();

            if (resposta.IsSuccessStatusCode)
            {
                string streamRejeito = response.Result.Content.ReadAsStringAsync().Result;
                Rejeito resalva = JsonConvert.DeserializeObject<Rejeito>(streamRejeito);
                anexo.Descricao = txtMotivoResalva.Text;
                resalva.Anexo = anexo;

                    lblTitle.Text = "Enviando e-mail com reslvas...";
                    lblTitle.Update();

                Utility.EnviarEmail(resalva);
            }

            if (dialog == DialogResult.No) 
            {
                MessageBox.Show($"Ocorreu um erro ao tentar salvar com resolvaso documento {anexo.Nome}\n favor contate do administrador do sistema", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            panelForm.Enabled = true;
        }
    }
}

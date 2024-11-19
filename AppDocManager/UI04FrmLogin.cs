using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using System.Net.Http;
using AppDocManager.Services;
using AppDocManager.Models;
using Newtonsoft.Json;
using AppAwm.Util;

namespace AppDocManager
{
    public partial class UI04FrmLogin : Form
    {
        public UI04FrmLogin()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
      );

        private void iconBtnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            IconBtnEnviar.Enabled = (txtLogin.TextLength > 5 && txtSenha.TextLength > 5);
        }

        private void IconBtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = !Enabled;

                var content = new MultipartFormDataContent
                {
                    { new StringContent("3"), "perfil" },
                    { new StringContent($"{txtLogin.Text}"), "userName" },
                    { new StringContent($"{txtSenha.Text}"), "password" },
                    { new StringContent("true"), "operacao" }
                };
                var response = ServiceAwm.Post("login", content);
                var resposta = response.Result.EnsureSuccessStatusCode();

                if (resposta.IsSuccessStatusCode)
                {
                    string streamUsuario = response.Result.Content.ReadAsStringAsync().Result;
                    Usuario usuario = JsonConvert.DeserializeObject<Usuario>(streamUsuario);

                    if (usuario.Cd_Usuario > 0)
                    {
                        Utility.Usuario = usuario;
                        Hide();
                        UI01FrmMain f = new UI01FrmMain();
                        f.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuário não localizado ou inativado pelo administrador", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                Enabled = !Enabled;

            }
            catch (Exception ex)
            {
                Enabled = false;
                throw new Exception("Ocorreu um erro ao tentar efetuar o login", ex);
            }
        }
    }
}

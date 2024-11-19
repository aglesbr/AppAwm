using AppAwm.Util;
using AppDocManager.Models;
using AppDocManager.Models.Enum;
using AppDocManager.Services;
using FontAwesome.Sharp;
using MaterialSkin;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Windows.Forms;

namespace AppDocManager
{
    public partial class UI01FrmMain : Form
    {
        private Form frm;
        private bool isOpen = false;
        private IconButton currentBtn;
        private string title = "Validação de Documentação Técnica";
        public static IconButton salvar;


        public UI01FrmMain()
        {
            InitializeComponent();
            salvar = IconBtnSalvar;

            panelMenu.Size = new Size(0, 1000);
            materialComboBoxEmpresa.SkinManager.ColorScheme = new ColorScheme(0, 0, 0, Accent.LightBlue700, TextShade.WHITE);
            materialComboBoxEmpresa.SkinManager.Theme = MaterialSkinManager.Themes.DARK;
            panelMsgGridEmpyt.SendToBack();
            DataGridViewAnexos.RowEnter -= DataGridViewAnexos_RowEnter;
        }

        private void UI01FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoveClickEvent(salvar);
            Application.Exit();
        }

        private void UI01FrmMain_Load(object sender, EventArgs e)
        {
            materialComboBoxEmpresa.SelectedIndexChanged -= materialComboBoxEmpresa_SelectedIndexChanged;
            comboBoxStatus.SelectedIndexChanged -= comboBoxStatus_SelectedIndexChanged;
            comboBoxTipoDocumento.SelectedIndexChanged -= comboBoxTipoDocumento_SelectedIndexChanged;

            Iniciar();

            materialComboBoxEmpresa.SelectedIndexChanged += materialComboBoxEmpresa_SelectedIndexChanged;
            comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;
            comboBoxTipoDocumento.SelectedIndexChanged += comboBoxTipoDocumento_SelectedIndexChanged;
        }

        private void IconBtnSair_Click(object sender, EventArgs e)
        {
            Utility.Usuario = null;
            panelBody.Controls.Clear();
            Application.Exit();
        }

        private void IconBtnDashBoard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            lblTitle.Text = $"{title} - DashBoard.";
            ResetForms(true);
        }

        private void BackWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // monitora os documentos com resalvas e atualiza o status;
                ServiceAwm.Put($"Operacao/UpdateValidadeResalva");

                //monitora a validade dos documentos
                ServiceAwm.Get($"Operacao/Monitor");

            }
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um erro na tentativa de monitoramento de documentos.\nComunique ao administrador do sistema.\nERRO:" + ex.Message);
            }
        }

        private void IconBtnRejeitar_Click(object sender, EventArgs e)
        {
            List<Anexo> lst = (List<Anexo>)DataGridViewAnexos.DataSource;

            Anexo anexo = lst.FirstOrDefault(f => f.Cd_Anexo == Convert.ToInt32(txtIdAnexo.Text));

            frm = new UI02FrmRejeitar(anexo);
            DialogResult dialog = frm.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                ResetForms();
                int page = Convert.ToInt16(txtGridPage.Text);
                BindingDataGrid(PesquisaDocumentos(page));
            }
        }

        private void IconBtnAprovar_Click(object sender, EventArgs e)
        {
            try
            {
                int page = Convert.ToInt16(txtGridPage.Text);
                string msg = $"A aprovação deste documento requer alguma resalva?";
                DialogResult dialog = MessageBox.Show(msg, "Aprovar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                List<Anexo> lst = (List<Anexo>)DataGridViewAnexos.DataSource;
                Anexo anexo = lst.FirstOrDefault(f => f.Cd_Anexo == Convert.ToInt32(txtIdAnexo.Text));

                if (dialog == DialogResult.Yes)
                {
                    anexo.Status = EnumStatusDocs.Resalva;
                    frm = new UI02FrmResalva(anexo);
                    dialog = frm.ShowDialog();

                    if (dialog == DialogResult.OK)
                    {
                        ResetForms();
                        BindingDataGrid(PesquisaDocumentos(page));
                    }
                    return;
                }

                if (dialog == DialogResult.No)
                {
                    Loading();
                    var content = new MultipartFormDataContent
                    {
                        { new StringContent(txtIdAnexo.Text), "Id" },
                        { new StringContent("3"), "enumStatus" },
                        { new StringContent(Utility.Usuario.Nome), "usuarioAnalista" },
                    };

                    var response = ServiceAwm.Put("Operacao/UpdateStatus", content);
                    var resposta = response.Result.EnsureSuccessStatusCode();

                    response = ServiceAwm.Get($"Operacao/GetUserInfAll?nameUser={anexo.Cd_UsuarioCriacao}&idFunc={anexo.Cd_Funcionario_Id}&idEmp={anexo.Cd_Empresa_Id}");
                    resposta = response.Result.EnsureSuccessStatusCode();

                    if (resposta.IsSuccessStatusCode)
                    {
                        string streamRejeito = response.Result.Content.ReadAsStringAsync().Result;
                        Rejeito rejeito = JsonConvert.DeserializeObject<Rejeito>(streamRejeito);
                        anexo.Status = EnumStatusDocs.Aprovado;
                        rejeito.Anexo = anexo;

                        Utility.EnviarEmail(rejeito);

                        ResetForms();
                        BindingDataGrid(PesquisaDocumentos(page));
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um erro ao tentar aprovar o documento", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    IconBtnAprovar.Enabled = IconBtnRejeitar.Enabled = IconBtnAtribuirApedidao.Enabled = false;

                    Loading();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar alerar o status:", ex);
            }
        }

        private void IconBtnAbirDocumento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdAnexo.Text))
                return;

            int id = Convert.ToInt32(txtIdAnexo.Text);
            EnumStatusDocs status = ((EnumStatusDocs)Convert.ToInt16(txtStatus.Text));

            try
            {
                var response = ServiceAwm.Get($"Operacao/GetAnexo?id={id}");
                var resposta = response.Result.EnsureSuccessStatusCode();

                if (resposta.IsSuccessStatusCode)
                {
                    string streamAnexo = response.Result.Content.ReadAsStringAsync().Result;
                    Anexo obj = JsonConvert.DeserializeObject<Anexo>(streamAnexo);

                    frm = new UI03FrmPdf(obj) { TopLevel = false };
                    panelBody.Controls.Clear();
                    panelBody.Controls.Add(frm);
                    ActivateButton(sender);
                    lblTitle.Text = $"{title} - Analisar arquivo.";
                    frm.Show();

                    if (obj.Status != status)
                    {
                        var content = new MultipartFormDataContent
                        {
                            { new StringContent(id.ToString()), "Id" },
                            { new StringContent(obj.Status.ToString()), "enumStatus" },
                            { new StringContent(Utility.Usuario.Nome), "usuarioAnalista" },
                        };

                        response = ServiceAwm.Put("Operacao/UpdateStatus", content);
                        resposta = response.Result.EnsureSuccessStatusCode();

                        int page = Convert.ToInt16(txtGridPage.Text);

                        List<Anexo> anexos = PesquisaDocumentos(page);
                        BindingDataGrid(anexos);

                        // ENVIA WHATSAPP
                        //var ret = await Task.Run(() =>  Utility.SendWhatsAppAsync(obj));
                    }

                    IconBtnAprovar.Enabled = IconBtnRejeitar.Enabled = IconBtnAtribuirApedidao.Enabled = obj.Status == EnumStatusDocs.EmAnalise;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um erro ao tentar abrir o documento", ex);
            }
        }

        private void TimerOpenCloseMenu_Tick_1(object sender, EventArgs e)
        {
            if (!isOpen)
            {
                panelMenu.Width += 30;
                if (panelMenu.Size.Width == panelMenu.MaximumSize.Width)
                {
                    TimerOpenCloseMenu.Stop();
                    isOpen = true;
                }
            }
            else
            {
                panelMenu.Width -= 30;
                if (panelMenu.Size.Width == panelMenu.MinimumSize.Width)
                {
                    TimerOpenCloseMenu.Stop();
                    isOpen = false;
                }
            }
        }

        private void IconBtnMenu_Click(object sender, EventArgs e)
        {
            TimerOpenCloseMenu.Start();
        }

        private void IconBtnMonitorar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpaFormulario(panelForm);

                Loading();

                var response = ServiceAwm.Get("Operacao/Search");
                var resposta = response.Result.EnsureSuccessStatusCode();

                if (resposta.IsSuccessStatusCode)
                {
                    string streamAnexo = response.Result.Content.ReadAsStringAsync().Result;
                    List<Anexo> anexos = JsonConvert.DeserializeObject<List<Anexo>>(streamAnexo);
                    BindingDataGrid(anexos);

                    MonitorarRAbbitMQ();

                    IconBtnMonitorar.IconColor = Color.Lime;
                    toolTipControl.ToolTipIcon = ToolTipIcon.Info;
                    toolTipControl.ToolTipTitle = "Awm";
                    toolTipControl.SetToolTip(IconBtnMonitorar, "Monitorando entrada de documentos");

                    Loading();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IconBtnAtribuirApedidao_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIdFuncionario.Text);
            frm = new UI05FrmTreinamento(id) { TopLevel = false };
            panelBody.Controls.Clear();
            panelBody.Controls.Add(frm);
            ActivateButton(sender);
            salvar.Visible = true;
            lblTitle.Text = $"{title} - Aptidões";
            frm.Show();
        }
      
        private void DataGridViewAnexos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowCount > 0)
            {
                var senderGrid = (DataGridView)sender;

                foreach (DataGridViewRow row in senderGrid.Rows)
                {
                    var item = (Anexo)row.DataBoundItem;
                    var itemRow = row.Cells.OfType<DataGridViewImageCell>().FirstOrDefault();

                    if (item.Status == EnumStatusDocs.Resalva)
                    {
                        itemRow.Value = Properties.Resources.aprovadoResalva;
                        itemRow.ToolTipText = "Doc Aprovado com Reslva";
                    }

                    if (item.Status == EnumStatusDocs.Rejeitado)
                    {
                        itemRow.Value = Properties.Resources.rejeitado;
                        itemRow.ToolTipText = "Doc Rejeitado";
                    }
                    if (item.Status == EnumStatusDocs.Aprovado)
                    {
                        itemRow.Value = Properties.Resources.aprovado;
                        itemRow.ToolTipText = "Doc Aprovado";
                    }

                    if (item.Status == EnumStatusDocs.EmAnalise)
                    {
                        itemRow.Value = Properties.Resources.analise;
                        itemRow.ToolTipText = "Em Analise";
                    }

                    if (item.Status == EnumStatusDocs.Enviado)
                    {
                        itemRow.Value = Properties.Resources.enviado;
                        itemRow.ToolTipText = "Doc Enviado";
                    }

                    if (item.Status == EnumStatusDocs.Expirado)
                    {
                        itemRow.Value = Properties.Resources.expired;
                        itemRow.ToolTipText = "Doc Expirado";
                    }
                }
            }
        }

        private void DataGridViewAnexos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var senderGrid = (DataGridView)sender;
                var obj = (Anexo)senderGrid.Rows[e.RowIndex].DataBoundItem;
                txtIdAnexo.Text = obj.Cd_Anexo.ToString();
                txtNome.Text = obj.Nome;
                txtDescricao.Text = obj.Descricao;
                txtStatus.Text = ((int)obj.Status).ToString();
                txtIdFuncionario.Text = obj.Cd_Funcionario_Id.ToString();
                txtTipoDocumento.Text = Utility.ListaTipoAnexo.FirstOrDefault(f => f.Key == obj.TipoAnexo).Value;
                IconBtnAprovar.Enabled = IconBtnRejeitar.Enabled = IconBtnAtribuirApedidao.Enabled = obj.Status == EnumStatusDocs.EmAnalise;
            }
        }

        private void materialComboBoxEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetForms();
            BindingDataGrid(PesquisaDocumentos());
        }

        private void comboBoxTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetForms();
            BindingDataGrid(PesquisaDocumentos());
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetForms();
            BindingDataGrid(PesquisaDocumentos());
        }

        private void Loading()
        {
            Invoke((MethodInvoker)delegate
            {
                lblLoad.Visible = !lblLoad.Visible;
                lblLoad.Text = "Aguarde um momentnto...";
            });
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.FromArgb(131, 205, 255);
                currentBtn.TextImageRelation = TextImageRelation.Overlay;
                currentBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                currentBtn.BackColor = Color.Transparent;
            }
        }

        private void ActivateButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;

                currentBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(241, 245, 78);
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                currentBtn.BackColor = Color.FromArgb(46, 51, 73);

                iconPictureBoxTile.IconChar = currentBtn.IconChar;
                salvar.Visible = false;
                RemoveClickEvent(salvar);
            }
        }

        void LimpaFormulario(Control control)
        {
            control.Controls.OfType<TextBox>().ToList().ForEach(t => t.Text = string.Empty);
            materialComboBoxEmpresa.SelectedIndex = 0;
        }

        void Iniciar()
        {
            //********* POPULA COMBOBOX DE EMPRESA
            #region Empresa
            var empresasResponse = ServiceAwm.Get("Operacao/GetEmpesa");
            var resposta = empresasResponse.Result.EnsureSuccessStatusCode();

            if (resposta.IsSuccessStatusCode)
            {
                string streamEmpresa = empresasResponse.Result.Content.ReadAsStringAsync().Result;

                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(streamEmpresa);
                empresas.Insert(0, new Empresa { Id = 0, Nome = "********* Filtro Pro Empresa - TODOS *********" });

                materialComboBoxEmpresa.DataSource = empresas;
                materialComboBoxEmpresa.DisplayMember = "Nome";
                materialComboBoxEmpresa.ValueMember = "Id";
            }

            #endregion

            //********* POPULA COMBOBOX DE STATUS DO DOCUMENTO
            #region Status de documento

            comboBoxStatus.DataSource = Utility.ListaStatus;
            comboBoxStatus.DisplayMember = "Value";
            comboBoxStatus.ValueMember = "Key";

            #endregion

            //********* POPULA COMBOBOX DO TIPO DO DOCUMENTO
            #region Tipo do documento

            comboBoxTipoDocumento.DataSource = Utility.ListaTipoAnexo;
            comboBoxTipoDocumento.DisplayMember = "Value";
            comboBoxTipoDocumento.ValueMember = "Key";

            #endregion

            //********* MONITORAMETNO DE DOCUMENTOS
            #region Monitoramento de documentação
            BackWorker.RunWorkerAsync();
            #endregion
        }

        void BindingButtons(int totalPage)
        {
            IconButton btn;
            flowLayoutPaginacao.Controls.Clear();
            flowLayoutPaginacao.BackColor = Color.FromArgb(24, 30, 54);

            for (int i = 1; i <= totalPage; i++)
            {
                btn = new IconButton()
                {
                    FlatStyle = iconBtnModel.FlatStyle,
                    ForeColor = iconBtnModel.ForeColor,
                    BackColor = iconBtnModel.BackColor,
                    Cursor = iconBtnModel.Cursor,
                    Size = iconBtnModel.Size,
                    Font = iconBtnModel.Font,
                    TextAlign = iconBtnModel.TextAlign,
                    Visible = true,
                    Text = i.ToString(),
                };

                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(46, 56, 79);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 51, 73);
                btn.Click += (sender, e) =>
                {
                    IconButton b = (IconButton)sender;
                    txtGridPage.Text = (Convert.ToInt16(b.Text) - 1).ToString();
                    BindingDataGrid(PesquisaDocumentos(int.Parse(b.Text) - 1));
                    { ActivateButton(b); ResetForms(true); }
                };


                flowLayoutPaginacao.Controls.Add(btn);
            }
        }

        void BindingDataGrid(List<Anexo> anexos)
        {
            if (anexos.Count == 0)
            {
                DataGridViewAnexos.DataSource = null;
                DataGridViewAnexos.Visible = false;
                return;
            }


            DataGridViewAnexos.Visible = anexos.Count > 0;
            DataGridViewAnexos.RowEnter -= DataGridViewAnexos_RowEnter;
            DataGridViewAnexos.AutoGenerateColumns = false;
            DataGridViewAnexos.DataSource = anexos;
            DataGridViewAnexos.Rows[0].Selected = false;

            DataGridViewAnexos.RowEnter += DataGridViewAnexos_RowEnter;
        }

        List<Anexo> PesquisaDocumentos(int page = 0)
        {
            int id = Convert.ToInt32(materialComboBoxEmpresa.SelectedValue), status = Convert.ToInt32(comboBoxStatus.SelectedValue);
            string tipo = comboBoxTipoDocumento.SelectedValue.ToString();

            var response = ServiceAwm.Get($"Operacao/Search?id={id}&status={status}&tipo={tipo}&pageNumber={page}");
            var resposta = response.Result.EnsureSuccessStatusCode();

            if (resposta.IsSuccessStatusCode)
            {
                string streamAnexo = response.Result.Content.ReadAsStringAsync().Result;
                List<Anexo> anexos = JsonConvert.DeserializeObject<List<Anexo>>(streamAnexo);

                if (anexos.Count > 0)
                {
                    if (anexos[0].TotalPaginas > 1)
                    {
                        if (flowLayoutPaginacao.Controls.Count <= 1)
                            BindingButtons(anexos[0].TotalPaginas);
                    }
                    else
                    {
                        txtGridPage.Text = "0";
                        flowLayoutPaginacao.BackColor = Color.FromArgb(46, 51, 73);
                    }

                    flowLayoutPaginacao.Update();
                }


                return anexos;
            }
            return null;
        }

        void ResetForms(bool hasPagination = false)
        {
            panelForm.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = string.Empty);
            IconBtnAprovar.Enabled = IconBtnRejeitar.Enabled = IconBtnAtribuirApedidao.Enabled = false;
            panelBody.Controls.Clear();

            if (!hasPagination)
            {
                DisableButton();
                flowLayoutPaginacao.Controls.Clear();
            }

        }

        //********** remove os evento Click dos botões
        #region REMOVE ENVENTO DOS BOÕES
        public static void RemoveClickEvent(Button _button)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);

            object obj = f1.GetValue(_button);
            PropertyInfo pi = _button.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList list = (EventHandlerList)pi.GetValue(_button, null);
            list.RemoveHandler(obj, list[obj]);
        }

        #endregion

        void MonitorarRAbbitMQ()
        {
            Func<IConnection> cnn = () =>
            {
                ConnectionFactory factory = new ConnectionFactory
                {
                    UserName = "guest",
                    Password = "guest",
                    HostName = "localhost",
                };

                IConnection conn = factory.CreateConnection();

                return conn;
            };

            IConnection conexao = cnn.Invoke();

            var canal = conexao.CreateModel();

            canal.ExchangeDeclare("changeMq", ExchangeType.Fanout);
            var queueName = canal.QueueDeclare(queue: "operacao", false, false, false, null).QueueName;
            canal.QueueBind(queue: queueName, exchange: "changeMq", routingKey: string.Empty);

            var consumidor = new EventingBasicConsumer(canal);

            consumidor.Received += (Modal, ea) =>
            {
                string corpo = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                var itemMq = JsonConvert.DeserializeObject<int>(corpo);

                Invoke((MethodInvoker)delegate
                {
                    if (itemMq > 0)
                    {
                        materialComboBoxEmpresa_SelectedIndexChanged(null, null);
                    }
                });
            };

            canal.BasicConsume(queue: queueName, autoAck: false, consumer: consumidor);
        }
    }
}


namespace AppDocManager
{
    partial class UI01FrmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI01FrmMain));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.txtGridPage = new System.Windows.Forms.TextBox();
            this.lblLoad = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.TimerOpenCloseMenu = new System.Windows.Forms.Timer(this.components);
            this.panelBody = new System.Windows.Forms.Panel();
            this.flowLayoutPaginacao = new System.Windows.Forms.FlowLayoutPanel();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtIdAnexo = new System.Windows.Forms.TextBox();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.panelStatusTipoDoc = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxTipoDocumento = new System.Windows.Forms.ComboBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelComboEmpresa = new System.Windows.Forms.Panel();
            this.materialComboBoxEmpresa = new MaterialSkin.Controls.MaterialComboBox();
            this.panelForm = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTipoDocumento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.panelMsgGridEmpyt = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.DataGridViewAnexos = new System.Windows.Forms.DataGridView();
            this.Cd_Funcionario_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cd_Anexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoAnexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dt_Criacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.BackWorker = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.iconBtnModel = new FontAwesome.Sharp.IconButton();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.IconBtnDashBoard = new FontAwesome.Sharp.IconButton();
            this.IconBtnMonitorar = new FontAwesome.Sharp.IconButton();
            this.IconBtnAtribuirApedidao = new FontAwesome.Sharp.IconButton();
            this.IconBtnSair = new FontAwesome.Sharp.IconButton();
            this.IconBtnRejeitar = new FontAwesome.Sharp.IconButton();
            this.IconBtnAprovar = new FontAwesome.Sharp.IconButton();
            this.IconBtnAbirDocumento = new FontAwesome.Sharp.IconButton();
            this.IconBtnSalvar = new FontAwesome.Sharp.IconButton();
            this.iconPictureBoxTile = new FontAwesome.Sharp.IconPictureBox();
            this.IconBtnMenu = new FontAwesome.Sharp.IconButton();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtIdFuncionario = new System.Windows.Forms.TextBox();
            this.panelHeader.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.flowLayoutPaginacao.SuspendLayout();
            this.panelGrid.SuspendLayout();
            this.panelStatusTipoDoc.SuspendLayout();
            this.panelComboEmpresa.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.panelMsgGridEmpyt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewAnexos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxTile)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelHeader.Controls.Add(this.IconBtnSalvar);
            this.panelHeader.Controls.Add(this.txtGridPage);
            this.panelHeader.Controls.Add(this.lblLoad);
            this.panelHeader.Controls.Add(this.iconPictureBoxTile);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.IconBtnMenu);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1823, 42);
            this.panelHeader.TabIndex = 1;
            // 
            // txtGridPage
            // 
            this.txtGridPage.Location = new System.Drawing.Point(1420, 10);
            this.txtGridPage.Name = "txtGridPage";
            this.txtGridPage.Size = new System.Drawing.Size(26, 20);
            this.txtGridPage.TabIndex = 11;
            this.txtGridPage.Text = "0";
            this.txtGridPage.Visible = false;
            // 
            // lblLoad
            // 
            this.lblLoad.AutoSize = true;
            this.lblLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblLoad.Location = new System.Drawing.Point(1210, 8);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(100, 29);
            this.lblLoad.TabIndex = 5;
            this.lblLoad.Text = "lblLoad";
            this.lblLoad.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTitle.Location = new System.Drawing.Point(1229, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(553, 23);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Validação de Documentação Técnica";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelMenu.Controls.Add(this.IconBtnDashBoard);
            this.panelMenu.Controls.Add(this.IconBtnMonitorar);
            this.panelMenu.Controls.Add(this.IconBtnAtribuirApedidao);
            this.panelMenu.Controls.Add(this.IconBtnSair);
            this.panelMenu.Controls.Add(this.IconBtnRejeitar);
            this.panelMenu.Controls.Add(this.IconBtnAprovar);
            this.panelMenu.Controls.Add(this.IconBtnAbirDocumento);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 42);
            this.panelMenu.MaximumSize = new System.Drawing.Size(200, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 718);
            this.panelMenu.TabIndex = 4;
            // 
            // TimerOpenCloseMenu
            // 
            this.TimerOpenCloseMenu.Interval = 30;
            this.TimerOpenCloseMenu.Tick += new System.EventHandler(this.TimerOpenCloseMenu_Tick_1);
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.Transparent;
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBody.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelBody.Location = new System.Drawing.Point(200, 42);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(1130, 718);
            this.panelBody.TabIndex = 3;
            // 
            // flowLayoutPaginacao
            // 
            this.flowLayoutPaginacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPaginacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.flowLayoutPaginacao.Controls.Add(this.iconBtnModel);
            this.flowLayoutPaginacao.Location = new System.Drawing.Point(6, 684);
            this.flowLayoutPaginacao.Name = "flowLayoutPaginacao";
            this.flowLayoutPaginacao.Size = new System.Drawing.Size(487, 33);
            this.flowLayoutPaginacao.TabIndex = 5;
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatus.Location = new System.Drawing.Point(100, 47);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(26, 20);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.Visible = false;
            // 
            // txtIdAnexo
            // 
            this.txtIdAnexo.Location = new System.Drawing.Point(145, 47);
            this.txtIdAnexo.Name = "txtIdAnexo";
            this.txtIdAnexo.Size = new System.Drawing.Size(26, 20);
            this.txtIdAnexo.TabIndex = 0;
            this.txtIdAnexo.Visible = false;
            // 
            // panelGrid
            // 
            this.panelGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelGrid.Controls.Add(this.flowLayoutPaginacao);
            this.panelGrid.Controls.Add(this.panelStatusTipoDoc);
            this.panelGrid.Controls.Add(this.panelComboEmpresa);
            this.panelGrid.Controls.Add(this.panelForm);
            this.panelGrid.Controls.Add(this.panelMsgGridEmpyt);
            this.panelGrid.Controls.Add(this.DataGridViewAnexos);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(1330, 42);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(493, 718);
            this.panelGrid.TabIndex = 5;
            // 
            // panelStatusTipoDoc
            // 
            this.panelStatusTipoDoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStatusTipoDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panelStatusTipoDoc.Controls.Add(this.label5);
            this.panelStatusTipoDoc.Controls.Add(this.comboBoxTipoDocumento);
            this.panelStatusTipoDoc.Controls.Add(this.comboBoxStatus);
            this.panelStatusTipoDoc.Controls.Add(this.label4);
            this.panelStatusTipoDoc.Location = new System.Drawing.Point(6, 181);
            this.panelStatusTipoDoc.Name = "panelStatusTipoDoc";
            this.panelStatusTipoDoc.Size = new System.Drawing.Size(484, 58);
            this.panelStatusTipoDoc.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(4, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Tipo do Documento";
            // 
            // comboBoxTipoDocumento
            // 
            this.comboBoxTipoDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTipoDocumento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.comboBoxTipoDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTipoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.comboBoxTipoDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBoxTipoDocumento.FormattingEnabled = true;
            this.comboBoxTipoDocumento.Location = new System.Drawing.Point(3, 23);
            this.comboBoxTipoDocumento.Name = "comboBoxTipoDocumento";
            this.comboBoxTipoDocumento.Size = new System.Drawing.Size(238, 30);
            this.comboBoxTipoDocumento.TabIndex = 10;
            this.comboBoxTipoDocumento.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipoDocumento_SelectedIndexChanged);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.comboBoxStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(247, 23);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(232, 30);
            this.comboBoxStatus.TabIndex = 12;
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatus_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(341, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Status do Documento";
            // 
            // panelComboEmpresa
            // 
            this.panelComboEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelComboEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panelComboEmpresa.Controls.Add(this.materialComboBoxEmpresa);
            this.panelComboEmpresa.Location = new System.Drawing.Point(6, 121);
            this.panelComboEmpresa.Name = "panelComboEmpresa";
            this.panelComboEmpresa.Size = new System.Drawing.Size(482, 57);
            this.panelComboEmpresa.TabIndex = 2;
            // 
            // materialComboBoxEmpresa
            // 
            this.materialComboBoxEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialComboBoxEmpresa.AutoResize = false;
            this.materialComboBoxEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.materialComboBoxEmpresa.Depth = 0;
            this.materialComboBoxEmpresa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.materialComboBoxEmpresa.DropDownHeight = 2152;
            this.materialComboBoxEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialComboBoxEmpresa.DropDownWidth = 121;
            this.materialComboBoxEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialComboBoxEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.materialComboBoxEmpresa.FormattingEnabled = true;
            this.materialComboBoxEmpresa.IntegralHeight = false;
            this.materialComboBoxEmpresa.ItemHeight = 43;
            this.materialComboBoxEmpresa.Location = new System.Drawing.Point(4, 5);
            this.materialComboBoxEmpresa.MaxDropDownItems = 50;
            this.materialComboBoxEmpresa.MouseState = MaterialSkin.MouseState.OUT;
            this.materialComboBoxEmpresa.Name = "materialComboBoxEmpresa";
            this.materialComboBoxEmpresa.Size = new System.Drawing.Size(472, 49);
            this.materialComboBoxEmpresa.StartIndex = 0;
            this.materialComboBoxEmpresa.TabIndex = 0;
            this.materialComboBoxEmpresa.SelectedIndexChanged += new System.EventHandler(this.materialComboBoxEmpresa_SelectedIndexChanged);
            // 
            // panelForm
            // 
            this.panelForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panelForm.Controls.Add(this.txtIdFuncionario);
            this.panelForm.Controls.Add(this.txtIdAnexo);
            this.panelForm.Controls.Add(this.txtStatus);
            this.panelForm.Controls.Add(this.label3);
            this.panelForm.Controls.Add(this.txtTipoDocumento);
            this.panelForm.Controls.Add(this.label2);
            this.panelForm.Controls.Add(this.txtDescricao);
            this.panelForm.Controls.Add(this.label1);
            this.panelForm.Controls.Add(this.txtNome);
            this.panelForm.Location = new System.Drawing.Point(6, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(482, 118);
            this.panelForm.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(348, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Documento";
            // 
            // txtTipoDocumento
            // 
            this.txtTipoDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTipoDocumento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtTipoDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTipoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTipoDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTipoDocumento.HideSelection = false;
            this.txtTipoDocumento.Location = new System.Drawing.Point(351, 70);
            this.txtTipoDocumento.Multiline = true;
            this.txtTipoDocumento.Name = "txtTipoDocumento";
            this.txtTipoDocumento.ReadOnly = true;
            this.txtTipoDocumento.Size = new System.Drawing.Size(124, 43);
            this.txtTipoDocumento.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(4, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDescricao.HideSelection = false;
            this.txtDescricao.Location = new System.Drawing.Point(3, 70);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.Size = new System.Drawing.Size(342, 43);
            this.txtDescricao.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(2, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Arquivo";
            // 
            // txtNome
            // 
            this.txtNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtNome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtNome.HideSelection = false;
            this.txtNome.Location = new System.Drawing.Point(0, 20);
            this.txtNome.Multiline = true;
            this.txtNome.Name = "txtNome";
            this.txtNome.ReadOnly = true;
            this.txtNome.Size = new System.Drawing.Size(475, 25);
            this.txtNome.TabIndex = 1;
            // 
            // panelMsgGridEmpyt
            // 
            this.panelMsgGridEmpyt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMsgGridEmpyt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.panelMsgGridEmpyt.Controls.Add(this.label6);
            this.panelMsgGridEmpyt.Location = new System.Drawing.Point(6, 279);
            this.panelMsgGridEmpyt.Name = "panelMsgGridEmpyt";
            this.panelMsgGridEmpyt.Size = new System.Drawing.Size(484, 38);
            this.panelMsgGridEmpyt.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(131, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(242, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nenhum item foi localizado.";
            // 
            // DataGridViewAnexos
            // 
            this.DataGridViewAnexos.AllowUserToAddRows = false;
            this.DataGridViewAnexos.AllowUserToDeleteRows = false;
            this.DataGridViewAnexos.AllowUserToResizeColumns = false;
            this.DataGridViewAnexos.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.DataGridViewAnexos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.DataGridViewAnexos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewAnexos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.DataGridViewAnexos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridViewAnexos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridViewAnexos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.DataGridViewAnexos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewAnexos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.DataGridViewAnexos.ColumnHeadersHeight = 35;
            this.DataGridViewAnexos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cd_Funcionario_Id,
            this.Cd_Anexo,
            this.Nome,
            this.Descricao,
            this.TipoAnexo,
            this.Status,
            this.Dt_Criacao});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewAnexos.DefaultCellStyle = dataGridViewCellStyle15;
            this.DataGridViewAnexos.EnableHeadersVisualStyles = false;
            this.DataGridViewAnexos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.DataGridViewAnexos.Location = new System.Drawing.Point(6, 243);
            this.DataGridViewAnexos.MultiSelect = false;
            this.DataGridViewAnexos.Name = "DataGridViewAnexos";
            this.DataGridViewAnexos.ReadOnly = true;
            this.DataGridViewAnexos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewAnexos.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.DataGridViewAnexos.RowHeadersVisible = false;
            this.DataGridViewAnexos.RowHeadersWidth = 50;
            this.DataGridViewAnexos.RowTemplate.Height = 40;
            this.DataGridViewAnexos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewAnexos.Size = new System.Drawing.Size(486, 441);
            this.DataGridViewAnexos.TabIndex = 1;
            this.DataGridViewAnexos.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewAnexos_RowEnter);
            this.DataGridViewAnexos.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataGridViewAnexos_RowsAdded);
            // 
            // Cd_Funcionario_Id
            // 
            this.Cd_Funcionario_Id.HeaderText = "Cd_Funcionario_Id";
            this.Cd_Funcionario_Id.Name = "Cd_Funcionario_Id";
            this.Cd_Funcionario_Id.ReadOnly = true;
            this.Cd_Funcionario_Id.Visible = false;
            // 
            // Cd_Anexo
            // 
            this.Cd_Anexo.DataPropertyName = "Cd_Anexo";
            this.Cd_Anexo.HeaderText = "ID";
            this.Cd_Anexo.Name = "Cd_Anexo";
            this.Cd_Anexo.ReadOnly = true;
            this.Cd_Anexo.Visible = false;
            this.Cd_Anexo.Width = 30;
            // 
            // Nome
            // 
            this.Nome.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Arquivo";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 210;
            // 
            // Descricao
            // 
            this.Descricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            // 
            // TipoAnexo
            // 
            this.TipoAnexo.DataPropertyName = "TipoAnexo";
            this.TipoAnexo.HeaderText = "Tipo";
            this.TipoAnexo.Name = "TipoAnexo";
            this.TipoAnexo.ReadOnly = true;
            this.TipoAnexo.Visible = false;
            this.TipoAnexo.Width = 50;
            // 
            // Dt_Criacao
            // 
            this.Dt_Criacao.DataPropertyName = "Dt_Criacao";
            this.Dt_Criacao.HeaderText = "Enviado";
            this.Dt_Criacao.Name = "Dt_Criacao";
            this.Dt_Criacao.ReadOnly = true;
            // 
            // BackWorker
            // 
            this.BackWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackWorker_DoWork);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "?";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.ToolTipText = "Status";
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // iconBtnModel
            // 
            this.iconBtnModel.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnModel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtnModel.FlatAppearance.BorderSize = 0;
            this.iconBtnModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.iconBtnModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.iconBtnModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iconBtnModel.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtnModel.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.iconBtnModel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnModel.IconSize = 32;
            this.iconBtnModel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconBtnModel.Location = new System.Drawing.Point(3, 3);
            this.iconBtnModel.Name = "iconBtnModel";
            this.iconBtnModel.Size = new System.Drawing.Size(24, 30);
            this.iconBtnModel.TabIndex = 4;
            this.iconBtnModel.Text = "1";
            this.iconBtnModel.UseVisualStyleBackColor = false;
            this.iconBtnModel.Visible = false;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Image = ((System.Drawing.Image)(resources.GetObject("Status.Image")));
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Status.ToolTipText = "Status";
            this.Status.Width = 50;
            // 
            // IconBtnDashBoard
            // 
            this.IconBtnDashBoard.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnDashBoard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnDashBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconBtnDashBoard.FlatAppearance.BorderSize = 0;
            this.IconBtnDashBoard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnDashBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnDashBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnDashBoard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnDashBoard.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            this.IconBtnDashBoard.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.IconBtnDashBoard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnDashBoard.IconSize = 32;
            this.IconBtnDashBoard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnDashBoard.Location = new System.Drawing.Point(0, 215);
            this.IconBtnDashBoard.Name = "IconBtnDashBoard";
            this.IconBtnDashBoard.Size = new System.Drawing.Size(200, 43);
            this.IconBtnDashBoard.TabIndex = 5;
            this.IconBtnDashBoard.Text = "Dashboard ";
            this.IconBtnDashBoard.UseVisualStyleBackColor = false;
            this.IconBtnDashBoard.Click += new System.EventHandler(this.IconBtnDashBoard_Click);
            // 
            // IconBtnMonitorar
            // 
            this.IconBtnMonitorar.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnMonitorar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnMonitorar.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconBtnMonitorar.FlatAppearance.BorderSize = 0;
            this.IconBtnMonitorar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnMonitorar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnMonitorar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnMonitorar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnMonitorar.IconChar = FontAwesome.Sharp.IconChar.Buffer;
            this.IconBtnMonitorar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.IconBtnMonitorar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnMonitorar.IconSize = 32;
            this.IconBtnMonitorar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnMonitorar.Location = new System.Drawing.Point(0, 172);
            this.IconBtnMonitorar.Name = "IconBtnMonitorar";
            this.IconBtnMonitorar.Size = new System.Drawing.Size(200, 43);
            this.IconBtnMonitorar.TabIndex = 4;
            this.IconBtnMonitorar.Text = "Monitorar Documento";
            this.IconBtnMonitorar.UseVisualStyleBackColor = false;
            this.IconBtnMonitorar.Click += new System.EventHandler(this.IconBtnMonitorar_Click);
            // 
            // IconBtnAtribuirApedidao
            // 
            this.IconBtnAtribuirApedidao.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnAtribuirApedidao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnAtribuirApedidao.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconBtnAtribuirApedidao.Enabled = false;
            this.IconBtnAtribuirApedidao.FlatAppearance.BorderSize = 0;
            this.IconBtnAtribuirApedidao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnAtribuirApedidao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnAtribuirApedidao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnAtribuirApedidao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnAtribuirApedidao.IconChar = FontAwesome.Sharp.IconChar.ChartGantt;
            this.IconBtnAtribuirApedidao.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.IconBtnAtribuirApedidao.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnAtribuirApedidao.IconSize = 32;
            this.IconBtnAtribuirApedidao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnAtribuirApedidao.Location = new System.Drawing.Point(0, 129);
            this.IconBtnAtribuirApedidao.Name = "IconBtnAtribuirApedidao";
            this.IconBtnAtribuirApedidao.Size = new System.Drawing.Size(200, 43);
            this.IconBtnAtribuirApedidao.TabIndex = 6;
            this.IconBtnAtribuirApedidao.Text = "Atribuir Aptidões";
            this.IconBtnAtribuirApedidao.UseVisualStyleBackColor = false;
            this.IconBtnAtribuirApedidao.Click += new System.EventHandler(this.IconBtnAtribuirApedidao_Click);
            // 
            // IconBtnSair
            // 
            this.IconBtnSair.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnSair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.IconBtnSair.FlatAppearance.BorderSize = 0;
            this.IconBtnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnSair.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnSair.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.IconBtnSair.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.IconBtnSair.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnSair.IconSize = 32;
            this.IconBtnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnSair.Location = new System.Drawing.Point(0, 675);
            this.IconBtnSair.Name = "IconBtnSair";
            this.IconBtnSair.Size = new System.Drawing.Size(200, 43);
            this.IconBtnSair.TabIndex = 3;
            this.IconBtnSair.Text = "Sair e Fechar";
            this.IconBtnSair.UseVisualStyleBackColor = false;
            this.IconBtnSair.Click += new System.EventHandler(this.IconBtnSair_Click);
            // 
            // IconBtnRejeitar
            // 
            this.IconBtnRejeitar.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnRejeitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnRejeitar.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconBtnRejeitar.Enabled = false;
            this.IconBtnRejeitar.FlatAppearance.BorderSize = 0;
            this.IconBtnRejeitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnRejeitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnRejeitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnRejeitar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnRejeitar.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            this.IconBtnRejeitar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.IconBtnRejeitar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnRejeitar.IconSize = 32;
            this.IconBtnRejeitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnRejeitar.Location = new System.Drawing.Point(0, 86);
            this.IconBtnRejeitar.Name = "IconBtnRejeitar";
            this.IconBtnRejeitar.Size = new System.Drawing.Size(200, 43);
            this.IconBtnRejeitar.TabIndex = 2;
            this.IconBtnRejeitar.Text = "Reprovar Documento";
            this.IconBtnRejeitar.UseVisualStyleBackColor = false;
            this.IconBtnRejeitar.Click += new System.EventHandler(this.IconBtnRejeitar_Click);
            // 
            // IconBtnAprovar
            // 
            this.IconBtnAprovar.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnAprovar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnAprovar.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconBtnAprovar.Enabled = false;
            this.IconBtnAprovar.FlatAppearance.BorderSize = 0;
            this.IconBtnAprovar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnAprovar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnAprovar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnAprovar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnAprovar.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.IconBtnAprovar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.IconBtnAprovar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnAprovar.IconSize = 32;
            this.IconBtnAprovar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnAprovar.Location = new System.Drawing.Point(0, 43);
            this.IconBtnAprovar.Name = "IconBtnAprovar";
            this.IconBtnAprovar.Size = new System.Drawing.Size(200, 43);
            this.IconBtnAprovar.TabIndex = 1;
            this.IconBtnAprovar.Text = " Aprovar Documento";
            this.IconBtnAprovar.UseVisualStyleBackColor = false;
            this.IconBtnAprovar.Click += new System.EventHandler(this.IconBtnAprovar_Click);
            // 
            // IconBtnAbirDocumento
            // 
            this.IconBtnAbirDocumento.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnAbirDocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnAbirDocumento.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconBtnAbirDocumento.FlatAppearance.BorderSize = 0;
            this.IconBtnAbirDocumento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnAbirDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnAbirDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnAbirDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnAbirDocumento.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.IconBtnAbirDocumento.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.IconBtnAbirDocumento.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.IconBtnAbirDocumento.IconSize = 32;
            this.IconBtnAbirDocumento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnAbirDocumento.Location = new System.Drawing.Point(0, 0);
            this.IconBtnAbirDocumento.Name = "IconBtnAbirDocumento";
            this.IconBtnAbirDocumento.Size = new System.Drawing.Size(200, 43);
            this.IconBtnAbirDocumento.TabIndex = 0;
            this.IconBtnAbirDocumento.Text = "Abrir Documento";
            this.IconBtnAbirDocumento.UseVisualStyleBackColor = false;
            this.IconBtnAbirDocumento.Click += new System.EventHandler(this.IconBtnAbirDocumento_Click);
            // 
            // IconBtnSalvar
            // 
            this.IconBtnSalvar.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnSalvar.FlatAppearance.BorderSize = 0;
            this.IconBtnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnSalvar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnSalvar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.IconBtnSalvar.IconColor = System.Drawing.Color.Lime;
            this.IconBtnSalvar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnSalvar.IconSize = 32;
            this.IconBtnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnSalvar.Location = new System.Drawing.Point(200, 3);
            this.IconBtnSalvar.Name = "IconBtnSalvar";
            this.IconBtnSalvar.Size = new System.Drawing.Size(100, 36);
            this.IconBtnSalvar.TabIndex = 5;
            this.IconBtnSalvar.Text = "Salvar";
            this.IconBtnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconBtnSalvar.UseVisualStyleBackColor = false;
            this.IconBtnSalvar.Visible = false;
            // 
            // iconPictureBoxTile
            // 
            this.iconPictureBoxTile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBoxTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.iconPictureBoxTile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iconPictureBoxTile.IconChar = FontAwesome.Sharp.IconChar.PeopleLine;
            this.iconPictureBoxTile.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iconPictureBoxTile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBoxTile.Location = new System.Drawing.Point(1788, 3);
            this.iconPictureBoxTile.Name = "iconPictureBoxTile";
            this.iconPictureBoxTile.Size = new System.Drawing.Size(32, 32);
            this.iconPictureBoxTile.TabIndex = 4;
            this.iconPictureBoxTile.TabStop = false;
            // 
            // IconBtnMenu
            // 
            this.IconBtnMenu.BackColor = System.Drawing.Color.Transparent;
            this.IconBtnMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.IconBtnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnMenu.FlatAppearance.BorderSize = 0;
            this.IconBtnMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.IconBtnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnMenu.ForeColor = System.Drawing.SystemColors.Control;
            this.IconBtnMenu.IconChar = FontAwesome.Sharp.IconChar.Navicon;
            this.IconBtnMenu.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(181)))), ((int)(((byte)(239)))));
            this.IconBtnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnMenu.IconSize = 32;
            this.IconBtnMenu.Location = new System.Drawing.Point(12, 3);
            this.IconBtnMenu.Name = "IconBtnMenu";
            this.IconBtnMenu.Size = new System.Drawing.Size(39, 36);
            this.IconBtnMenu.TabIndex = 2;
            this.IconBtnMenu.UseVisualStyleBackColor = false;
            this.IconBtnMenu.Click += new System.EventHandler(this.IconBtnMenu_Click);
            // 
            // iconButton5
            // 
            this.iconButton5.BackColor = System.Drawing.Color.Transparent;
            this.iconButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconButton5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iconButton5.FlatAppearance.BorderSize = 0;
            this.iconButton5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iconButton5.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton5.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton5.IconSize = 32;
            this.iconButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.Location = new System.Drawing.Point(0, 589);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Size = new System.Drawing.Size(180, 43);
            this.iconButton5.TabIndex = 5;
            this.iconButton5.Text = "Dashboard e Analise";
            this.iconButton5.UseVisualStyleBackColor = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // txtIdFuncionario
            // 
            this.txtIdFuncionario.Location = new System.Drawing.Point(188, 47);
            this.txtIdFuncionario.Name = "txtIdFuncionario";
            this.txtIdFuncionario.Size = new System.Drawing.Size(26, 20);
            this.txtIdFuncionario.TabIndex = 11;
            this.txtIdFuncionario.Visible = false;
            // 
            // UI01FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1823, 760);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelHeader);
            this.Name = "UI01FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UI01FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.UI01FrmMain_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.flowLayoutPaginacao.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            this.panelStatusTipoDoc.ResumeLayout(false);
            this.panelStatusTipoDoc.PerformLayout();
            this.panelComboEmpresa.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.panelMsgGridEmpyt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewAnexos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxTile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Timer TimerOpenCloseMenu;
        private FontAwesome.Sharp.IconButton IconBtnMenu;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelGrid;
        private FontAwesome.Sharp.IconButton IconBtnAbirDocumento;
        private FontAwesome.Sharp.IconButton IconBtnRejeitar;
        private FontAwesome.Sharp.IconButton IconBtnAprovar;
        private System.Windows.Forms.Label lblTitle;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxTile;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.DataGridView DataGridViewAnexos;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private FontAwesome.Sharp.IconButton IconBtnSair;
        private FontAwesome.Sharp.IconButton IconBtnDashBoard;
        private FontAwesome.Sharp.IconButton IconBtnMonitorar;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTipoDocumento;
        private System.Windows.Forms.Panel panelComboEmpresa;
        private MaterialSkin.Controls.MaterialComboBox materialComboBoxEmpresa;
        private System.Windows.Forms.TextBox txtIdAnexo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cd_Funcionario_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cd_Anexo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoAnexo;
        private System.Windows.Forms.DataGridViewImageColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dt_Criacao;
        private System.Windows.Forms.TextBox txtStatus;
        private FontAwesome.Sharp.IconButton iconButton5;
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.Panel panelStatusTipoDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxTipoDocumento;
        private FontAwesome.Sharp.IconButton iconBtnModel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPaginacao;
        private System.Windows.Forms.Panel panelMsgGridEmpyt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGridPage;
        private System.ComponentModel.BackgroundWorker BackWorker;
        private FontAwesome.Sharp.IconButton IconBtnAtribuirApedidao;
        private FontAwesome.Sharp.IconButton IconBtnSalvar;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox txtIdFuncionario;
    }
}


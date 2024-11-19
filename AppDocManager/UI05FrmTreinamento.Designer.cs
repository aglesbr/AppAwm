namespace AppDocManager
{
    partial class UI05FrmTreinamento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI05FrmTreinamento));
            this.TreeViewTreinamento = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.materialChkEstrangeiro = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialChkPcd = new MaterialSkin.Controls.MaterialCheckbox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.pictureFoto = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFoto)).BeginInit();
            this.SuspendLayout();
            // 
            // TreeViewTreinamento
            // 
            this.TreeViewTreinamento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeViewTreinamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.TreeViewTreinamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TreeViewTreinamento.CheckBoxes = true;
            this.TreeViewTreinamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TreeViewTreinamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeViewTreinamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TreeViewTreinamento.FullRowSelect = true;
            this.TreeViewTreinamento.ItemHeight = 25;
            this.TreeViewTreinamento.Location = new System.Drawing.Point(12, 67);
            this.TreeViewTreinamento.Margin = new System.Windows.Forms.Padding(5);
            this.TreeViewTreinamento.Name = "TreeViewTreinamento";
            this.TreeViewTreinamento.Size = new System.Drawing.Size(438, 718);
            this.TreeViewTreinamento.TabIndex = 0;
            this.TreeViewTreinamento.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewTreinamento_AfterCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Atribuir Aptidões ao Colaborador";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtDescricao);
            this.panel1.Controls.Add(this.materialChkEstrangeiro);
            this.panel1.Controls.Add(this.materialChkPcd);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtEmpresa);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCargo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNome);
            this.panel1.Controls.Add(this.pictureFoto);
            this.panel1.Location = new System.Drawing.Point(467, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 281);
            this.panel1.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(188, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDescricao.HideSelection = false;
            this.txtDescricao.Location = new System.Drawing.Point(187, 212);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.Size = new System.Drawing.Size(382, 43);
            this.txtDescricao.TabIndex = 17;
            // 
            // materialChkEstrangeiro
            // 
            this.materialChkEstrangeiro.AutoSize = true;
            this.materialChkEstrangeiro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.materialChkEstrangeiro.Depth = 0;
            this.materialChkEstrangeiro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.materialChkEstrangeiro.Location = new System.Drawing.Point(15, 229);
            this.materialChkEstrangeiro.Margin = new System.Windows.Forms.Padding(0);
            this.materialChkEstrangeiro.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialChkEstrangeiro.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialChkEstrangeiro.Name = "materialChkEstrangeiro";
            this.materialChkEstrangeiro.ReadOnly = true;
            this.materialChkEstrangeiro.Ripple = true;
            this.materialChkEstrangeiro.Size = new System.Drawing.Size(115, 37);
            this.materialChkEstrangeiro.TabIndex = 16;
            this.materialChkEstrangeiro.Text = "Estrangeiro";
            this.materialChkEstrangeiro.UseVisualStyleBackColor = false;
            // 
            // materialChkPcd
            // 
            this.materialChkPcd.AutoSize = true;
            this.materialChkPcd.Depth = 0;
            this.materialChkPcd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.materialChkPcd.Location = new System.Drawing.Point(15, 192);
            this.materialChkPcd.Margin = new System.Windows.Forms.Padding(0);
            this.materialChkPcd.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialChkPcd.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialChkPcd.Name = "materialChkPcd";
            this.materialChkPcd.ReadOnly = true;
            this.materialChkPcd.Ripple = true;
            this.materialChkPcd.Size = new System.Drawing.Size(66, 37);
            this.materialChkPcd.TabIndex = 15;
            this.materialChkPcd.Text = "PCD";
            this.materialChkPcd.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(184, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Empresa";
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtEmpresa.HideSelection = false;
            this.txtEmpresa.Location = new System.Drawing.Point(187, 149);
            this.txtEmpresa.Multiline = true;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.ReadOnly = true;
            this.txtEmpresa.Size = new System.Drawing.Size(382, 25);
            this.txtEmpresa.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(184, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Cargo";
            // 
            // txtCargo
            // 
            this.txtCargo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCargo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtCargo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCargo.HideSelection = false;
            this.txtCargo.Location = new System.Drawing.Point(187, 93);
            this.txtCargo.Multiline = true;
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.ReadOnly = true;
            this.txtCargo.Size = new System.Drawing.Size(382, 25);
            this.txtCargo.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(184, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nome";
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
            this.txtNome.Location = new System.Drawing.Point(187, 38);
            this.txtNome.Multiline = true;
            this.txtNome.Name = "txtNome";
            this.txtNome.ReadOnly = true;
            this.txtNome.Size = new System.Drawing.Size(384, 25);
            this.txtNome.TabIndex = 9;
            // 
            // pictureFoto
            // 
            this.pictureFoto.Image = ((System.Drawing.Image)(resources.GetObject("pictureFoto.Image")));
            this.pictureFoto.Location = new System.Drawing.Point(15, 19);
            this.pictureFoto.Name = "pictureFoto";
            this.pictureFoto.Size = new System.Drawing.Size(147, 157);
            this.pictureFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureFoto.TabIndex = 0;
            this.pictureFoto.TabStop = false;
            // 
            // UI05FrmTreinamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 797);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TreeViewTreinamento);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UI05FrmTreinamento";
            this.Text = "UI05FrmTreinamento";
            this.Load += new System.EventHandler(this.UI05FrmTreinamento_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView TreeViewTreinamento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureFoto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private MaterialSkin.Controls.MaterialCheckbox materialChkPcd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmpresa;
        private MaterialSkin.Controls.MaterialCheckbox materialChkEstrangeiro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescricao;
    }
}
namespace AppDocManager
{
    partial class UI02FrmResalva
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.lblcout = new System.Windows.Forms.Label();
            this.IconBtnCancelar = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.IconBtnAprovar = new FontAwesome.Sharp.IconButton();
            this.txtMotivoResalva = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTipoDocumento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 49);
            this.panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(675, 25);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Informar Resalva do Documento";
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForm.Controls.Add(this.lblcout);
            this.panelForm.Controls.Add(this.IconBtnCancelar);
            this.panelForm.Controls.Add(this.label4);
            this.panelForm.Controls.Add(this.IconBtnAprovar);
            this.panelForm.Controls.Add(this.txtMotivoResalva);
            this.panelForm.Controls.Add(this.label3);
            this.panelForm.Controls.Add(this.txtTipoDocumento);
            this.panelForm.Controls.Add(this.label2);
            this.panelForm.Controls.Add(this.txtDescricao);
            this.panelForm.Controls.Add(this.label1);
            this.panelForm.Controls.Add(this.txtNome);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 49);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(725, 324);
            this.panelForm.TabIndex = 1;
            // 
            // lblcout
            // 
            this.lblcout.AutoSize = true;
            this.lblcout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblcout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblcout.Location = new System.Drawing.Point(26, 241);
            this.lblcout.Name = "lblcout";
            this.lblcout.Size = new System.Drawing.Size(39, 16);
            this.lblcout.TabIndex = 13;
            this.lblcout.Text = "0/100";
            // 
            // IconBtnCancelar
            // 
            this.IconBtnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.IconBtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnCancelar.FlatAppearance.BorderSize = 0;
            this.IconBtnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.IconBtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnCancelar.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            this.IconBtnCancelar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.IconBtnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnCancelar.IconSize = 36;
            this.IconBtnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnCancelar.Location = new System.Drawing.Point(200, 257);
            this.IconBtnCancelar.Name = "IconBtnCancelar";
            this.IconBtnCancelar.Size = new System.Drawing.Size(190, 52);
            this.IconBtnCancelar.TabIndex = 4;
            this.IconBtnCancelar.Text = "Cancelar";
            this.IconBtnCancelar.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(21, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Justificar resalva";
            // 
            // IconBtnAprovar
            // 
            this.IconBtnAprovar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.IconBtnAprovar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnAprovar.Enabled = false;
            this.IconBtnAprovar.FlatAppearance.BorderSize = 0;
            this.IconBtnAprovar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.IconBtnAprovar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnAprovar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconBtnAprovar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnAprovar.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.IconBtnAprovar.IconColor = System.Drawing.Color.Yellow;
            this.IconBtnAprovar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnAprovar.IconSize = 36;
            this.IconBtnAprovar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnAprovar.Location = new System.Drawing.Point(421, 257);
            this.IconBtnAprovar.Name = "IconBtnAprovar";
            this.IconBtnAprovar.Size = new System.Drawing.Size(278, 52);
            this.IconBtnAprovar.TabIndex = 3;
            this.IconBtnAprovar.Text = "Aprovar com resalva";
            this.IconBtnAprovar.UseVisualStyleBackColor = false;
            this.IconBtnAprovar.Click += new System.EventHandler(this.IconBtnAprovar_Click);
            // 
            // txtMotivoResalva
            // 
            this.txtMotivoResalva.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMotivoResalva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtMotivoResalva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMotivoResalva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMotivoResalva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMotivoResalva.HideSelection = false;
            this.txtMotivoResalva.Location = new System.Drawing.Point(24, 182);
            this.txtMotivoResalva.MaxLength = 100;
            this.txtMotivoResalva.Multiline = true;
            this.txtMotivoResalva.Name = "txtMotivoResalva";
            this.txtMotivoResalva.Size = new System.Drawing.Size(675, 56);
            this.txtMotivoResalva.TabIndex = 0;
            this.txtMotivoResalva.TextChanged += new System.EventHandler(this.txtMotivoResalva_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(323, 69);
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
            this.txtTipoDocumento.Location = new System.Drawing.Point(326, 88);
            this.txtTipoDocumento.Multiline = true;
            this.txtTipoDocumento.Name = "txtTipoDocumento";
            this.txtTipoDocumento.ReadOnly = true;
            this.txtTipoDocumento.Size = new System.Drawing.Size(373, 56);
            this.txtTipoDocumento.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(21, 69);
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
            this.txtDescricao.Location = new System.Drawing.Point(24, 88);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.Size = new System.Drawing.Size(286, 56);
            this.txtDescricao.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(21, 7);
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
            this.txtNome.Location = new System.Drawing.Point(24, 26);
            this.txtNome.Multiline = true;
            this.txtNome.Name = "txtNome";
            this.txtNome.ReadOnly = true;
            this.txtNome.Size = new System.Drawing.Size(675, 25);
            this.txtNome.TabIndex = 1;
            // 
            // UI02FrmResalva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(725, 373);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UI02FrmResalva";
            this.ShowInTaskbar = false;
            this.Text = "UI02FrmRejeitar";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.UI02FrmResalva_Load);
            this.panel1.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTipoDocumento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMotivoResalva;
        private FontAwesome.Sharp.IconButton IconBtnAprovar;
        private FontAwesome.Sharp.IconButton IconBtnCancelar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblcout;
    }
}
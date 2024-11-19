namespace AppDocManager
{
    partial class UI04FrmLogin
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
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconBtnFechar = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.IconBtnEnviar = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogin
            // 
            this.txtLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtLogin.HideSelection = false;
            this.txtLogin.Location = new System.Drawing.Point(35, 104);
            this.txtLogin.MaxLength = 50;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(527, 44);
            this.txtLogin.TabIndex = 0;
            this.txtLogin.WordWrap = false;
            this.txtLogin.TextChanged += new System.EventHandler(this.txtLogin_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Agency FB", 25.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(27, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "Login de Acesso";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.Controls.Add(this.iconBtnFechar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 48);
            this.panel1.TabIndex = 4;
            // 
            // iconBtnFechar
            // 
            this.iconBtnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.iconBtnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtnFechar.FlatAppearance.BorderSize = 0;
            this.iconBtnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnFechar.Font = new System.Drawing.Font("Arial Narrow", 18.25F, System.Drawing.FontStyle.Bold);
            this.iconBtnFechar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iconBtnFechar.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.iconBtnFechar.IconColor = System.Drawing.Color.Salmon;
            this.iconBtnFechar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnFechar.IconSize = 42;
            this.iconBtnFechar.Location = new System.Drawing.Point(552, 4);
            this.iconBtnFechar.Name = "iconBtnFechar";
            this.iconBtnFechar.Size = new System.Drawing.Size(40, 40);
            this.iconBtnFechar.TabIndex = 8;
            this.iconBtnFechar.TabStop = false;
            this.iconBtnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconBtnFechar.UseVisualStyleBackColor = false;
            this.iconBtnFechar.Click += new System.EventHandler(this.iconBtnFechar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(29, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 34);
            this.label2.TabIndex = 4;
            this.label2.Text = "Usuário";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(29, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 34);
            this.label3.TabIndex = 6;
            this.label3.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtSenha.HideSelection = false;
            this.txtSenha.Location = new System.Drawing.Point(35, 202);
            this.txtSenha.MaxLength = 50;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(527, 44);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.WordWrap = false;
            this.txtSenha.TextChanged += new System.EventHandler(this.txtLogin_TextChanged);
            // 
            // IconBtnEnviar
            // 
            this.IconBtnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.IconBtnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconBtnEnviar.Enabled = false;
            this.IconBtnEnviar.FlatAppearance.BorderSize = 0;
            this.IconBtnEnviar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.IconBtnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconBtnEnviar.Font = new System.Drawing.Font("Arial Narrow", 18.25F, System.Drawing.FontStyle.Bold);
            this.IconBtnEnviar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IconBtnEnviar.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.IconBtnEnviar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(181)))), ((int)(((byte)(239)))));
            this.IconBtnEnviar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconBtnEnviar.IconSize = 42;
            this.IconBtnEnviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconBtnEnviar.Location = new System.Drawing.Point(34, 269);
            this.IconBtnEnviar.Name = "IconBtnEnviar";
            this.IconBtnEnviar.Size = new System.Drawing.Size(153, 50);
            this.IconBtnEnviar.TabIndex = 3;
            this.IconBtnEnviar.Text = "Enviar...";
            this.IconBtnEnviar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconBtnEnviar.UseVisualStyleBackColor = false;
            this.IconBtnEnviar.Click += new System.EventHandler(this.IconBtnEnviar_Click);
            // 
            // UI04FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(595, 331);
            this.Controls.Add(this.IconBtnEnviar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtLogin);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UI04FrmLogin";
            this.Opacity = 0.88D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI04FrmLogin";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSenha;
        private FontAwesome.Sharp.IconButton IconBtnEnviar;
        private FontAwesome.Sharp.IconButton iconBtnFechar;
    }
}
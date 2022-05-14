namespace GeradorTestes.WinApp
{
    partial class TelaCadastroTesteForm
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
            this.btnGerarPDF = new System.Windows.Forms.Button();
            this.btnGabarito = new System.Windows.Forms.Button();
            this.tbDisciplina = new System.Windows.Forms.TextBox();
            this.lblDisciplina = new System.Windows.Forms.Label();
            this.lblMateria = new System.Windows.Forms.Label();
            this.cbMateria = new System.Windows.Forms.ComboBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.cbSerie = new System.Windows.Forms.ComboBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.cbNumeroQsts = new System.Windows.Forms.ComboBox();
            this.btnQuestao = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rtTeste = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.btnGerarPDF);
            this.panel1.Controls.Add(this.btnGabarito);
            this.panel1.Controls.Add(this.tbDisciplina);
            this.panel1.Controls.Add(this.lblDisciplina);
            this.panel1.Controls.Add(this.lblMateria);
            this.panel1.Controls.Add(this.cbMateria);
            this.panel1.Controls.Add(this.lblSerie);
            this.panel1.Controls.Add(this.cbSerie);
            this.panel1.Controls.Add(this.lblNumero);
            this.panel1.Controls.Add(this.cbNumeroQsts);
            this.panel1.Controls.Add(this.btnQuestao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1355, 112);
            this.panel1.TabIndex = 0;
            // 
            // btnGerarPDF
            // 
            this.btnGerarPDF.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGerarPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarPDF.Enabled = false;
            this.btnGerarPDF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGerarPDF.Location = new System.Drawing.Point(1052, 29);
            this.btnGerarPDF.Name = "btnGerarPDF";
            this.btnGerarPDF.Size = new System.Drawing.Size(144, 59);
            this.btnGerarPDF.TabIndex = 11;
            this.btnGerarPDF.Text = "Gerar PDF";
            this.btnGerarPDF.UseVisualStyleBackColor = false;
            this.btnGerarPDF.Click += new System.EventHandler(this.btnGerarPDF_Click);
            // 
            // btnGabarito
            // 
            this.btnGabarito.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGabarito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGabarito.Enabled = false;
            this.btnGabarito.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGabarito.Location = new System.Drawing.Point(902, 29);
            this.btnGabarito.Name = "btnGabarito";
            this.btnGabarito.Size = new System.Drawing.Size(144, 59);
            this.btnGabarito.TabIndex = 10;
            this.btnGabarito.Text = "Gerar Gabarito";
            this.btnGabarito.UseVisualStyleBackColor = false;
            this.btnGabarito.Click += new System.EventHandler(this.btnGabarito_Click);
            // 
            // tbDisciplina
            // 
            this.tbDisciplina.Location = new System.Drawing.Point(464, 53);
            this.tbDisciplina.Multiline = true;
            this.tbDisciplina.Name = "tbDisciplina";
            this.tbDisciplina.ReadOnly = true;
            this.tbDisciplina.Size = new System.Drawing.Size(261, 35);
            this.tbDisciplina.TabIndex = 9;
            // 
            // lblDisciplina
            // 
            this.lblDisciplina.AutoSize = true;
            this.lblDisciplina.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDisciplina.Location = new System.Drawing.Point(464, 29);
            this.lblDisciplina.Name = "lblDisciplina";
            this.lblDisciplina.Size = new System.Drawing.Size(77, 21);
            this.lblDisciplina.TabIndex = 8;
            this.lblDisciplina.Text = "Disciplina";
            // 
            // lblMateria
            // 
            this.lblMateria.AutoSize = true;
            this.lblMateria.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMateria.Location = new System.Drawing.Point(273, 29);
            this.lblMateria.Name = "lblMateria";
            this.lblMateria.Size = new System.Drawing.Size(63, 21);
            this.lblMateria.TabIndex = 6;
            this.lblMateria.Text = "Matéria";
            // 
            // cbMateria
            // 
            this.cbMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMateria.Enabled = false;
            this.cbMateria.FormattingEnabled = true;
            this.cbMateria.Items.AddRange(new object[] {
            "05",
            "10",
            "15",
            "20"});
            this.cbMateria.Location = new System.Drawing.Point(273, 55);
            this.cbMateria.Name = "cbMateria";
            this.cbMateria.Size = new System.Drawing.Size(185, 33);
            this.cbMateria.TabIndex = 5;
            this.cbMateria.SelectedIndexChanged += new System.EventHandler(this.cbMateria_SelectedIndexChanged);
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSerie.Location = new System.Drawing.Point(143, 29);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(45, 21);
            this.lblSerie.TabIndex = 4;
            this.lblSerie.Text = "Série";
            // 
            // cbSerie
            // 
            this.cbSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerie.FormattingEnabled = true;
            this.cbSerie.Items.AddRange(new object[] {
            "Primeira",
            "Segunda"});
            this.cbSerie.Location = new System.Drawing.Point(143, 55);
            this.cbSerie.Margin = new System.Windows.Forms.Padding(6);
            this.cbSerie.Name = "cbSerie";
            this.cbSerie.Size = new System.Drawing.Size(121, 33);
            this.cbSerie.TabIndex = 3;
            this.cbSerie.SelectedIndexChanged += new System.EventHandler(this.cbSerie_SelectedIndexChanged);
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNumero.Location = new System.Drawing.Point(12, 29);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(98, 21);
            this.lblNumero.TabIndex = 2;
            this.lblNumero.Text = "Nº Questões";
            // 
            // cbNumeroQsts
            // 
            this.cbNumeroQsts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNumeroQsts.FormattingEnabled = true;
            this.cbNumeroQsts.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20"});
            this.cbNumeroQsts.Location = new System.Drawing.Point(12, 55);
            this.cbNumeroQsts.Name = "cbNumeroQsts";
            this.cbNumeroQsts.Size = new System.Drawing.Size(102, 33);
            this.cbNumeroQsts.TabIndex = 1;
            // 
            // btnQuestao
            // 
            this.btnQuestao.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnQuestao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuestao.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuestao.Location = new System.Drawing.Point(752, 29);
            this.btnQuestao.Name = "btnQuestao";
            this.btnQuestao.Size = new System.Drawing.Size(144, 59);
            this.btnQuestao.TabIndex = 0;
            this.btnQuestao.Text = "Gerar Questões";
            this.btnQuestao.UseVisualStyleBackColor = false;
            this.btnQuestao.Click += new System.EventHandler(this.btnQuestao_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.btnLimpar);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 499);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1355, 78);
            this.panel2.TabIndex = 1;
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.Enabled = false;
            this.btnLimpar.Location = new System.Drawing.Point(152, 14);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(112, 52);
            this.btnLimpar.TabIndex = 1;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(23, 14);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 52);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rtTeste
            // 
            this.rtTeste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtTeste.Location = new System.Drawing.Point(0, 112);
            this.rtTeste.Name = "rtTeste";
            this.rtTeste.Size = new System.Drawing.Size(1355, 387);
            this.rtTeste.TabIndex = 2;
            this.rtTeste.Text = "";
            // 
            // TelaCadastroTesteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 577);
            this.Controls.Add(this.rtTeste);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TelaCadastroTesteForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Teste";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TelaCadastroTesteForm_FormClosing);
            this.Load += new System.EventHandler(this.TelaCadastroTesteForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.ComboBox cbNumeroQsts;
        private System.Windows.Forms.Button btnQuestao;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.ComboBox cbSerie;
        private System.Windows.Forms.Label lblMateria;
        private System.Windows.Forms.ComboBox cbMateria;
        private System.Windows.Forms.RichTextBox rtTeste;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.TextBox tbDisciplina;
        private System.Windows.Forms.Label lblDisciplina;
        private System.Windows.Forms.Button btnGabarito;
        private System.Windows.Forms.Button btnGerarPDF;
    }
}
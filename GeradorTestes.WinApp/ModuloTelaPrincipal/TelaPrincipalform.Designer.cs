namespace GeradorTestes.WinApp
{
    partial class TelaPrincipalForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPrincipalForm));
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.cadastroMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disciplinaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materiaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questaoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnInserir = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.btnPDF = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblToolPrincipal = new System.Windows.Forms.ToolStripLabel();
            this.statusRodape = new System.Windows.Forms.StatusStrip();
            this.lblStatusPrincipal = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuPrincipal.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuPrincipal.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(1861, 33);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "menuStrip1";
            // 
            // cadastroMenuItem
            // 
            this.cadastroMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disciplinaMenuItem,
            this.materiaMenuItem,
            this.questaoMenuItem,
            this.testeMenuItem});
            this.cadastroMenuItem.Name = "cadastroMenuItem";
            this.cadastroMenuItem.Size = new System.Drawing.Size(99, 29);
            this.cadastroMenuItem.Text = "Cadastro";
            // 
            // disciplinaMenuItem
            // 
            this.disciplinaMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.disciplinaMenuItem.Name = "disciplinaMenuItem";
            this.disciplinaMenuItem.Size = new System.Drawing.Size(270, 34);
            this.disciplinaMenuItem.Text = "Disciplinas";
            this.disciplinaMenuItem.Click += new System.EventHandler(this.disciplinaMenuItem_Click);
            // 
            // materiaMenuItem
            // 
            this.materiaMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.materiaMenuItem.Name = "materiaMenuItem";
            this.materiaMenuItem.Size = new System.Drawing.Size(270, 34);
            this.materiaMenuItem.Text = "Matérias";
            this.materiaMenuItem.Click += new System.EventHandler(this.materiaMenuItem_Click);
            // 
            // questaoMenuItem
            // 
            this.questaoMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.questaoMenuItem.Name = "questaoMenuItem";
            this.questaoMenuItem.Size = new System.Drawing.Size(270, 34);
            this.questaoMenuItem.Text = "Questões";
            this.questaoMenuItem.Click += new System.EventHandler(this.questaoMenuItem_Click);
            // 
            // testeMenuItem
            // 
            this.testeMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.testeMenuItem.Name = "testeMenuItem";
            this.testeMenuItem.Size = new System.Drawing.Size(270, 34);
            this.testeMenuItem.Text = "Testes";
            this.testeMenuItem.Click += new System.EventHandler(this.testeMenuItem_Click);
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 66);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1861, 781);
            this.panelPrincipal.TabIndex = 1;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInserir,
            this.btnEditar,
            this.btnExcluir,
            this.btnPDF,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.lblToolPrincipal});
            this.toolStrip.Location = new System.Drawing.Point(0, 33);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1861, 33);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnInserir
            // 
            this.btnInserir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInserir.Enabled = false;
            this.btnInserir.Image = ((System.Drawing.Image)(resources.GetObject("btnInserir.Image")));
            this.btnInserir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(34, 28);
            this.btnInserir.Text = "toolStripButton1";
            this.btnInserir.ToolTipText = "inserir novo registro";
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Enabled = false;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(34, 28);
            this.btnEditar.Text = "toolStripButton2";
            this.btnEditar.ToolTipText = "editar registro selecionado";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(34, 28);
            this.btnExcluir.Text = "toolStripButton3";
            this.btnExcluir.ToolTipText = "excluir registro selecionado";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPDF.Enabled = false;
            this.btnPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnPDF.Image")));
            this.btnPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(34, 28);
            this.btnPDF.Text = "toolStripButton1";
            this.btnPDF.ToolTipText = "gerar arquivo pdf";
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // lblToolPrincipal
            // 
            this.lblToolPrincipal.Name = "lblToolPrincipal";
            this.lblToolPrincipal.Size = new System.Drawing.Size(83, 28);
            this.lblToolPrincipal.Text = "Cadastro";
            // 
            // statusRodape
            // 
            this.statusRodape.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusRodape.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusRodape.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusPrincipal});
            this.statusRodape.Location = new System.Drawing.Point(0, 847);
            this.statusRodape.Name = "statusRodape";
            this.statusRodape.Size = new System.Drawing.Size(1861, 32);
            this.statusRodape.TabIndex = 3;
            // 
            // lblStatusPrincipal
            // 
            this.lblStatusPrincipal.Name = "lblStatusPrincipal";
            this.lblStatusPrincipal.Size = new System.Drawing.Size(113, 25);
            this.lblStatusPrincipal.Text = "Tela principal";
            // 
            // TelaPrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1861, 879);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.statusRodape);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuPrincipal;
            this.MaximizeBox = false;
            this.Name = "TelaPrincipalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador de Testes";
            this.Load += new System.EventHandler(this.TelaPrincipalForm_Load);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusRodape.ResumeLayout(false);
            this.statusRodape.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem cadastroMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questaoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disciplinaMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testeMenuItem;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnInserir;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.StatusStrip statusRodape;
        private System.Windows.Forms.ToolStripLabel lblToolPrincipal;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusPrincipal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem materiaMenuItem;
        private System.Windows.Forms.ToolStripButton btnPDF;
    }
}

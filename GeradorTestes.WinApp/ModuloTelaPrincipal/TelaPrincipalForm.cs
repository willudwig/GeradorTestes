using GeradorTestes.Infra.Arquivo;
using GeradorTestes.Infra.Arquivo.ModuloQuestao;
using GeradorTestes.Infra.Arquivo.ModuloDisciplina;
using GeradorTestes.Infra.Arquivo.ModuloMateria;
using GeradorTestes.Infra.Arquivo.ModuloTeste;
using GeradorTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloDisciplina;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GeradorTestes.WinApp.ModuloQuestao;
using GeradorTestes.WinApp.ModuloTeste;

namespace GeradorTestes.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private IControlador controlador;
        private Dictionary<string, IControlador> controladores;
        private DataContext contextoDados;

        public TelaPrincipalForm()
        {
            InitializeComponent();

            Instancia = this;

            lblStatusPrincipal.Text = string.Empty;
            lblToolPrincipal.Text = string.Empty;

            contextoDados = contextoDados;

            InicializarControladores();
        }

        private void InicializarControladores()
        {
            var repositorioDisciplina = new RepositorioDisciplinaArquivo(contextoDados);
            var repositorioMateria = new RepositorioMateriaArquivo(contextoDados);
            var repositorioQuestao = new RepositorioQuestaoArquivo(contextoDados);
            var repositorioTeste = new RepositorioTesteArquivo(contextoDados);

            controladores = new Dictionary<string, IControlador>();

            controladores.Add("Disciplinas", new ControladorDisciplina(repositorioDisciplina));
            controladores.Add("Matérias", new ControladorMateria(repositorioMateria, repositorioDisciplina));
            controladores.Add("Questões", new ControladorQuestao(repositorioQuestao, repositorioMateria));
             controladores.Add("Testes", new ControladorTeste(repositorioTeste, repositorioMateria, repositorioQuestao));
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        public void AtualizarRodape(string mensagem)
        {
            lblStatusPrincipal.Text = mensagem;
        }

        private void questaoMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
            HabilitarBotoesToolStrip();
        }

        private void materiaMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
            HabilitarBotoesToolStrip();
        }

        private void disciplinaMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
            HabilitarBotoesToolStrip();
        }

        private void testeMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
            HabilitarBotoesToolStrip();
        }

        private void HabilitarBotoesToolStrip()
        {
            btnInserir.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelPrincipal.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            try { panelPrincipal.Controls.Add(listagemControl); } catch (InvalidOperationException){ return; };
        }

        private void ConfigurarToolbox()
        {
            IConfiguracaoToolStrip configuracao = controlador.ObtemConfiguracaoToolStrip();

            if (configuracao != null)
            {
                toolStrip.Enabled = true;

                lblToolPrincipal.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarBotoes(IConfiguracaoToolStrip configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void ConfigurarTooltips(IConfiguracaoToolStrip configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void TelaPrincipalForm_Load(object sender, EventArgs e)
        {
            Instancia.AtualizarRodape("");
        }
    }
}

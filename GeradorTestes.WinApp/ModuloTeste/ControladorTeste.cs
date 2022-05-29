using BancoDados.ModuloDisciplina;
using BancoDados.ModuloMateria;
using BancoDados.ModuloQuestao;
using BancoDados.ModuloTeste;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloTeste;
using GeradorTestes.Infra.Arquivo.Compartilhado;
using GeradorTestes.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public class ControladorTeste : IControlador
    {

        private readonly RepositorioTesteBancoDados repoTeste;
        private readonly RepositorioMateriaBancoDados repoMateria;
        private readonly RepositorioQuestaoBancoDados repoQuestao; 
        private readonly RepositorioDisciplinaBancoDados repoDisciplina;

        private TabelaTestesControl tabelaTestes;


        public ControladorTeste(RepositorioTesteBancoDados repoTeste, RepositorioMateriaBancoDados repoMateria, RepositorioQuestaoBancoDados repoQuestao, RepositorioDisciplinaBancoDados repoDisciplina)
        {
            this.repoTeste = repoTeste;
            this.repoMateria = repoMateria;
            this.repoQuestao = repoQuestao;
            this.repoDisciplina = repoDisciplina;
        }

        public void Editar()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null || testeSelecionado.Numero == 0)
            {
                MessageBox.Show("Selecione um teste primeiro",
                "Edição de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTesteForm tela = new();

            tela.DesabilitarBotoesDeCima();

            tela.Teste = testeSelecionado;

            CarregarDisciplinasNoTeste(tela);

            tela.questoesTeste = CarregarQuestoes();

            tela.GravarRegistro = repoTeste.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
           
        }

        public void Excluir()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null || testeSelecionado.Numero == 0)
            {
                MessageBox.Show("Selecione um teste primeiro",
                "Exclusão de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir o teste?",
                "Exclusão de Teste", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repoTeste.Excluir(testeSelecionado);
                CarregarTestes();
            }
        }

        public void Inserir()
        {
            TelaCadastroTesteForm tela = new();
            tela.Teste = new();

            tela.GravarRegistro = repoTeste.Inserir;

            CarregarDisciplinasNoTeste(tela);

            tela.questoesTeste = CarregarQuestoes();

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public IConfiguracaoToolStrip ObtemConfiguracaoToolStrip()
        {
            return new ConfiguracaoToolStripTeste();
        }

        public UserControl ObtemListagem()
        {
            if (tabelaTestes == null)
                tabelaTestes = new TabelaTestesControl();

            CarregarTestes();

            return tabelaTestes;
        }

        private Teste ObtemTesteSelecionado()
        {
            var numero = tabelaTestes.ObtemNumerTesteSelecionado();

            return repoTeste.SelecionarPorNumero(numero);
        }

        private void CarregarTestes()
        {
            List<Teste> testes = repoTeste.SelecionarTodos();

            tabelaTestes.AtualizarRegistros(testes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {testes.Count} teste(s)");
        }

        public List<Questao> CarregarQuestoes()
        {
            var todas = repoQuestao.SelecionarTodos();
            return todas;
        }

        public void GerarPDF()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null || testeSelecionado.Numero == 0)
            {
                MessageBox.Show("Selecione um teste primeiro",
                "Edição de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ArquivoPDF pdf = new();

            pdf.GerarPDF_ItextSharp(testeSelecionado.Prova);

            MessageBox.Show("Arquivo PDF gerado com sucesso!\n\n Caminho: C: -> temp -> pdf -> Teste.pdf", "Aviso");

            AbrirPDFDiretorrio();
        }

        private void AbrirPDFDiretorrio()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\temp\pdf";
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Diagnostics.Process.Start(openFileDialog.FileName);
                }
                catch(System.ComponentModel.Win32Exception)
                {
                    MessageBox.Show("Adquira a versão Premium do Gerador de Teste 1.0\npara poder visualizar o arquivo PDF pelo programa!", "Abrir PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void CarregarDisciplinasNoTeste(TelaCadastroTesteForm tela)
        {
            List<Disciplina> disciplinas = repoDisciplina.SelecionarTodos();

            tela.disciplinasTeste = disciplinas;
        }

    }
}

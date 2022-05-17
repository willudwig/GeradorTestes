using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloTeste;
using GeradorTestes.Infra.Arquivo.ModuloMateria;
using GeradorTestes.Infra.Arquivo.ModuloQuestao;
using GeradorTestes.Infra.Arquivo.ModuloTeste;
using GeradorTestes.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public class ControladorTeste : IControlador
    {

        private readonly RepositorioTesteArquivo repoTeste;
        private readonly RepositorioMateriaArquivo repoMateria;
        private readonly RepositorioQuestaoArquivo repoQuestao;
        private TabelaTestesControl tabelaTestes;


        public ControladorTeste(RepositorioTesteArquivo repoTeste, RepositorioMateriaArquivo repoMateria, RepositorioQuestaoArquivo repoQuestao)
        {
            this.repoTeste = repoTeste;
            this.repoMateria = repoMateria;
            this.repoQuestao = repoQuestao;
        }

        public void Editar()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um teste primeiro",
                "Edição de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTesteForm tela = new();

            tela.Teste = testeSelecionado;

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

            if (testeSelecionado == null)
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

            CarregarMateriasNoTeste(tela);
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

        public void CarregarMateriasNoTeste(TelaCadastroTesteForm tela)
        {
            List<Materia> materias = repoMateria.SelecionarTodos();

            tela.materiasTeste = materias;
        }

        public List<Questao> CarregarQuestoes()
        {
            var todas = repoQuestao.SelecionarTodos();

            return todas;
        }

        public void ExibirTelaGerarPDF()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um teste primeiro",
                "Edição de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTesteForm tela = new();

            tela.Teste = testeSelecionado;

            tela.DeixarSomenteBotaoPDF();

            tela.ShowDialog();
        }

    }
}

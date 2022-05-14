using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloQuestao;
using GeradorTestes.WinApp.Compartilhado;
using System.Collections.Generic;

using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloQuestao
{
    public class ControladorQuestao : IControlador
    {
        private readonly IRepositorioQuestao repoQuestao;
        private readonly IRepositorioMateria repoMateria;
        private TabelaQuestoesControl tabelaQuestoes;

        public ControladorQuestao(IRepositorioQuestao repoQuestao, IRepositorioMateria repoMateria)
        {
            this.repoQuestao = repoQuestao;
            this.repoMateria = repoMateria;
        }

        public void Editar()
        {
            Questao questSelecionada = ObtemQuestaoSelecionada();

            if (questSelecionada == null)
            {
                MessageBox.Show("Selecione uma matéria primeiro",
                "Edição de Materias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroQuestaoForm tela = new();

            tela.Questao = questSelecionada;

            tela.GravarRegistro = repoQuestao.Editar;

            CarregarMateriasNaQuestao(tela);

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestoes();
            }
        }

        public void Excluir()
        {
            Questao questSelecionada = ObtemQuestaoSelecionada();

            if (questSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Exclusão de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a questão?",
                "Exclusão de Questão", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repoQuestao.Excluir(questSelecionada);
                CarregarQuestoes();
            }
        }

        public void Inserir()
        {
            TelaCadastroQuestaoForm tela = new();
            tela.Questao = new();

            tela.GravarRegistro = repoQuestao.Inserir;

            CarregarMateriasNaQuestao(tela);

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestoes();
            }
        }

        public IConfiguracaoToolStrip ObtemConfiguracaoToolStrip()
        {
            return new ConfiguracaoToolStripQuestao();
        }

        public UserControl ObtemListagem()
        {
            if (tabelaQuestoes == null)
                tabelaQuestoes = new TabelaQuestoesControl();

            CarregarQuestoes();

            return tabelaQuestoes;
        }

        private void CarregarQuestoes()
        {
            List<Questao> quests = repoQuestao.SelecionarTodos();

            tabelaQuestoes.AtualizarRegistros(quests);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {quests.Count} questão(ões)");
        }

        private void CarregarMateriasNaQuestao(TelaCadastroQuestaoForm tela)
        {
            List<Materia> materias = repoMateria.SelecionarTodos();

            tela.materiasQuestao = materias;
        }

        private Questao ObtemQuestaoSelecionada()
        {
            var numero = tabelaQuestoes.ObtemNumerQuestaoSelecionada();

            return repoQuestao.SelecionarPorNumero(numero);
        }

    }
}

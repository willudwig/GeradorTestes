using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloDisciplina;
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
        private readonly IRepositorioDisciplina repoDisciplina;
        private TabelaQuestoesControl tabelaQuestoes;

        public ControladorQuestao(IRepositorioQuestao repoQuestao, IRepositorioMateria repoMateria, IRepositorioDisciplina repoDisciplina)
        {
            this.repoQuestao = repoQuestao;
            this.repoMateria = repoMateria;
            this.repoDisciplina = repoDisciplina;
        }

        public void Inserir()
        {
            TelaCadastroQuestaoForm tela = new();
            tela.Questao = new();

            tela.opcaoBotao = "inserir";
            tela.listaQuestoes = new();
            tela.listaQuestoes = repoQuestao.SelecionarTodos();

            tela.GravarRegistro = repoQuestao.Inserir;

            CarregarMateriasNaQuestao(tela);
            CarregarDisciplinasNaQuestao(tela);

            DialogResult resultado = tela.ShowDialog();

            if (tela.jaExiste == false)

                AdicionarAlternativas(tela.Questao);

            if (resultado == DialogResult.OK)
            {
                CarregarQuestoes();
            }
        }

        public void Editar()
        {
            Questao questSelecionada = ObtemQuestaoSelecionada();

            if (questSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro", 
                                "Edição de Questões", 
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Exclamation);

                return;
            }

            TelaCadastroQuestaoForm tela = new();

            tela.Questao = questSelecionada;

            tela.GravarRegistro = repoQuestao.Editar;

            CarregarMateriasNaQuestao(tela);
            CarregarDisciplinasNaQuestao(tela);

            DialogResult resultado = tela.ShowDialog();

            EditarAlternativas(tela.Questao);

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

        private void CarregarDisciplinasNaQuestao(TelaCadastroQuestaoForm tela)
        {
            List<Disciplina> disciplinas = repoDisciplina.SelecionarTodos();

            tela.disciplinasQuestao = disciplinas;
        }

        private Questao ObtemQuestaoSelecionada()
        {
            var numero = tabelaQuestoes.ObtemNumerQuestaoSelecionada();

            if (numero == 0) 
                return null;

            return repoQuestao.SelecionarPorNumero(numero);
        }

        public void GerarPDF()
        {
            throw new System.NotImplementedException();
        }

        public void EditarAlternativas(Questao questao)
        {
            repoQuestao.EditarAlternativas(questao);
        }

        private void AdicionarAlternativas(Questao questao)
        {
            if (questao.alternativas.Count == 0)
                return;

            repoQuestao.AdicionarAlternativas(questao);
        }

    }
}

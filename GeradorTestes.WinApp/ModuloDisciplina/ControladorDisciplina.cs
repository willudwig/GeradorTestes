using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloQuestao;
using GeradorTestes.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public class ControladorDisciplina : IControlador
    {
        private readonly IRepositorioDisciplina repoDisciplina;
        private readonly IRepositorioMateria repoMateria;
        private readonly IRepositorioQuestao repoQuestao;
        TabelaDisciplinasControl tabelaDisciplinas;
        ControladorMateria controladorMateria;

        public ControladorDisciplina(IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria, IRepositorioQuestao repositorioQuestao)
        {
            repoDisciplina = repositorioDisciplina;
            repoMateria = repositorioMateria;
            repoQuestao = repositorioQuestao;
        }

        public void Editar()
        {
            Disciplina discSelecionada = ObtemDisciplinaSelecionada();

            if (discSelecionada == null)
            {
                MessageBox.Show("Selecione uma disicplina primeiro",
                "Edição de Disciplinas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroDisciplinaForm tela = new();

            tela.Disciplina = discSelecionada;

            tela.GravarRegistro = repoDisciplina.Editar;

            controladorMateria = new(repoMateria, repoDisciplina);

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDisciplinas();
            }
        }

        private Disciplina ObtemDisciplinaSelecionada()
        {
            var numero = tabelaDisciplinas.ObtemNumerDisciplinaSelecionada();

            return repoDisciplina.SelecionarPorNumero(numero);
        }

        public void Excluir()
        {
            Disciplina discSelecionado = ObtemDisciplinaSelecionada();

            if (discSelecionado == null)
            {
                MessageBox.Show("Selecione uma disciplina primeiro",
                "Exclusão de Disciplinas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a disciplina?",
                "Exclusão de Disciplina", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repoDisciplina.Excluir(discSelecionado);
                CarregarDisciplinas();
            }
        }

        public void Inserir()
        {
            TelaCadastroDisciplinaForm tela = new();
            tela.Disciplina = new();

            tela.GravarRegistro = repoDisciplina.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDisciplinas();
            }
        }

        private void CarregarDisciplinas()
        {
            List<Disciplina> disciplinas = repoDisciplina.SelecionarTodos();

            tabelaDisciplinas.AtualizarRegistros(disciplinas);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {disciplinas.Count} disciplina(s)");
        }

        public IConfiguracaoToolStrip ObtemConfiguracaoToolStrip()
        {
            return new ConfiguracaoToolStripDisciplina();
        }

        public UserControl ObtemListagem()
        {
            if (tabelaDisciplinas == null)
                tabelaDisciplinas = new TabelaDisciplinasControl();

            CarregarDisciplinas();

            return tabelaDisciplinas;
        }
    }
}

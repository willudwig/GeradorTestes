using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloMateria;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public class ControladorDisciplina : IControlador
    {
        private readonly IRepositorioDisciplina repoDisciplina;
        private readonly IRepositorioMateria repoMateria;
        TabelaDisciplinasControl tabelaDisciplinas;


        public ControladorDisciplina(IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria)
        {
            repoDisciplina = repositorioDisciplina;
            repoMateria = repositorioMateria;
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

            DialogResult resultado = tela.ShowDialog();

            EditarMateriaPelaDisciplina(tela.nomeAntigo, tela.Disciplina);

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

        public void ExibirTelaGerarPDF()
        {
            throw new System.NotImplementedException();
        }

        public void EditarMateriaPelaDisciplina(string antiga, Disciplina discEditada)
        {
            TelaCadastroMateriaForm tela = new();

            List<Materia> materias = repoMateria.SelecionarTodos();

            if (materias.Count == 0)
                return;

            List<Materia> materiasSelecionadas = materias.FindAll(m => m.Disciplina.Nome == antiga);

            if (materiasSelecionadas.Count == 0)
                return;

            foreach (Materia mat in materiasSelecionadas)
            {
                mat.Disciplina = discEditada;

                tela.Materia = mat;

                tela.GravarRegistro = repoMateria.Editar;
            }
        }
    }
}

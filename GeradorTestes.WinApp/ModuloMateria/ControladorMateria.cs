using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloQuestao;
using GeradorTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloMateria;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public class ControladorMateria : IControlador
    {
        private readonly IRepositorioMateria repoMateria;
        private readonly IRepositorioDisciplina repoDisciplina;
        private readonly IRepositorioQuestao repoQuestao;
        private TabelaMateriasControl tabelaMaterias;

        public ControladorMateria(IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina, IRepositorioQuestao repositorioQuestao)
        {
            repoMateria = repositorioMateria;
            repoDisciplina = repositorioDisciplina;
            repoQuestao = repositorioQuestao;
        }
        public void Inserir()
        {
            TelaCadastroMateriaForm tela = new();
            tela.Materia = new();

            tela.opcaoBotao = "inserir";
            tela.listaMaterias = new();
            tela.listaMaterias = repoMateria.SelecionarTodos();

            tela.GravarRegistro = repoMateria.Inserir;

            tela.DialogResult = DialogResult.None;

            CarregarDisciplinasNaMateria(tela);

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }
        }

        public void Editar()
        {
            Materia matSelecionada = ObtemMateriaSelecionada();

            if (matSelecionada == null)
            {
                MessageBox.Show("Selecione uma matéria primeiro",
                "Edição de Materias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroMateriaForm tela = new();

            tela.Materia = matSelecionada;

            tela.GravarRegistro = repoMateria.Editar;

            CarregarDisciplinasNaMateria(tela);

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }
        }

        public void Excluir()
        {
            Materia matSelecionada = ObtemMateriaSelecionada();

            if (VerificarMateriaVinculadaQuestao(matSelecionada) == false)
            {
                if (matSelecionada == null)
                {
                    MessageBox.Show("Selecione uma matéria primeiro",
                    "Exclusão de Matérias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult resultado = MessageBox.Show("Deseja realmente excluir a matéria?",
                    "Exclusão de Matéria", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (resultado == DialogResult.OK)
                {
                    repoMateria.Excluir(matSelecionada);
                    CarregarMaterias();
                }
                else
                    return;
            }
        }

        public void GerarPDF()
        {
            throw new System.NotImplementedException();
        }

        public IConfiguracaoToolStrip ObtemConfiguracaoToolStrip()
        {
            return new ConfiguracaoToolStripMateria();
        }

        public UserControl ObtemListagem()
        {
            if (tabelaMaterias == null)
                tabelaMaterias = new TabelaMateriasControl();

            CarregarMaterias();

            return tabelaMaterias;
        }

        private void CarregarDisciplinasNaMateria(TelaCadastroMateriaForm tela)
        {
            List<Disciplina> disciplinas = repoDisciplina.SelecionarTodos();

            tela.disciplinasMateria = disciplinas;
        }

        private void CarregarMaterias()
        {
            List<Materia> materias = repoMateria.SelecionarTodos();

            tabelaMaterias.AtualizarRegistros(materias);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {materias.Count} materia(s)");
        }

        public Materia ObtemMateriaSelecionada()
        {
            var numero = tabelaMaterias.ObtemNumerMateriaSelecionada();

            return repoMateria.SelecionarPorNumero(numero);
        }

        private bool VerificarMateriaVinculadaQuestao(Materia materia)
        {
            List<Questao> questoes = repoQuestao.SelecionarTodos();

            bool resultado = questoes.Exists(q => q.TituloMateria.Equals(materia.Titulo));

            if (resultado)
            {
                MessageBox.Show("A matéria selecionada está vinculada a uma questão\n e não poderá ser excluída.", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            return resultado;
        }
    }
}

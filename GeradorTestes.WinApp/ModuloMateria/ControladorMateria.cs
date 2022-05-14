using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
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
        private TabelaMateriasControl tabelaMaterias;

        public ControladorMateria(IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina)
        {
            repoMateria = repositorioMateria;
            repoDisciplina = repositorioDisciplina;
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
            Materia matSelecionado = ObtemMateriaSelecionada();

            if (matSelecionado == null)
            {
                MessageBox.Show("Selecione uma matéria primeiro",
                "Exclusão de Matérias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a matéria?",
                "Exclusão de Matéria", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repoMateria.Excluir(matSelecionado);
                CarregarMaterias();
            }
        }

        public void Inserir()
        {
            TelaCadastroMateriaForm tela = new();
            tela.Materia = new();

            tela.GravarRegistro = repoMateria.Inserir;

            CarregarDisciplinasNaMateria(tela);

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }
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

        private Materia ObtemMateriaSelecionada()
        {
            var numero = tabelaMaterias.ObtemNumerMateriaSelecionada();

            return repoMateria.SelecionarPorNumero(numero);
        }
    }
}

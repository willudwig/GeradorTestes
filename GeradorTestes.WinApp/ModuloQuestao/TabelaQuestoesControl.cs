using GeradorTeste.Dominio;
using GeradorTestes.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloQuestao
{
    public partial class TabelaQuestoesControl : UserControl
    {
        public TabelaQuestoesControl()
        {
            InitializeComponent();

            grid.ConfigurarGridSomenteLeitura();
            grid.ConfigurarGridZebrado();
            grid.Columns.AddRange(ObterColunas());
        }

        private DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                new DataGridViewTextBoxColumn { DataPropertyName = "TituloMateria", HeaderText = "Título"},

                new DataGridViewTextBoxColumn { DataPropertyName = "DisciplinaDaMateria", HeaderText = "Disciplina"},

                new DataGridViewTextBoxColumn { DataPropertyName = "SerieMateria", HeaderText = "Série"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Pergunta", HeaderText = "Pergunta"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Resposta", HeaderText = "Resposta"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Bimestre", HeaderText = "Bimestre"},
            };

            return colunas;
        }

        public int ObtemNumerQuestaoSelecionada()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Questao> questoes)
        {
            grid.DataSource = questoes;
        }
    }
}

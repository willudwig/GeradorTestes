using GeradorTeste.Dominio.ModuloTeste;
using GeradorTestes.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public partial class TabelaTestesControl : UserControl
    {
        public TabelaTestesControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "DisciplinaDaMateria", HeaderText = "Disciplina"},

                new DataGridViewTextBoxColumn { DataPropertyName = "TituloDaMateria", HeaderText = "Matéria"},

                new DataGridViewTextBoxColumn { DataPropertyName = "SerieDaMateria", HeaderText = "Série"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Data", HeaderText = "Data"},

            };

            return colunas;
        }

        public int ObtemNumerTesteSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Teste> testes)
        {
            grid.DataSource = testes;
        }

    }
}

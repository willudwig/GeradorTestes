using FluentValidation.Results;
using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static GeradorTeste.Dominio.ModuloMateria.Materia;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public partial class TelaCadastroMateriaForm : Form
    {
        private Materia materia;
        public string nomeAntigo;
        public List<Materia> listaMaterias;
        public string opcaoBotao;

        public List<Disciplina> disciplinasMateria;

        public Func<Materia, ValidationResult> GravarRegistro
        {
            get; set;
        }

        public Materia Materia
        {
            get { return materia; }
            set 
            { 
                materia = value;

                tbTitulo.Text = materia.Titulo;
                cbDisciplina.Text = "selecionar...";
                cbSerie.Text = "selecionar...";
            }
        }

        public TelaCadastroMateriaForm()
        {
            InitializeComponent();
            disciplinasMateria = new();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            tbTitulo.Clear();
            tbTitulo.Focus();
            cbSerie.Text = "selecionar...";
            cbDisciplina.Text = "selecionar...";
        }

        private void TelaCadastroMateriaForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
            CarregarDisciplinasComboBox();
            DeixarComboBoxSelecionados();

            nomeAntigo = tbTitulo.Text;
        }

        private void TelaCadastroMateriaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (VerificarCombosVazios() == true)
            {
                materia.Titulo = tbTitulo.Text;
                materia.Serie = (EnumeradorSerie)cbSerie.SelectedIndex;

                materia.Disciplina.Nome = cbDisciplina.Text;
                var disc = disciplinasMateria.Find(x => x.Nome.Equals(materia.Disciplina.Nome));
                materia.Disciplina.Numero = disc.Numero;
               
                if (VerificarMateriaExistente(materia) == true)

                     return;

                else
                {
                    ValidationResult resultadoValidacao = GravarRegistro(materia);

                    if (resultadoValidacao.IsValid == false)
                    {
                        string erro = resultadoValidacao.Errors[0].ErrorMessage;

                        TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                        DialogResult = DialogResult.None;
                    }
                }

                opcaoBotao = "";
            }
            else
                return;
        }

        private bool VerificarMateriaExistente(Materia  materia)
        {
            if (opcaoBotao == "inserir")
            {
                bool n = listaMaterias.Exists(x => x.Titulo.Equals(materia.Titulo));

                if (n)
                {
                    MessageBox.Show("Já existe uma matéria com esse nome", "Aviso");
                    return true;
                }
                else

                    return false;
            }

            return false;
        }

        private void DeixarComboBoxSelecionados()
        {
            cbSerie.SelectedIndex = 0;
        }

        private void CarregarDisciplinasComboBox()
        {
            cbDisciplina.Items.Clear();
            
            foreach (Disciplina d in disciplinasMateria)
            {
                cbDisciplina.Items.Add(d.Nome);
            }
        }

        private bool VerificarCombosVazios()
        {
            if(cbSerie.Text == "")
            {
                MessageBox.Show("Ao menos uma série de ver escolhida", "Aviso");
                return false;
            }

            if (cbDisciplina.Text == "")
            {
                MessageBox.Show("Ao menos uma disciplina deve ser escolhida", "Aviso");
                return false;
            }

            return true;
        }
    }
}

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

                ValidationResult resultadoValidacao = GravarRegistro(materia);

                if (resultadoValidacao.IsValid == false)
                {
                    string erro = resultadoValidacao.Errors[0].ErrorMessage;

                    TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                    DialogResult = DialogResult.None;
                }
            }
            else
                return;
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

using GeradorTeste.Dominio.ModuloDisciplina;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public partial class TelaCadastroDisciplinaForm : Form
    {
        private Disciplina disciplina;
        public string nomeAntigo, nomeNovo, opcaoBotao;
        public List<Disciplina> listaDisciplinas;

        public Func<Disciplina, ValidationResult> GravarRegistro
        {
            get; set; 
        }

        public Disciplina Disciplina
        {
            get
            {
                return disciplina;
            }
            set
            {
                disciplina = value;

                tbNome.Text = disciplina.Nome;
            }
        }

        public TelaCadastroDisciplinaForm()
        {
            InitializeComponent();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            tbNome.Clear();
            tbNome.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            nomeNovo = tbNome.Text;
            disciplina.Nome = tbNome.Text;

            if (VerificarDisciplinaExistente(disciplina) == true)

                    return;
            else
            {
                ValidationResult resultadoValidacao = GravarRegistro(disciplina);

                if (resultadoValidacao.IsValid == false)
                {
                    string erro = resultadoValidacao.Errors[0].ErrorMessage;

                    TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                    DialogResult = DialogResult.None;
                }
            }

            opcaoBotao = "";
        }

        private bool VerificarDisciplinaExistente(Disciplina disciplina)
        {
            if (opcaoBotao == "inserir")
            {
                bool n = listaDisciplinas.Exists(x => x.Nome.Equals(disciplina.Nome));

                if (n)
                {
                    MessageBox.Show("Já existe uma disciplina com esse nome", "Aviso");
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void TelaCadastroDisciplinaForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");

            if (tbNome.Text != "")
                nomeAntigo = tbNome.Text;
        }

        private void TelaCadastroDisciplinaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }
    }
}

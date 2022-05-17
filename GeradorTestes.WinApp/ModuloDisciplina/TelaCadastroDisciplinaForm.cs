using GeradorTeste.Dominio.ModuloDisciplina;
using System;
using System.Windows.Forms;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public partial class TelaCadastroDisciplinaForm : Form
    {
        private Disciplina disciplina;
        public string nomeAntigo, nomeNovo;

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

            ValidationResult resultadoValidacao = GravarRegistro(disciplina);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
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

using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTestes.Infra.Arquivo.ModuloMateria;
using GeradorTestes.WinApp.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static GeradorTeste.Dominio.ModuloMateria.Materia;

namespace GeradorTestes.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestaoForm : Form
    {
        int contFalsas = 1;
        Questao questao;
        public List<Materia> materiasQuestao;

        public Func<Questao, ValidationResult> GravarRegistro
        {
            get; set;
        }

        public Questao Questao
        {
            get { return questao; }
            set
            {
                questao = value;

                cbMateriaTitulo.Text = "";
                tbDisciplina.Text = questao.Materia.Disciplina.Nome;
                tbSerie.Text = questao.Materia.Serie.ToString();
                cbBimestre.Text = "";
                tbPergunta.Text = questao.Pergunta;
                tbResposta.Text = questao.Resposta;
                tbAlternativa.Text = "";
            }
        }

        public TelaCadastroQuestaoForm()
        {
            InitializeComponent();
            materiasQuestao = new();
        }


        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCamposQuestao();
        }

        #region não eventos


        private void LimparCamposQuestao()
        {
            cbMateriaTitulo.Items.Clear();
            cbMateriaTitulo.Text = "selecionar...";
            cbBimestre.Text = "selecionar...";
            cbMateriaTitulo.Text = "selecionar...";
            tbSerie.Clear();
            tbPergunta.Clear();
            tbResposta.Clear();
            tbDisciplina.Clear();
            lblFalsa1.Text = "Falsa 01 - ";
            lblFalsa2.Text = "Falsa 02 - ";
            lblFalsa3.Text = "Falsa 03 - ";
            tbAlternativa.Clear();
            tbAlternativa.Enabled = true;
            CarregarMateriasNaQuestao();    
        }

        private void DeixarComboboxSelecionado()
        {
            cbBimestre.SelectedIndex = 0;
        }

        private void CarregarMateriasNaQuestao()
        {
            cbMateriaTitulo.Items.Clear();

            foreach (Materia m in materiasQuestao)
            {
                cbMateriaTitulo.Items.Add(m.Titulo);
            }
        }

        #endregion

        private void btnAlternativa_Click(object sender, EventArgs e)
        {
            if (tbAlternativa.Text == "")
            {
                MessageBox.Show("Campo de alternativa não pode ser vazio", "Aviso");
                return;
            }

            switch (contFalsas)
            {
                case 1:
                    lblFalsa1.Text += " " + tbAlternativa.Text;
                    break;

                case 2:
                    if (VerificarAlternativaExistente() == true) break;
                    lblFalsa2.Text += " " + tbAlternativa.Text;
                    break;

                case 3:
                    if(VerificarAlternativaExistente() == true) break;
                    lblFalsa3.Text += " " + tbAlternativa.Text;
                    break;

                default:
                    break;
            }

            lbContador.Text = "0" + (++contFalsas) + " de 03";

            if (contFalsas > 3)
            {
                btnAlternativa.Enabled = false;
                tbAlternativa.Enabled = false;
                btnOK.Enabled = true;
                lblTodos.Visible = false;
                lbContador.Text = "03 de 03";
            }

            tbAlternativa.Clear();
            tbAlternativa.Focus();
        }

        private bool VerificarAlternativaExistente()
        {
            string a = lblFalsa1.Text.Substring(11).Trim();
            string b = lblFalsa2.Text.Substring(11).Trim();

            if (tbAlternativa.Text == a  || tbAlternativa.Text == b)
            {
                MessageBox.Show("Alternativa já cadastrada", "Aviso");
                tbAlternativa.Clear();
                tbAlternativa.Focus();
                contFalsas--;
                return true;
            }

            return false;
        }

        private void tbAlternativa_TextChanged(object sender, EventArgs e)
        {
            btnAlternativa.Enabled = true;
        }

        private void TelaCadastroQuestaoForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
            CarregarMateriasNaQuestao();
            DeixarComboboxSelecionado();
        }

        private void TelaCadastroQuestaoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void cbMateriaTitulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Materia m in materiasQuestao)
            {
                if (m.Titulo == cbMateriaTitulo.SelectedItem)
                {
                    tbSerie.Text = m.Serie.ToString();
                    tbDisciplina.Text = m.Disciplina.Nome;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (VerificarMateriaVazia() == false)
            {
                questao.Materia.Titulo = cbMateriaTitulo.SelectedItem.ToString();
                questao.Materia.Disciplina.Nome = tbDisciplina.Text;
                switch (tbSerie.Text)
                {
                    case "Primeira":
                        questao.Materia.Serie = EnumeradorSerie.Primeira;
                        break;

                    case "Segunda":
                        questao.Materia.Serie = EnumeradorSerie.Segunda;
                        break;

                    default:
                        break;
                }
                questao.alternativas.Add(lblFalsa1.Text.Substring(11).Trim());
                questao.alternativas.Add(lblFalsa2.Text.Substring(11).Trim());
                questao.alternativas.Add(lblFalsa3.Text.Substring(11).Trim());
                questao.Pergunta = tbPergunta.Text;
                questao.Resposta = tbResposta.Text;

                ValidationResult resultadoValidacao = GravarRegistro(questao);

                if (resultadoValidacao.IsValid == false)
                {
                    string erro = resultadoValidacao.Errors[0].ErrorMessage;

                    TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                    DialogResult = DialogResult.None;
                }
            }
        }

        private bool VerificarMateriaVazia()
        {
            if(cbMateriaTitulo.Text == "")
            {
                MessageBox.Show("Ao menos uma matéria deve ser escolhida", "Aviso");
                return true;
            }

            return false;
        }
    }
    
}

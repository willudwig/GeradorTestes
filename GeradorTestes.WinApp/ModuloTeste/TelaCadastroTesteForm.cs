using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloTeste;
using GeradorTestes.Infra.Arquivo.Compartilhado;
using GeradorTestes.Infra.Arquivo.ModuloMateria;
using GeradorTestes.WinApp.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static GeradorTeste.Dominio.ModuloTeste.Teste;

namespace GeradorTestes.WinApp
{
    public partial class TelaCadastroTesteForm : Form
    {
        Teste teste;
        Questao qAleatoria;
        Questao escolhida;
        public List<Disciplina> disciplinasTeste;
        public List<Questao> questoesTeste;
        List<string> falsasComVerdadeira;
        List<Questao> aleatorias;
        List<Questao> questaoQueJafoi;
        List<Questao> listaQuestoesFiltradas;
        int numQst;
        int numQstoesComboBox;
        public RepositorioMateriaArquivo rpMat;
        public ControladorTeste contrlTeste;
        string altA, altB, altC, altD;
        string cabecalho, questao;

        public Func<Teste, ValidationResult> GravarRegistro
        {
            get; set;
        }

        public Teste Teste
        {
            get
            {
                return teste;
            }
            set
            {
                teste = value;

                rtTeste.Text = teste.Prova;
            }
        }

        public TelaCadastroTesteForm()
        {
            InitializeComponent();
            disciplinasTeste = new();
            questoesTeste = new();
            aleatorias = new();
            falsasComVerdadeira = new();
            escolhida = new();
            questaoQueJafoi = new();
            numQst = 0;
        }

        

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            rtTeste.Clear();
            btnGerarQuestoes.Enabled = true;
        }

        private void cbSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarDisciplinasNoTeste();
            cbDisciplina.Enabled = true;
        }

        private void TelaCadastroTesteForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
            PrimeiroCarregamentoDisciplinasNoTeste();
            DeixarComboboxSelecionado();
        }

        private void TelaCadastroTesteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (VerificarCampos() == true)
            {
                teste.Prova = rtTeste.Text;
                int numeroQuestoes = cbNumeroQsts.SelectedIndex;

                switch (numeroQuestoes)
                {
                    case 0:
                        teste.NumeroQuestoes = EnumNumeroQuestoes.cinco;
                        break;

                    case 1:
                        teste.NumeroQuestoes = EnumNumeroQuestoes.dez;
                        break;

                    case 2:
                        teste.NumeroQuestoes = EnumNumeroQuestoes.quinze;
                        break;

                    case 3:
                        teste.NumeroQuestoes = EnumNumeroQuestoes.vinte;
                        break;

                    default:
                        break;
                }

                teste.data = DateTime.Now;

                teste.DisciplinaTeste.Nome = cbDisciplina.Text;
                var disciplinaEscolhida = disciplinasTeste.Find(x => x.Nome.Equals(teste.DisciplinaTeste.Nome));
                teste.DisciplinaTeste.Numero = disciplinaEscolhida.Numero;

                ValidationResult resultadoValidacao = GravarRegistro(teste);

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

        private void btnGerarQuestoes_Click(object sender, EventArgs e)
        {
            numQstoesComboBox = Convert.ToInt32(cbNumeroQsts.Text);
            int i = 0;

            if (VerificacoesBtnQuestao() == true)
            {
                numQst++;

                HabilitarBotoes();

                TrazerQuestão(); // aleatorias

                ExibirCabecalho();

                ExibirQuestao(numQstoesComboBox);

                rtTeste.Text = cabecalho + questao;
            }
            else
                return;

            btnGerarQuestoes.Enabled = false;
        }

        private void btnGabarito_Click(object sender, EventArgs e)
        {
            rtTeste.Text += $"=====================================================\n" +
                               $"\n" +
                               $"Gabarito:\n" +
                               $"\n" +
                               $"" + Teste.gabarito;

            btnGabarito.Enabled = false;
        }

        private void btnGerarPDF_Click(object sender, EventArgs e)
        {
            ArquivoPDF pdf = new();

            pdf.GerarPDF_ItextSharp(rtTeste.Text);

            MessageBox.Show("Arquivo PDF gerado com sucesso!\n\n Caminho: C: -> temp -> pdf -> Teste.pdf", "Aviso");
        }

        #region não eventos
        private void ExibirQuestao(int numerosEscolhidoComboBox)
        {
            var rnd = new Random();
            int i = 0;
            questaoQueJafoi = new();

            while (i < numerosEscolhidoComboBox)
            {
                if (aleatorias.Count == 0)
                    return;

                escolhida = aleatorias[rnd.Next(aleatorias.Count)];

                if (questaoQueJafoi.Contains(escolhida))

                    continue;

                else
                {
                    questaoQueJafoi.Add(escolhida);

                    PosicionarAlternativas(escolhida);

                    questao +=
                                    $"" + (numQst) + " - " + escolhida.Pergunta + "\n" +
                                    $"\n" +
                                    $"" + altA + "\n" +
                                    $"" + altB + "\n" +
                                    $"" + altC + "\n" +
                                    $"" + altD + "\n" +
                                    $"\n";

                    aleatorias.Remove(escolhida);
                }

                numQst++;

                i++;
            }

        }

        private void ExibirCabecalho()
        {
            if (qAleatoria == null)
                return;

            cabecalho = $"Data: " + teste.DataString.ToString() + "\n" +
                            $"\n" +
                            $"Teste de " + qAleatoria.Materia.Disciplina.Nome + "\n" +
                            $"Série: " + qAleatoria.Materia.Serie.ToString() + "\n" +
                            $"\n" +
                            $"\n";
        }

        private void HabilitarBotoes()
        {
            btnOK.Enabled = true;
            btnLimpar.Enabled = true;
            btnGabarito.Enabled = true;
            btnGerarPDF.Enabled = true;
        }

        public void DesabilitarBotoesDeCima()
        {
            //panel3.Visible = false;

            btnGabarito.Visible = true;
            btnGabarito.Enabled = true;
            btnGerarPDF.Visible = false;
            btnGerarQuestoes.Visible = false;

            btnOK.Enabled = true;
            btnLimpar.Enabled = false;
            cbNumeroQsts.Enabled = false;
        }

        private bool VerificarCampos()
        {
            if(cbDisciplina.Text == "" || cbSerie.Text == "" || cbDisciplina.Text == "" || rtTeste.Text == "")
            {
                MessageBox.Show("Todos os campos devem estar com informações", "Aviso");
                return false;
            }

            return true;
        }

        private bool VerificacoesBtnQuestao()
        {
            if (questoesTeste.Count == 0)
            {
                MessageBox.Show("Não há questões pra serem exibidas", "Aviso");
                return false;
            }

            if (string.IsNullOrEmpty(cbDisciplina.Text))
            {
                MessageBox.Show("Uma disciplina deve ser selecionada", "Aviso");
                return false;
            }

            int numquest = RetornarQuantidadeQuestoesPelaDisciplinaSelecionada();

            if (numQstoesComboBox > numquest)
            {
                MessageBox.Show("Não há questões suficientes para gerar a prova\nconforme as opções escolhidas", "Aviso");
                return false;
            }

            return true;
        }

        private int RetornarQuantidadeQuestoesPelaDisciplinaSelecionada()
        {
            List<Questao> questoes = listaQuestoesFiltradas.FindAll(q => q.Materia.Disciplina.Nome.Equals(cbDisciplina.Text));

            int qtd = questoes.Count;

            return qtd;
        }

        private void PosicionarAlternativas(Questao q)
        {
            var rnd = new Random();

            AdicionarAoGabarito(q);

            q.alternativas.Add(q.Resposta); // adiciona a resposta verdadeira às outras alternativas

            falsasComVerdadeira = new();

            while (falsasComVerdadeira.Count < 4)
            {
                string alternescolhida = q.alternativas[rnd.Next(q.alternativas.Count)];

                if (falsasComVerdadeira.Contains(alternescolhida))
                {
                    continue;
                }
                else
                    falsasComVerdadeira.Add(alternescolhida);
            }

            altA = "A) " + falsasComVerdadeira[0];
            altB = "B) " + falsasComVerdadeira[1];
            altC = "C) " + falsasComVerdadeira[2];
            altD = "D) " + falsasComVerdadeira[3];

        }

        private void AdicionarAoGabarito(Questao q)
        {
            Teste.gabarito += numQst + " - " + q.Resposta + "\n";
        }

        private void TrazerQuestão()
        {
            List<Questao> filtradas = questoesTeste.FindAll(x => x.Materia.Disciplina.Nome.Equals(cbDisciplina.Text) && x.Materia.Serie.ToString().Equals(cbSerie.Text));

            if (filtradas.Count == 0)
                return;

            qAleatoria = filtradas[0];

            aleatorias = filtradas;
        }

        internal void DeixarSomenteBotaoPDF()
        {
            cbNumeroQsts.Visible = false;
            cbSerie.Visible = false;
            cbDisciplina.Visible = false;
            btnGerarQuestoes.Visible = false;
            btnGabarito.Visible = false;
            btnOK.Visible = false;
            btnLimpar.Visible = false;
            cbDisciplina.Visible = false;
            lblDisciplina.Visible = false;
            lblSerie.Visible = false;
            lblNumero.Visible = false;

            btnGerarPDF.BackColor = System.Drawing.Color.OrangeRed;
            btnGerarPDF.ForeColor = Color.White;
            btnGerarPDF.Font = new Font(Font, FontStyle.Bold);
            btnGerarPDF.Enabled = true;
        }

        private void DeixarComboboxSelecionado()
        {
            cbNumeroQsts.SelectedIndex = 0;
            cbSerie.SelectedIndex = 0;
        }

        private void CarregarDisciplinasNoTeste()
        {
            cbDisciplina.Items.Clear();

            try
            {
                var qstsFiltradas = questoesTeste.FindAll(x => x.Materia.SerieString.Equals(cbSerie.Text));

                if (qstsFiltradas.Count == 0)
                {
                    MessageBox.Show("Não foram cadastradas questões da primeira série","Aviso");
                    cbSerie.SelectedIndex = 1;
                    qstsFiltradas = questoesTeste.FindAll(x => x.Materia.SerieString.Equals(cbSerie.Items[1].ToString()));
                }

                listaQuestoesFiltradas = qstsFiltradas;

                foreach (Questao q in qstsFiltradas)
                {
                    if (cbDisciplina.Items.Count != 0)
                    {
                        if (cbDisciplina.Items.Contains(q.DisciplinaDaMateria))
                            continue;

                        else
                           cbDisciplina.Items.Add(q.Materia.Disciplina.Nome);
                    }
                    else
                        cbDisciplina.Items.Add(q.Materia.Disciplina.Nome);
                }

                cbDisciplina.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível preencher disciplinas na lista", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PrimeiroCarregamentoDisciplinasNoTeste()
        {
            cbDisciplina.Items.Clear();

            var qstsFiltradas = questoesTeste.FindAll(x => x.Materia.SerieString.Equals(cbSerie.Text));

            if (qstsFiltradas.Count == 0)
            {
                cbSerie.SelectedIndex = 1;
                qstsFiltradas = questoesTeste.FindAll(x => x.Materia.SerieString.Equals(cbSerie.Items[1].ToString()));
            }

            listaQuestoesFiltradas = qstsFiltradas;

            foreach (Questao q in qstsFiltradas)
            {
                if (cbDisciplina.Items.Count != 0)
                {
                    if (cbDisciplina.Items.Contains(q.DisciplinaDaMateria))
                        continue;

                    else
                        cbDisciplina.Items.Add(q.Materia.Disciplina.Nome);
                }
                else
                    cbDisciplina.Items.Add(q.Materia.Disciplina.Nome);
            }

            cbDisciplina.SelectedIndex = 0;
        }

        #endregion
    }
}

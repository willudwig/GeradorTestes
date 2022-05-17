using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloTeste;
using GeradorTestes.Infra.Arquivo.Compartilhado;
using GeradorTestes.Infra.Arquivo.ModuloMateria;
using GeradorTestes.Infra.Arquivo.ModuloTeste;
using GeradorTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using static GeradorTeste.Dominio.ModuloMateria.Materia;
using static GeradorTeste.Dominio.ModuloTeste.Teste;

namespace GeradorTestes.WinApp
{
    public partial class TelaCadastroTesteForm : Form
    {
        Teste teste;
        Questao qAleatoria;
        Questao escolhida;
        public List<Materia> materiasTeste;
        public List<Questao> questoesTeste;
        List<string> falsasComVerdadeira;
        List<Questao> aleatorias;
        List<Questao> questaoQueJafoi;
        Questao q;
        int numQst;
        public RepositorioMateriaArquivo rpMat;
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
            materiasTeste = new(); 
            questoesTeste = new();
            aleatorias = new();
            falsasComVerdadeira = new();
            escolhida = new();
            questaoQueJafoi = new();
            numQst = 0;
        }

        private void DeixarComboboxSelecionado()
        {
            cbNumeroQsts.SelectedIndex = 0;
            cbSerie.SelectedIndex = 0;
        }

        private void CarregarMateriasNoTeste(string serieMateria)
        {
            cbMateria.Items.Clear();

            foreach (Materia m in materiasTeste)
            {
                if (m.Serie.ToString() == serieMateria)
                {
                    cbMateria.Items.Add(m.Titulo);
                }
                        
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            rtTeste.Clear();
        }

        private void cbSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarMateriasNoTeste(cbSerie.Text);
            cbMateria.Enabled = true;
            
        }

        private void TelaCadastroTesteForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
            DeixarComboboxSelecionado();
        }

        private void CarregarTextBoxDisciplina()
        {
            var mat = materiasTeste.Find(x => x.Titulo.Equals(cbMateria.Text));
            tbDisciplina.Text = mat.Disciplina.Nome;
        }

        private void TelaCadastroTesteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            teste.Prova = rtTeste.Text;
            teste.Materia.Titulo = cbMateria.Text;
            teste.Materia.Serie = (EnumeradorSerie)cbSerie.SelectedIndex;
            teste.NumeroQuestoes = (EnumNumeroQuestoes)cbNumeroQsts.SelectedIndex;
            teste.Materia.Disciplina.Nome = tbDisciplina.Text;

            ValidationResult resultadoValidacao = GravarRegistro(teste);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void btnGerarQuestoes_Click(object sender, EventArgs e)
        {
            int numQstoesComboBox = Convert.ToInt32(cbNumeroQsts.Text);
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

        private void ExibirQuestao(int numerosEscolhidoComboBox)
        {
            var rnd = new Random();
            int i = 0;
            questaoQueJafoi = new();

            while(i < numerosEscolhidoComboBox)
            {
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
                                    $"" + altD + "\n"+
                                    $"\n";

                    aleatorias.Remove(escolhida);
                }
                
                numQst++;

                i++;
            }

        }

        private void HabilitarBotoes()
        {
            btnOK.Enabled = true;
            btnLimpar.Enabled = true;
            btnGabarito.Enabled = true;
            btnGerarPDF.Enabled = true; 
        }

        private bool VerificacoesBtnQuestao()
        {
            if (questoesTeste.Count == 0)
            {
                MessageBox.Show("Não há questões pra serem exibidas", "Aviso");
                return false;
            }

            if (string.IsNullOrEmpty(cbMateria.Text))
            {
                MessageBox.Show("Matéria deve ser selecionada", "Aviso");
                return false;
            }

            if( Convert.ToInt32(cbNumeroQsts.Text) > questoesTeste.Count)
            {
                MessageBox.Show("Número de questões insuficientes para a quantidade selecionada", "Aviso");
                return false;
            }

            return true;
        }

        private void cbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarTextBoxDisciplina();
        }
     
        private void ExibirCabecalho()
        {
            cabecalho =     $"Data: " + teste.Data.ToString() + "\n" +
                            $"\n" +
                            $"Teste de " + qAleatoria.Materia.Titulo + "\n" +
                            $"Disciplina: " + qAleatoria.Materia.Disciplina.Nome + "\n" +
                            $"Série: " + qAleatoria.Materia.Serie.ToString() + "\n" +
                            $"\n" +
                            $"\n";
        }

        private void PosicionarAlternativas(Questao q)
        {
            var rnd = new Random();
           
            AdicionarAoGabarito(q);

            q.alternativas.Add(q.Resposta); // adiciona a resposta verdadeira às outras alternativas

            falsasComVerdadeira = new();

            while(falsasComVerdadeira.Count < 4)
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

        private void btnGabarito_Click(object sender, EventArgs e)
        {
            rtTeste.Text +=    $"=====================================================\n" +
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

        private void AdicionarAoGabarito(Questao q)
        {
           Teste.gabarito += numQst + " - " + q.Resposta + "\n";
        }

        private void TrazerQuestão()
        {
            List<Questao> filtradas = questoesTeste.FindAll(x => x.TituloMateria.Equals(cbMateria.Text));

            qAleatoria = filtradas[0];

            aleatorias = filtradas;
        }
    }
}

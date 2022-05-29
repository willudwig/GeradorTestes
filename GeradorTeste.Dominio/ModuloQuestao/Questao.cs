using GeradorTeste.Dominio.Compartilhado;
using GeradorTeste.Dominio.ModuloMateria;
using System.Collections.Generic;

namespace GeradorTeste.Dominio
{
    public class Questao : EntidadeBase<Questao>
    {
        public Materia Materia { get; set; }
        public EnumeradorBimestre Bimestre { get; set; }
        public string  Pergunta { get; set; }
        public string Resposta { get; set; }

        public List<string> alternativas;

        public string TituloMateria
        {
            get
            {
                return Materia.Titulo;
            }
        }

        public string DisciplinaDaMateria
        {
            get { return Materia.Disciplina.Nome; }
        }

        public string SerieMateria
        {
            get
            {
                return Materia.Serie.ToString();
            }
        }

        public Questao()
        {
            Materia = new();
            Materia.Disciplina = new();
            alternativas = new();
        }

        public Questao(int numero, Materia materia, EnumeradorBimestre bimestre, string pergunta, string resposta)
        {
            Numero = numero;
            Materia = materia;
            Bimestre = bimestre;
            Pergunta = pergunta;
            Resposta = resposta;
        }

        public enum EnumeradorBimestre {Primeiro, Segundo, Terceiro, Quarto}

        public override void Atualizar(Questao registro)
        {
            Numero = registro.Numero;
        }
    }
}

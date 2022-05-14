using GeradorTeste.Dominio.Compartilhado;
using GeradorTeste.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;


namespace GeradorTeste.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {
        public string Prova { get; set; }
        public Materia Materia { get; set; }
        public EnumNumeroQuestoes NumeroQuestoes { get; set; }

        public string gabarito;
        public string Data
        {
            get { return DateTime.Now.ToShortDateString(); }
        }

        public string DisciplinaDaMateria
        {
            get { return Materia.Disciplina.Nome; }
        }

        public string TituloDaMateria
        {
            get { return Materia.Titulo; }
        }

        public string SerieDaMateria
        {
            get { return Materia.Serie.ToString(); }
        }

        public Teste()
        {
            Materia = new();
        }

        public Teste(string prova, Materia materia, EnumNumeroQuestoes numeroQuestoes)
        {
            Prova = prova;
            Materia = materia;
            NumeroQuestoes = numeroQuestoes;
        }

        public override void Atualizar(Teste registro)
        {
            Numero = registro.Numero;
        }

        public enum EnumNumeroQuestoes {cinco, dez, quinze, vinte}
    }
}

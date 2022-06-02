using GeradorTeste.Dominio.Compartilhado;
using GeradorTeste.Dominio.ModuloDisciplina;
using System;


namespace GeradorTeste.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {
        public DateTime data;

        public string Prova { get; set; }

        public Disciplina DisciplinaTeste { get; set; }

        public EnumNumeroQuestoes NumeroQuestoes { get; set; }

        public string gabarito;

        public string DataString
        {
            get { return data.ToString(); }
        }

        public string DisciplinaNome
        {
            get { return DisciplinaTeste.Nome; }
        }

        public string NumeroQuestoesString
        {
            get { return NumeroQuestoes.ToString(); }
        }

        public Teste()
        {
            DisciplinaTeste = new();
            data = new();
        }

        public Teste(string prova, Disciplina disciplina, EnumNumeroQuestoes numeroQuestoes)
        {
            Prova = prova;
            DisciplinaTeste = disciplina;
            NumeroQuestoes = numeroQuestoes;
        }

        public override void Atualizar(Teste registro)
        {
            Numero = registro.Numero;
        }

        public enum EnumNumeroQuestoes {cinco, dez, quinze, vinte}
    }
}

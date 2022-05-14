using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloTeste;
using GeradorTestes.Infra.Arquivo.Compartilhado.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GeradorTestes.Infra.Arquivo
{
    [Serializable]
    public class DataContext //Container
    {
        private readonly ISerializador serializador;

        public List<Questao> Questoes { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
        public List<Teste> Testes { get; set; }
        public List<Materia> Materias { get; set; }

        public DataContext()
        {
            Questoes = new List<Questao>();

            Disciplinas = new List<Disciplina>();

            Testes = new List<Teste>();

            Materias = new List<Materia>();
        }

        public DataContext(ISerializador serializador) : this()
        {
            this.serializador = serializador;

            CarregarDados();
        }

        public void GravarDados()
        {
            serializador.GravarDadosEmArquivo(this);
        }

        private void CarregarDados()
        {
            var ctx = serializador.CarregarDadosDoArquivo();

            if (ctx.Questoes.Any())
                this.Questoes.AddRange(ctx.Questoes);

            if (ctx.Disciplinas.Any())
                this.Disciplinas.AddRange(ctx.Disciplinas);

            if (ctx.Testes.Any())
                this.Testes.AddRange(ctx.Testes);

            if (ctx.Materias.Any())
                this.Materias.AddRange(ctx.Materias);
        }
    }
}

using FluentValidation;
using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Arquivo.ModuloQuestao
{
    public class RepositorioQuestaoArquivo : RepositorioEmArquivoBase<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext == null)
                return;

            if (dataContext.Questoes.Count > 0)
                contador = dataContext.Questoes.Max(x => x.Numero);
        }

        public override List<Questao> ObterRegistros()
        {
            if (dataContext == null)
            {
                dataContext = new();
                dataContext = DeserializarDados();
            }

            return dataContext.Questoes;
        }

        public override AbstractValidator<Questao> ObterValidador()
        {
            return new ValidadorQuestao();
        }

        private DataContext DeserializarDados()
        {
            SerializadorDadosEmJsonDotnet serie = new();
            return serie.CarregarDadosDoArquivo();
        }

        private void SerializarDados(DataContext dc)
        {
            SerializadorDadosEmJsonDotnet serie = new();
            serie.GravarDadosEmArquivo(dc);
        }

        ValidationResult IRepositorio<Questao>.Editar(Questao registro)
        {
            ValidationResult resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                foreach (var item in registros)
                {
                    if (item.Numero == registro.Numero)
                    {
                        item.Atualizar(registro);
                        break;
                    }
                }

                SerializarDados(dataContext);
            }

            return resultadoValidacao;
        }

        ValidationResult IRepositorio<Questao>.Excluir(Questao registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            SerializarDados(dataContext);

            return resultadoValidacao;
        }

        ValidationResult IRepositorio<Questao>.Inserir(Questao novoRegistro)
        {
            ValidationResult resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                novoRegistro.Numero = registros.Count + 1;

                registros.Add(novoRegistro);

                SerializarDados(dataContext);
            }

            return resultadoValidacao;
        }

        private ValidationResult Validar(Questao registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = ObterRegistros()
               .Select(x => x.Pergunta)
               .Contains(registro.Pergunta);

            if (nomeEncontrado && registro.Numero == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Pergunta já cadastrada"));

            return resultadoValidacao;
        }
    }
}

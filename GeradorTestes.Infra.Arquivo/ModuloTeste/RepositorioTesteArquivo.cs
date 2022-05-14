using FluentValidation;
using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Arquivo.ModuloTeste
{
    public class RepositorioTesteArquivo : RepositorioEmArquivoBase<Teste>, IRepositorio<Teste>
    {
        public RepositorioTesteArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext == null)
                return;

            if (dataContext.Testes.Count > 0)
                contador = dataContext.Testes.Max(x => x.Numero);
        }

        public override List<Teste> ObterRegistros()
        {
            if (dataContext == null)
            {
                dataContext = new();
                dataContext = DeserializarDados();
            }

            return dataContext.Testes;
        }

        public override AbstractValidator<Teste> ObterValidador()
        {
            return new ValidadorTeste();
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

        ValidationResult IRepositorio<Teste>.Editar(Teste registro)
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

        ValidationResult IRepositorio<Teste>.Excluir(Teste registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            SerializarDados(dataContext);

            return resultadoValidacao;
        }

        ValidationResult IRepositorio<Teste>.Inserir(Teste novoRegistro)
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

        private ValidationResult Validar(Teste registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = ObterRegistros()
               .Select(x => x.Prova)
               .Contains(registro.Prova);

            if (nomeEncontrado && registro.Numero == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Teste já cadastrado"));

            return resultadoValidacao;
        }
    }
}

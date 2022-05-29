using FluentValidation;
using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloDisciplina;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Arquivo.ModuloDisciplina
{
    public class RepositorioDisciplinaArquivo : RepositorioEmArquivoBase<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplinaArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext == null)
                return;
            
            if (dataContext.Disciplinas.Count > 0)
                contador = dataContext.Disciplinas.Max(x => x.Numero);
        }

        public override List<Disciplina> ObterRegistros()
        {
            if (dataContext == null)
            {
                dataContext = new();
                dataContext = DeserializarDados();
            }

            return dataContext.Disciplinas;
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

        public override AbstractValidator<Disciplina> ObterValidador()
        {
            return new ValidadorDisciplina();
        }


        ValidationResult IRepositorio<Disciplina>.Editar(Disciplina registro)
        {
            var resultadoValidacao = Validar(registro);

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

        ValidationResult IRepositorio<Disciplina>.Excluir(Disciplina registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            SerializarDados(dataContext);

            return resultadoValidacao;
        }

        ValidationResult IRepositorio<Disciplina>.Inserir(Disciplina novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                novoRegistro.Numero = registros.Count + 1;

                registros.Add(novoRegistro);

                SerializarDados(dataContext);
            }

            return resultadoValidacao;
        }

        private ValidationResult Validar(Disciplina novoRegistro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = ObterRegistros()
               .Select(x => x.Nome)
               .Contains(novoRegistro.Nome);

            if (nomeEncontrado && novoRegistro.Numero == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Nome já está cadastrado"));

            return resultadoValidacao;
        }

        public void Serializador()
        {
            SerializarDados(dataContext);
        }

    }
}

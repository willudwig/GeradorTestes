using FluentValidation;
using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Arquivo.ModuloMateria
{
    public class RepositorioMateriaArquivo : RepositorioEmArquivoBase<Materia>, IRepositorioMateria
    {
        public RepositorioMateriaArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext == null)
                return;

            if (dataContext.Materias.Count > 0)
                contador = dataContext.Materias.Max(x => x.Numero);
        }

        public override List<Materia> ObterRegistros()
        {
            if (dataContext == null)
            {
                dataContext = new();
                dataContext = DeserializarDados();
            }

            return dataContext.Materias;
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

        public override AbstractValidator<Materia> ObterValidador()
        {
            return new ValidadorMateria();
        }

        ValidationResult IRepositorio<Materia>.Editar(Materia registro)
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

        ValidationResult IRepositorio<Materia>.Excluir(Materia registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            SerializarDados(dataContext);

            return resultadoValidacao;
        }

        ValidationResult IRepositorio<Materia>.Inserir(Materia novoRegistro)
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

        private ValidationResult Validar(Materia novoRegistro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = ObterRegistros()
               .Select(x => x.Titulo)
               .Contains(novoRegistro.Titulo);

            if (nomeEncontrado && novoRegistro.Numero == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Nome já está cadastrado"));

            return resultadoValidacao;
        }
    }
}

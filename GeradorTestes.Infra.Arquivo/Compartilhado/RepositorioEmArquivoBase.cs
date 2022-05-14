using FluentValidation;
using FluentValidation.Results;
using GeradorTeste.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Arquivo
{
    public abstract class RepositorioEmArquivoBase<T> where T : EntidadeBase<T>
    {
        protected DataContext dataContext;
        protected int contador = 0;

        public RepositorioEmArquivoBase(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public abstract AbstractValidator<T> ObterValidador();

        public virtual ValidationResult Inserir(T novoRegistro)
        {
           var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                novoRegistro.Numero = registros.Count + 1;

                registros.Add(novoRegistro);

                SerializarDados(dataContext);
            }

            return resultadoValidacao;
        }

        private void SerializarDados(DataContext dc)
        {
            SerializadorDadosEmJsonDotnet serie = new();
            serie.GravarDadosEmArquivo(dc);
        }

        public virtual ValidationResult Editar(T registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

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
            }

            return resultadoValidacao;
        }
        public virtual ValidationResult Excluir(T registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            return resultadoValidacao;
        }

        public abstract List<T> ObterRegistros();

        public virtual List<T> SelecionarTodos()
        {
            return ObterRegistros().ToList();
        }
        public virtual T SelecionarPorNumero(int numero)
        {
            return ObterRegistros().FirstOrDefault(x => x.Numero == numero);
        }





    }
}

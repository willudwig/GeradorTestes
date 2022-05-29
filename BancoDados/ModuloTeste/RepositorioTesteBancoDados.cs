
using FluentValidation.Results;
using GeradorTeste.Dominio.ModuloTeste;
using System.Collections.Generic;

namespace BancoDados.ModuloTeste
{
    public class RepositorioTesteBancoDados : IRepositorioTeste
    {
        ConexaoBancoDados cbd;

        public RepositorioTesteBancoDados()
        {
            cbd = new();
        }

        public ValidationResult Editar(Teste registro)
        {
            var validador = new ValidadorTeste();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.EditarTesteNoBancoDados(registro);

            return resultadoValidador;
        }

        public ValidationResult Excluir(Teste registro)
        {
            var validador = new ValidadorTeste();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.ExcluirTesteNoBancoDados(registro.Numero);

            return resultadoValidador;
        }

        public ValidationResult Inserir(Teste novoRegistro)
        {
            var validador = new ValidadorTeste();

            var resultadoValidador = validador.Validate(novoRegistro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.InserirtTesteBancoDados(novoRegistro);

            novoRegistro.Numero = cbd.id;

            return resultadoValidador;
        }

        public Teste SelecionarPorNumero(int numero)
        {
            return cbd.SelecionarTestePorNumero(numero);
        }

        public List<Teste> SelecionarTodos()
        {
            return cbd.SelecionarTodosTeste();
        }
    }
}

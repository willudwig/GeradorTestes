using FluentValidation.Results;
using GeradorTeste.Dominio.ModuloDisciplina;
using System.Collections.Generic;

namespace BancoDados.ModuloDisciplina
{
    public class RepositorioDisciplinaBancoDados : IRepositorioDisciplina
    {
        ConexaoBancoDados cbd;

        public RepositorioDisciplinaBancoDados()
        {
            cbd = new();
        }

        public ValidationResult Editar(Disciplina registro)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.EditarDisciplinaNoBancoDados(registro);

            return resultadoValidador;
        }

        public ValidationResult Excluir(Disciplina registro)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.ExcluirDisciplinaNoBancoDados(registro.Numero);

            return resultadoValidador;
        }

        public ValidationResult Inserir(Disciplina novoRegistro)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidador = validador.Validate(novoRegistro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.InserirDisciplinaNoBancoDeDados(novoRegistro.Nome);

            novoRegistro.Numero = cbd.id;

            return resultadoValidador;
        }

        public Disciplina SelecionarPorNumero(int numero)
        {
            return cbd.SelecionarDisciplinaPorNumero(numero);
        }

        public List<Disciplina> SelecionarTodos()
        {
            return cbd.SelecionarTodosDisciplina();
        }
    }
}

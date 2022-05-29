using FluentValidation.Results;
using GeradorTeste.Dominio.ModuloMateria;
using System.Collections.Generic;


namespace BancoDados.ModuloMateria
{
    public class RepositorioMateriaBancoDados : IRepositorioMateria
    {
        ConexaoBancoDados cbd;

        public RepositorioMateriaBancoDados()
        {
            cbd = new();
        }

        public ValidationResult Editar(Materia registro)
        {
            var validador = new ValidadorMateria();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.EditarMateriaNoBancoDados(registro);

            return resultadoValidador;
        }

        public ValidationResult Excluir(Materia registro)
        {
            var validador = new ValidadorMateria();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.ExcluirMateriaNoBancoDados(registro.Numero);
            
            return resultadoValidador;
        }

        public ValidationResult Inserir(Materia novoRegistro)
        {
            var validador = new ValidadorMateria();

            var resultadoValidador = validador.Validate(novoRegistro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.InserirMateriaoBancoDados(novoRegistro);

            novoRegistro.Numero = cbd.id;

            return resultadoValidador;
        }

        public Materia SelecionarPorNumero(int numero)
        {
            return cbd.SelecionarMateriaPorNumero(numero);
        }

        public List<Materia> SelecionarTodos()
        {
            return cbd.SelecionarTodosMateria();
        }
    }
}

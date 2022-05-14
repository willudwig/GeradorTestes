using FluentValidation.Results;
using GeradorTeste.Dominio.Compartilhado;
using System.Collections.Generic;


namespace GeradorTeste.Dominio
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        ValidationResult Inserir(T novoRegistro);

        ValidationResult Editar(T registro);

        ValidationResult Excluir(T registro);

        List<T> SelecionarTodos();

        T SelecionarPorNumero(int numero);
    }
}

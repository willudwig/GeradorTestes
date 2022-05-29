using FluentValidation.Results;
using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDados.ModuloQuestao
{
    public class RepositorioQuestaoBancoDados : IRepositorioQuestao
    {
        ConexaoBancoDados cbd;

        public RepositorioQuestaoBancoDados()
        {
            cbd = new();
        }

        public void AdicionarAlternativas(Questao questao)
        {
            cbd.AdicionarAlternativas(questao);
        }

        public ValidationResult Editar(Questao registro)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.EditarQuestaoNoBancoDados(registro);

            return resultadoValidador;
        }

        public void EditarAlternativas(Questao questao)
        {
            cbd.EditarAlternativaBancoDados(questao);
        }

        public ValidationResult Excluir(Questao registro)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidador = validador.Validate(registro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.ExcluirQuestaoNoBancoDados(registro.Numero);

            return resultadoValidador;
        }

        public ValidationResult Inserir(Questao novoRegistro)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidador = validador.Validate(novoRegistro);

            if (resultadoValidador.IsValid == false)
                return resultadoValidador;

            cbd.InserirQuestaoBancoDados(novoRegistro);

            novoRegistro.Numero = cbd.id;

            return resultadoValidador;
        }

        public Questao SelecionarPorNumero(int numero)
        {
            return cbd.SelecionarQuestaoPorNumero(numero);
        }

        public List<Questao> SelecionarTodos()
        {
            return cbd.SelecionarTodosQuestao();
        }
    }
}

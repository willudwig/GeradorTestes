using FluentValidation;

namespace GeradorTeste.Dominio.ModuloQuestao
{
    public class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.Pergunta).NotNull().NotEmpty();
            RuleFor(x => x.Resposta).NotNull().NotEmpty();
        }
    }
}

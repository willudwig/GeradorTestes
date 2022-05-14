using FluentValidation;


namespace GeradorTeste.Dominio.ModuloDisciplina
{
    public class ValidadorDisciplina : AbstractValidator<Disciplina>
    {
        public ValidadorDisciplina()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty();
        }
    }
}

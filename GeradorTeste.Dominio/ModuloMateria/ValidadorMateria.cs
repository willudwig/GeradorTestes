using FluentValidation;

namespace GeradorTeste.Dominio.ModuloMateria
{
    public class ValidadorMateria : AbstractValidator<Materia>
    {
        public ValidadorMateria()
        {
            RuleFor(x => x.Titulo).NotNull().NotEmpty();
        }
    }
}

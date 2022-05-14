using FluentValidation;


namespace GeradorTeste.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Prova).NotNull().NotEmpty();
        }
    }
}

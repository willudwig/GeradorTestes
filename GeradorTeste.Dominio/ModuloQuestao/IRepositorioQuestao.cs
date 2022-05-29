


namespace GeradorTeste.Dominio.ModuloQuestao
{
    public interface IRepositorioQuestao : IRepositorio<Questao>
    {
        void AdicionarAlternativas(Questao questao);
        void EditarAlternativas(Questao questao);
    }
}

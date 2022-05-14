
namespace GeradorTestes.Infra.Arquivo.Compartilhado.Interfaces
{
    public interface ISerializador
    {
        DataContext CarregarDadosDoArquivo();

        void GravarDadosEmArquivo(DataContext dados);
    }
}

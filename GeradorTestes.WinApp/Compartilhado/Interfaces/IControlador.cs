
using System.Windows.Forms;

namespace GeradorTestes.WinApp.Compartilhado
{
    public interface IControlador
    {
        void Inserir();
        void Editar();
        void Excluir();
        UserControl ObtemListagem();
        IConfiguracaoToolStrip ObtemConfiguracaoToolStrip();
        void GerarPDF();
    }
}

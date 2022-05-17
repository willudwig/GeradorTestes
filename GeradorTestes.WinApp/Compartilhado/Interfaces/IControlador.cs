using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        void ExibirTelaGerarPDF();

    }
}

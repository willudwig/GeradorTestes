using GeradorTestes.Infra.Arquivo;
using GeradorTestes.Infra.Arquivo.Compartilhado.Interfaces;
using System;
using System.Windows.Forms;

namespace GeradorTestes.WinApp
{
    internal static class Program
    {
        static ISerializador serializador = new SerializadorDadosEmJsonDotnet();

        static DataContext contexto = new DataContext(serializador);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new TelaPrincipalForm());
            }
            catch(InvalidOperationException invex)
            {
                if (invex.Message == "System.InvalidOperationException")
                    return;
            }

           // contexto.GravarDados();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            contexto.GravarDados();
        }
    }
}

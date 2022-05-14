using GeradorTestes.WinApp.Compartilhado;


namespace GeradorTestes.WinApp.ModuloMateria
{
    public class ConfiguracaoToolStripMateria : IConfiguracaoToolStrip
    {
        public string TipoCadastro => "Cadastro de Matéria";

        public string TooltipInserir => "inserir nova matéria";

        public string TooltipEditar => "editar matéria selecionada";

        public string TooltipExcluir => "excluir matéria selecionada";
    }
}

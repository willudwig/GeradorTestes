using GeradorTestes.WinApp.Compartilhado;


namespace GeradorTestes.WinApp.ModuloQuestao
{
    public class ConfiguracaoToolStripQuestao : IConfiguracaoToolStrip
    {
        public string TipoCadastro => "Cadastro de Questão";

        public string TooltipInserir => "inserir nova questão";

        public string TooltipEditar => "editar questão";

        public string TooltipExcluir => "excluir questão";
    }
}

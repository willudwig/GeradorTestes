using GeradorTestes.WinApp.Compartilhado;
using System;


namespace GeradorTestes.WinApp.ModuloTeste
{
    public class ConfiguracaoToolStripTeste : IConfiguracaoToolStrip
    {
        public string TipoCadastro => "Cadastro de Teste";

        public string TooltipInserir => "inserir um novo teste";

        public string TooltipEditar => "editar um teste selecionado";

        public string TooltipExcluir => "excluir um teste selecionado";
    }
}

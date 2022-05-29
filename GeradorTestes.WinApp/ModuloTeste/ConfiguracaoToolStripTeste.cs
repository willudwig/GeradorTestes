using GeradorTestes.WinApp.Compartilhado;
using System;


namespace GeradorTestes.WinApp.ModuloTeste
{
    public class ConfiguracaoToolStripTeste : IConfiguracaoToolStrip
    {
        public string TipoCadastro => "Cadastro de Teste";

        public string TooltipInserir => "inserir um novo teste";

        public string TooltipEditar => "não é possível editar testes";

        public string TooltipExcluir => "excluir um teste selecionado";
    }
}

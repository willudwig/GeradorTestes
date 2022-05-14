using GeradorTestes.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public class ConfiguracaoToolStripDisciplina : IConfiguracaoToolStrip
    {
        public string TipoCadastro => "Cadastro de Disciplina";

        public string TooltipInserir => "inserir nova disciplina";

        public string TooltipEditar => "editar disciplina selecionada";

        public string TooltipExcluir => "excluir disciplina selecionada";
    }
}

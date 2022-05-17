
using System.IO;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace GeradorTestes.Infra.Arquivo.Compartilhado
{
    public class ArquivoPDF
    {
        public void GerarPDF_Sharp(string texto)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var doc = new PdfSharp.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                var font = new PdfSharp.Drawing.XFont("Arial", 14);

                textFormatter.DrawString(texto, font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, 0, page.Width, page.Height));

                doc.Save("Teste.pdf");
                //System.Diagnostics.Process.Start("Teste.pdf");
            }
        }

        public void GerarPDF_ItextSharp(string texto)
        {
            string nomeArquivo = @"C:\temp\pdf\Teste.pdf";
            FileStream arquivoPDF = new(nomeArquivo, FileMode.Create);
            Document doc = new(PageSize.A4);
            PdfWriter escritorPDF = PdfWriter.GetInstance(doc, arquivoPDF);

            string dados = "";

            Paragraph paragrafo = new(dados, new Font(Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold));
            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Add("Gerador de Testes 1.0\n\n");

            paragrafo.Font = new Font(Font.NORMAL, 12, (int)System.Drawing.FontStyle.Regular);
            paragrafo.Alignment = Element.ALIGN_LEFT;
            paragrafo.Add(texto + "\n");

            doc.Open();
            doc.Add(paragrafo);
            doc.Close();
        }
    }
}

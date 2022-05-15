using GeradorTestes.Infra.Arquivo.Compartilhado.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GeradorTestes.Infra.Arquivo
{
    public class SerializadorDadosEmJsonDotnet : ISerializador
    {
        private const string arquivo = @"C:\temp\dados.json";

        public DataContext CarregarDadosDoArquivo()
        {
            if (File.Exists(arquivo) == false)
                return new DataContext();

            string arquivoJson = File.ReadAllText(arquivo);

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            return JsonConvert.DeserializeObject<DataContext>(arquivoJson, settings);
        }

        public void GravarDadosEmArquivo(DataContext dados)
        {
            try
            {
                string folder = @"C:\temp\";

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.Formatting = Formatting.Indented;
                settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

                string arquivoJson = JsonConvert.SerializeObject(dados, settings);

                File.WriteAllText(arquivo, arquivoJson);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.IO;

namespace FullAtuSeletor
{
    public class Arquivo
    {
        public string enderecoOrigem { get; set; } = string.Empty;
        public string enderecoDestino { get; set; } = string.Empty;

        public Arquivo()
        {

        }

        public void Gravar(Arquivo dados)
        {
            dados.enderecoOrigem = $@"\\172.25.100.248\wms246\Builds\WMS\Outros\Import_Full_Updater\Atu";
            string caminhoPasta = $@"{AppDomain.CurrentDomain.BaseDirectory}\config.json";

            File.WriteAllText(caminhoPasta, JsonConvert.SerializeObject(dados).ToString());
        }
        public Arquivo Ler()
        {
            if (File.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\config.json"))
            {

                var objeto = JsonConvert.DeserializeObject<Arquivo>(File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\config.json"));
                return objeto;
            }
            else {
                return new Arquivo();

            
            }
        }
    }
}




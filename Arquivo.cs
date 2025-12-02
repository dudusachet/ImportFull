using Newtonsoft.Json;
using System;
using System.IO;

namespace FullAtuSeletor
{
    public class Arquivo
    {
        public string enderecoOrigem { get; set; }
        public string enderecoDestino { get; set; }

        public Arquivo()
        {

        }

        public void Gravar(Arquivo dados)
        {
            dados.enderecoOrigem = $@"\\172.25.100.248\wms246\Builds\WMS\Outros\ImpValidator\Atu";
            string caminhoPasta = $@"{AppDomain.CurrentDomain.BaseDirectory}\config.json";

            File.WriteAllText(caminhoPasta, JsonConvert.SerializeObject(dados).ToString());
        }
        public Arquivo Ler()
        {
            var objeto = JsonConvert.DeserializeObject<Arquivo>(File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\config.json"));
            return objeto;
        }
    }
}

    


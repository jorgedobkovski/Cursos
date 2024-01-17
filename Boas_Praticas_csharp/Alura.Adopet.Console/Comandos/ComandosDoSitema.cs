using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console.Comandos
{
    internal class ComandosDoSitema
    {
        private Dictionary<string, IComando> comandosDoSistema = new()
        {
            {"help", new Help() },
            {"import", new Import() },
            {"list", new List() },
            {"show", new Show() },
        };
        public IComando? this[string key] => comandosDoSistema.ContainsKey(key) ? comandosDoSistema[key] : null;
        /*
         * esse indexador permite acessar instâncias de classes que implementam a interface IComando com base em uma chave (nome do comando) 
         * fornecida como parâmetro. Se a chave corresponder a um comando no dicionário comandosDoSistema, a instância desse comando será 
         * retornada; caso contrário, será retornado null. Isso é útil para buscar comandos específicos a partir de um nome, fornecendo uma 
         * maneira conveniente de acessar e executar diferentes funcionalidades do sistema.
         */
    }
}

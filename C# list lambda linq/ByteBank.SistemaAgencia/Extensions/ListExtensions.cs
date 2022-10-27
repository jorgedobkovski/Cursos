using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.SistemaAgencia.Extensions
{
    public static class ListExtensions //Uma classe com métodos de extenão não pode ser genérica, os métodos podem ser genéricos.
    {
        public static void AdicionarVarios<T>(this List<T> lista, params T[] itens)
        {
            foreach (var item in itens)
            {
                lista.Add(item);
            }
        }
    }
}

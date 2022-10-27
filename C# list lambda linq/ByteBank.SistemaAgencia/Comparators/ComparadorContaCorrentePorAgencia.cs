using ByteBank.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.SistemaAgencia.Comparators
{
    public class ComparadorContaCorrentePorAgencia : IComparer<ContaCorrente>
    {
        public int Compare(ContaCorrente x, ContaCorrente y)
        {
            if (x == y) return 0; // equivalentes
            if (x == null) return 1; //y fica na frente
            if (y == null) return -1; //x fica na frente
            if (x.Agencia < y.Agencia) return -1; //x fica na frente
            if (x.Agencia == y.Agencia) return 0; //equivalentes
            return 1; //y fica na frente

            //poderia usar: return x.Agencia.CompareTo(y.Agencia);
        }
    }
}

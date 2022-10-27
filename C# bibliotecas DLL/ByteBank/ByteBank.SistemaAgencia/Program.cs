using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteBank.Modelos;
using Humanizer;

namespace ByteBank.SistemaAgencia
{
    class Program
    {
        static void Main(string[] args)
        {
            //ContaCorrente conta1 = new ContaCorrente(123, 123212);
            //Console.WriteLine(conta1.Numero);

            //DateTime dataFimPagamento = new DateTime(2022, 8, 17);
            //DateTime dataCorrente = DateTime.Now;

            //TimeSpan diferenca = dataFimPagamento - dataCorrente;

            //string mensagem = "Vencimento em " + TimeSpanHumanizeExtensions.Humanize(diferenca);
            //Console.WriteLine(mensagem);

            string url = "pagina?argumentos";

            string argumentos = url.Substring(7);

            Console.WriteLine(url);
            Console.WriteLine(argumentos);

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank
{
    class Program
    {
        static void Main(string[] args)
        {
            SyntaxSugar();
            Console.ReadLine();
        }

        private static void SyntaxSugar()
        {
            using (LeitorDeArquivo leitor = new LeitorDeArquivo("contas.txt"))
            {
                leitor.LerProximaLinha();
                leitor.LerProximaLinha();
                leitor.LerProximaLinha();
            }
        }
        private static void CarregarContas() 
        {
            LeitorDeArquivo leitor = new LeitorDeArquivo("contas.txt");
            try
            {
                leitor.LerProximaLinha();
                leitor.LerProximaLinha();
                leitor.LerProximaLinha();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Exceção do tipo FileNotFoundException capturada e tratada;");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceção do tipo IOException capturada e tratada;");
            }
            finally
            {
                leitor.Fechar();
            }
            
        }

        private static void TestaInnerException()
        {
            try
            {
                ContaCorrente conta1 = new ContaCorrente(4564, 789684);
                ContaCorrente conta2 = new ContaCorrente(7891, 456794);

                conta1.Transferir(10000, conta2);
            }
            catch (OperacaoFinanceiraException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                Console.WriteLine("Informações da INNER EXCEPTION (exceção interna):");

                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.InnerException.StackTrace);
            }
        }

    }
}

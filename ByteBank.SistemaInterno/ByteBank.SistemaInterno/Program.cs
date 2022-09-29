using System;
using ByteBank.Modelos;

namespace ByteBank.SistemaInterno
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ContaCorrente conta = new ContaCorrente(1231,12321);

            conta.Sacar(10);

            Console.WriteLine(conta.Numero);

            Console.ReadLine();
        }
    }
}

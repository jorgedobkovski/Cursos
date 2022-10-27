using ByteBankImportacaoExportacao.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBankImportacaoExportacao
{
    partial class Program
    {
        static void UsandoStreamReader()
        {

            var arquivo = "contas.txt";
            using (var fluxoDoArquivo = new FileStream(arquivo, FileMode.Open))
            using (var leitor = new StreamReader(fluxoDoArquivo))
            {
                while (!leitor.EndOfStream)
                {
                    var linha = leitor.ReadLine();
                    var contaCorrente = ConverterStringParaContaCorrente(linha);

                    Console.WriteLine($"{contaCorrente.Titular.Nome} : Numero {contaCorrente.Numero}, ag. {contaCorrente.Agencia}, Saldo: {contaCorrente.Saldo}");
                }
            }

            Console.ReadLine();
        }

        static ContaCorrente ConverterStringParaContaCorrente(string linha)
        {
            var campos = linha.Split(','); //retorna um array string[]

            var agencia = campos[0];
            var numero = campos[1];
            var saldo = campos[2].Replace('.', ',');
            var titular = campos[3];

            var agenciaComoInt = int.Parse(agencia);
            var numeroComoInt = int.Parse(numero);
            var saldoComoDouble = double.Parse(saldo);
            var titularConta = new Cliente();
            titularConta.Nome = titular;

            var resultado = new ContaCorrente(agenciaComoInt, numeroComoInt);
            resultado.Depositar(saldoComoDouble);
            resultado.Titular = titularConta;

            return resultado;
        }

    }
}

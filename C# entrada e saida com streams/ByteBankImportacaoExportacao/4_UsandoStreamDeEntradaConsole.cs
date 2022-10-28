using System.IO;
using System.Text;
using System;

namespace ByteBankImportacaoExportacao
{
    partial class Program
    {
        static void LerEntradaConsole()
        {
            using (var fluxoDeEntrada = Console.OpenStandardInput())
            using (var fs = new FileStream("arquivoDaConsole.txt", FileMode.Create))
            {
                var buffer = new byte[1024];
                while (true)
                {
                    var bytesLidos = fluxoDeEntrada.Read(buffer, 0, 1024);
                    fs.Write(buffer, 0, bytesLidos);
                    fs.Flush();
                    Console.WriteLine($"Bytes lidos da console: {bytesLidos}");
                }
            }
        }

        static void UsarConsoleComandos()
        {
            Console.WriteLine("Digite seu nome:");
            string nome = Console.ReadLine();

            Console.WriteLine($"Olá, {nome}");
        }

        static void UsarClasseFile()
        {
            File.WriteAllText("escrevendoComAClasseFile.txt", "Testando File.WriteAllText");
            Console.WriteLine("Arquivo escrevendoComAClasseFile.txt criado!");

            var bytesArquivo = File.ReadAllBytes("contas.txt");
            Console.WriteLine($"Arquivo contas.txt possui {bytesArquivo.Length} bytes");

            var linhas = File.ReadAllLines("contas.txt");

            foreach(var line in linhas)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine(linhas.Length);
        }
    }
}

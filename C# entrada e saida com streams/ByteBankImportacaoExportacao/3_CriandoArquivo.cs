using System.IO;
using System.Text;
using System;

namespace ByteBankImportacaoExportacao
{
    partial class Program
    {
        static void CriarArquivo()
        {
            var caminhoDoArquivo = "contasExportadas.csv";

            using (var fluxoDeArquivo = new FileStream(caminhoDoArquivo, FileMode.Create))
            {
                var contaComoString = "1893,812398,238.50,Jorge Luiz";
                var encoding = Encoding.UTF8;
                var bytes = encoding.GetBytes(contaComoString);

                fluxoDeArquivo.Write(bytes, 0, bytes.Length);
            }

            Console.WriteLine("arquivo criado.");

        }

        static void CriarArquivoComWriter()
        {
            var caminhoDoArquivo = "contasExportadas.csv";
            using(var fluxoDeArquivo = new FileStream(caminhoDoArquivo, FileMode.Create))
            using(var escritor = new StreamWriter(fluxoDeArquivo))
            {
                var contaComoString = "1803,991368,238.50,Pedro Alcantara";
                escritor.WriteLine(contaComoString);
            }

            Console.WriteLine("arquivo criado com writer.");
        }

        static void TestarFlush()
        {
            var caminhoDoArquivo = "arquivoDeTeste.txt";
            using (var fluxoDeArquivo = new FileStream(caminhoDoArquivo, FileMode.Create))
            using (var escritor = new StreamWriter(fluxoDeArquivo))
            {
                for(int i = 0; i < 199; i++)
                {
                    escritor.WriteLine($"Linha {i}");

                    escritor.Flush(); // Despeja o buffer para o Stream

                    Console.WriteLine($"Linha {i} escrita no documento");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("arquivo criado para teste do método flush.");
        }

        static void CriarArquivoBinario()
        {
            var arquivo = "dadosBinarios.txt";
            using(var fs = new FileStream(arquivo, FileMode.Create))
            using(var escritor = new BinaryWriter(fs))
            {
                escritor.Write(18236);
                escritor.Write(102);
                escritor.Write(1736.97);
                escritor.Write("Fátima Terezinha");
            }

            Console.WriteLine("arquivo binário criado.");
        }

    }
}

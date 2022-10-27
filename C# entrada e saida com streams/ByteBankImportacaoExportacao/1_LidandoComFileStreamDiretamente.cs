using System.IO;
using System.Text;
using System;

namespace ByteBankImportacaoExportacao
{
    partial class Program
    {
        static void LidandoComFileStreamDiretamente()
        {
            var arquivo = "contas.txt";
            using (var fluxoDoArquivo = new FileStream(arquivo, FileMode.Open))
            {
                var buffer = new byte[1024];
                var quantidadeBytesLidos = -1;

                while (quantidadeBytesLidos != 0)
                {
                    quantidadeBytesLidos = fluxoDoArquivo.Read(buffer, 0, 1024);
                    EscreverBuffer(buffer, quantidadeBytesLidos);
                }
            }

            Console.ReadLine();
        }

        static void EscreverBuffer(byte[] buffer, int bytesLidos)
        {
            var encoding = Encoding.Default;
            var texto = encoding.GetString(buffer, 0, bytesLidos);

            Console.Write(texto);
        }
    }
}

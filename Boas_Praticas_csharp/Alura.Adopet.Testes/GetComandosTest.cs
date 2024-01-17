using Alura.Adopet.Console.Comandos;
using Alura.Adopet.Console.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Testes
{
    public class GetComandosTest
    {
        [Fact]
        public void ListaDeComandoNaoDeveSerNula()
        {
            //Arrange
            Assembly assemblyComOTipoDocComando = Assembly.GetAssembly(typeof(DocComando))!;

            //act
            var dicionario = GetComandos.ComandosToDictionary(assemblyComOTipoDocComando);

            //assert
            Assert.NotNull(dicionario);
        }

        [Fact]
        public void QuandoExistemComandosDeveRetornarDicionarioNaoVazio()
        {
            //Arrange
            Assembly assemblyComOTipoDocComando = Assembly.GetAssembly(typeof(DocComando))!;

            //Act
            var dicionario = GetComandos.ComandosToDictionary(assemblyComOTipoDocComando);

            //Assert
            Assert.NotEmpty(dicionario);
        }
    }
}

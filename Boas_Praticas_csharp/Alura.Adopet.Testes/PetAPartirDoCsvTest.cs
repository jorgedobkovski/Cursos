using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Testes
{
    public class PetAPartirDoCsvTest
    {
        [Fact]
        public void QuandoStringForValidaDeveRetornarUmPet()
        {
            //Arrange
            string linha = "456b24f4-19e2-4423-845d-4a80e8854a41;Lima Limão;1";            

            //Act
            Pet pet = linha.ConverteDoTexto();

            //Assert
            Assert.NotNull(pet);
        }

        [Fact]
        public void QuandoStringForNulaDeveLançarUmaArgumentException()
        {
            //Arrange
            string? linha = null;

            //Act + Assert
            Assert.ThrowsAny<ArgumentException>(() => linha.ConverteDoTexto());           
        }

        [Fact]
        public void QuandoStringForVaziaDeveLancarArgumentException()
        {
            //Arrange
            string linha = ""; ;

            //Act + Assert
            Assert.ThrowsAny<ArgumentException>(() => linha.ConverteDoTexto());
        }

        [Fact]
        public void QuandoQuantidadeDeCamposForInsuficienteDeveLancarExcecao()
        {
            //Arrange
            string linha = "456b24f4-19e2-4423-845d-4a80e8854a41;Lima Limão"; ;

            //Act + Assert
            Assert.ThrowsAny<Exception>(() => linha.ConverteDoTexto());
        }

        [Fact]
        public void QuandoGuidForInvalidoDeveLancarExcecao()
        {
            //Arrange
            string linha = "guidInvalido;Lima Limão;1"; ;

            //Act + Assert
            Assert.ThrowsAny<Exception>(() => linha.ConverteDoTexto());
        }

        [Fact]
        public void QuandoTipoDoPetForInvalidoDeveLancarExcecao()
        {
            //Arrange
            string linha = "456b24f4-19e2-4423-845d-4a80e8854a41;Lima Limão;8"; 

            //Act + Assert
            Assert.ThrowsAny<Exception>(() => linha.ConverteDoTexto());
        }

    }
}

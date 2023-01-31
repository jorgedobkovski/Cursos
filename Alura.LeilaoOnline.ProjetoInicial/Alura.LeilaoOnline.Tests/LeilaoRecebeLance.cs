using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //arranje
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulan0", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert

            var qntdObtida = leilao.Lances.Count();

            Assert.Equal(1, qntdObtida);
        }

        [Theory]
        [InlineData(2, new double[] {800,900})]
        [InlineData(4, new double[] {100,1200,1400,1300})]

        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int qntdEsperada, double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulan0", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for(var i = 0; i < ofertas.Length; i++)
            {
                if(i%2==0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qntdObtida = leilao.Lances.Count();

            Assert.Equal(qntdEsperada, qntdObtida);
        }
    }
}

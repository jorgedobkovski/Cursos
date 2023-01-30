using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(2, new double[] {800,900})]
        [InlineData(4, new double[] {100,1200,1400,1300})]

        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int qntdEsperada, double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulan0", leilao);

            foreach(var oferta in ofertas)
            {
                leilao.RecebeLance(fulano, oferta);
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

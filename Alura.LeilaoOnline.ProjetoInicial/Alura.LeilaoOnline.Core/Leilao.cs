﻿using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoEmAndamento,
        LeilaoFinalizado
    }
    public class Leilao
    {
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; set; }

        public Leilao(string peca)
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
            Peca = peca;
            _lances = new List<Lance>();
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (Estado == EstadoLeilao.LeilaoEmAndamento)
            {
                _lances.Add(new Lance(cliente, valor));
            }
        }

        public void IniciaPregao()
        {

        }

        public void TerminaPregao()
        {
            Estado = EstadoLeilao.LeilaoFinalizado;
            Ganhador = Lances
            .DefaultIfEmpty(new Lance(null, 0))
            .OrderBy(l => l.Valor)
            .LastOrDefault();
        }
    }
}

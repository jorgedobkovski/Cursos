using Alura.Adopet.Console.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console.Util
{
    public static class PetAPartirDoCsv
    {
        public static Pet ConverteDoTexto(this string linha)
        {
            if (string.IsNullOrEmpty(linha)) throw new ArgumentException("Texto não pode ser nulo ou vazia!");
            
            string[]? propriedades = linha.Split(';');

            if (propriedades.Length != 3) throw new Exception("Quantidade insuficiente de informações!");

            bool sucessoAoConverterGuidValido = Guid.TryParse(propriedades[0], out Guid petId);
            if (!sucessoAoConverterGuidValido) throw new ArgumentException("Guid inválido");

            if (propriedades[2] != "0" && propriedades[2] != "1") throw new ArgumentException("Tipo de pet inválido");

            Pet pet = new Pet(petId,
            propriedades[1],
            int.Parse(propriedades[2]) == 1 ? TipoPet.Gato : TipoPet.Cachorro
            );
            return pet;
        }
    }
}

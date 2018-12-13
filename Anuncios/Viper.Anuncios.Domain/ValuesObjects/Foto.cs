using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Anuncios.Domain.ValuesObjects
{

    public class Foto : ValueObject<Foto>
    {
        public Uri EnderecoFoto { get; private set; }

        public Foto(Uri enderecoFoto)
        {
            new Contract().Requires()
                          .IsNotNull(enderecoFoto, nameof(EnderecoFoto), "O endereço da foto é inválido.")
                          .ThrowExceptionIfInvalid();

            EnderecoFoto = enderecoFoto;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EnderecoFoto;
        }

        public override string ToString()
        {
            return EnderecoFoto.ToString();
        }
    }
}

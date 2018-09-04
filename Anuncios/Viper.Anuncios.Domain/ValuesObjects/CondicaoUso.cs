using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Anuncios.Domain.ValuesObjects
{

    public class CondicaoUso : ValueObject<CondicaoUso>
    {
        public string Descricao { get; private set; }

        public static CondicaoUso Novo => new CondicaoUso(nameof(Novo));
        public static CondicaoUso Usado => new CondicaoUso(nameof(Usado));

        private CondicaoUso(string descricao)
        {
            Descricao = descricao;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Descricao;
        }
    }
}

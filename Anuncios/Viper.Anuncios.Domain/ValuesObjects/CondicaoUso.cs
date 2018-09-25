using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Anuncios.Domain.ValuesObjects
{

    public class CondicaoUso : Enumeration<CondicaoUso>
    {
        public static readonly CondicaoUso Novo = new CondicaoUso(1, nameof(Novo));
        public static readonly CondicaoUso Usado = new CondicaoUso(2, nameof(Usado));

        private CondicaoUso(int id, string descricao)
            : base(id, descricao)
        {
            
        }
    }
}

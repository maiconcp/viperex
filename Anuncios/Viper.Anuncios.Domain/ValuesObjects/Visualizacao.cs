using System;
using System.Collections.Generic;
using Viper.Common;

namespace Viper.Anuncios.Domain.ValuesObjects
{
    public class Visualizacao : ValueObject<Visualizacao> 
    {
        public DateTime Data { get; private set; }    

        public Visualizacao(DateTime data)
        {
            Data = data;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Data;
        }    
    }
}

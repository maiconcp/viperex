using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.ValuesObjects;

namespace Viper.Celulares.Domain.Entities
{
    public class Anuncio : Anuncios.Domain.Entities.Anuncio
    {
        public Anuncio(string titulo, string descricao, decimal preco, CondicaoUso condicaoUso, bool aceitoTroca) 
            : base(titulo, descricao, preco, condicaoUso, aceitoTroca)
        {
        }
    }
}

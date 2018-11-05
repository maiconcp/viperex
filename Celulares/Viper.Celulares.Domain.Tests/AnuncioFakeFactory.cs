using System;
using System.Collections.Generic;
using System.Text;
using Viper.Celulares.Domain.Entities;
using Viper.Anuncios.Domain.ValuesObjects;

namespace Viper.Celulares.Domain.Tests
{
    public class AnuncioFakeFactory
    {
        public static Anuncio CriarAnuncioValido()
        {
            return new Anuncio("Titulo", "Descricao", 10m, CondicaoUso.Usado, aceitoTroca: true);
        }

        public static Anuncio CriarAnuncioPorStatus(Status status)
        {
            var anuncio = AnuncioFakeFactory.CriarAnuncioValido();

            if (status.EhPublicado())
            {
                anuncio.Publicar();
            }
            else if (status.EhRejeitado())
            {
                anuncio.Rejeitar();
            }
            else if (status.EhVendido())
            {
                anuncio.Publicar();
                anuncio.Vender();
            }
            else if (status.EhExcluido())
            {
                anuncio.Excluir();
            }

            return anuncio;
        }
    }
}

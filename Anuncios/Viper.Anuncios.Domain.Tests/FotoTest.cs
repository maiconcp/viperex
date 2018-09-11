using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;
using Xunit;

namespace Viper.Anuncios.Domain.Tests
{
    public class FotoTest
    {
        private const string URL_PNG_VALIDA = "http://www.viperex.com.br/images/foto1.png";

        [Fact]
        public void Construtor_UrlValida_ObjetoCriado()
        {
            // Arrange
            // Act
            var foto = new Foto(new Uri(URL_PNG_VALIDA));

            // Assert
            Assert.NotNull(foto);
        }

        [Fact]
        public void Construtor_UrlInvalida_DomainException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Foto(null));
        }
    }
}

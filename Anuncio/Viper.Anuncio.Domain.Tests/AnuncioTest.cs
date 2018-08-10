using System;
using Xunit;
using Viper.Anuncio.Domain.Entities;
using Viper.Common;

namespace Viper.Anuncio.Domain.Tests
{
    public class AnuncioTest
    {
        [Fact]
        public void Construtor_DadosValido_ObjetoCriado()
        {
            // Arrange
            // Act
            var anuncio = new Anuncio2("Titulo", "Descricao", 10.0M);
            
            // Assert
            Assert.Equal("Titulo", anuncio.Titulo);
            Assert.Equal("Descricao", anuncio.Descricao);
            Assert.Equal(10.0M, anuncio.Preco);
            Assert.Equal(1, anuncio.DomainEvents.Count);
        }

                [Fact]
        public void MarcarComoVendido_Vendido_AnuncioMarcadoComoVendido()
        {
            // Arrange
            var anuncio = new Anuncio2("Titulo", "Descricao", 10.0M);

            // Act
            anuncio.MarcarComoVendido();
            
            // Assert
            Assert.True(anuncio.Vendido);
            Assert.Equal(DateTime.Now.Date, anuncio.DataDaVenda.Date);  
            Assert.Equal(2, anuncio.DomainEvents.Count);
        }
    }
}

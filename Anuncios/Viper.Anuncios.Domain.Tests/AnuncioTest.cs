using System;
using Xunit;
using Viper.Anuncios.Domain.Entities;
using Viper.Common;

namespace Viper.Anuncios.Domain.Tests
{
    public class AnuncioTest
    {
        [Fact]
        public void Construtor_DadosValido_ObjetoCriado()
        {
            // Arrange
            // Act
            var anuncio = new Anuncio("Titulo", "Descricao", 10.0M);
            
            // Assert
            Assert.Equal("Titulo", anuncio.Titulo);
            Assert.Equal("Descricao", anuncio.Descricao);
            Assert.Equal(10.0M, anuncio.Preco);
            Assert.Equal(1, anuncio.DomainEvents.Count);
        }

        [Theory]
        [InlineData("", "Descricao", 10)]
        [InlineData("Titulo", "", 10)]
        [InlineData("Título", "Descricao", 0)]
        [InlineData("Título", "Descricao", -10)]
        public void Construtor_DadosInvalido_DomainException(string titulo, string descricao, decimal valor)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Anuncio(titulo, descricao, valor));
        }

        [Fact]
        public void MarcarComoVendido_AnuncioAindaNaoVendido_AnuncioMarcadoComoVendido()
        {
            // Arrange
            var anuncio = new Anuncio("Titulo", "Descricao", 10.0M);

            // Act
            anuncio.MarcarComoVendido();
            
            // Assert
            Assert.True(anuncio.Vendido);
            Assert.Equal(DateTime.Now.Date, anuncio.DataDaVenda.Date);  
            Assert.Equal(2, anuncio.DomainEvents.Count);
        }

        [Fact]
        public void MarcarComoVendido_AnuncioJaVendido_DomainException()
        {
            // Arrange
            var anuncio = new Anuncio("Titulo", "Descricao", 10.0m);
            anuncio.MarcarComoVendido();

            // Act          
            // Assert
            Assert.Throws<DomainException>(() => anuncio.MarcarComoVendido());
        }
    }
}

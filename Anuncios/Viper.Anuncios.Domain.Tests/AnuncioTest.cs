using System;
using Xunit;
using Viper.Anuncios.Domain.Entities;
using Viper.Common;
using Viper.Anuncios.Domain.ValuesObjects;

namespace Viper.Anuncios.Domain.Tests
{
    public class AnuncioTest
    {
        private Anuncio CriarAnuncioValido()
        {
            return new Anuncio("Titulo", "Descricao", 10m);
        }

        [Fact]
        public void Construtor_DadosValido_ObjetoCriado()
        {
            // Arrange
            // Act
            var anuncio = CriarAnuncioValido();
            
            // Assert
            Assert.Equal("Titulo", anuncio.Titulo);
            Assert.Equal("Descricao", anuncio.Descricao);
            Assert.Equal(10.0M, anuncio.Preco);
            Assert.Equal(Status.Pendente, anuncio.Status);
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
            var anuncio = CriarAnuncioValido();
            anuncio.Publicar();
            // Act
            anuncio.MarcarComoVendido();
            
            // Assert
            Assert.Equal(Status.Vendido, anuncio.Status);
            Assert.Equal(DateTime.Now.Date, anuncio.DataDaVenda.Date);             
        }

        [Fact]
        public void MarcarComoVendido_AnuncioJaVendido_DomainException()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            anuncio.Publicar();
            anuncio.MarcarComoVendido();

            // Act          
            // Assert
            Assert.Throws<DomainException>(() => anuncio.MarcarComoVendido());
        }

        [Fact] 
        public void Visualizar_PrimeiraVisualizacao_VisualizacaoRegistrada()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();

            // Act          
            anuncio.Visualizar();

            // Assert
            Assert.Equal(1, anuncio.Visualizacoes.Count);
        }

        [Fact]
        public void Publicar_AnuncioPendente_AnuncioPublicado()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();

            // Act
            anuncio.Publicar();

            // Assert
            Assert.True(anuncio.Status.EhPublicado());
        }

        [Fact]
        public void Publicar_AnuncioJaPublicado_DomainException()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            anuncio.Publicar();

            // Act
            // Assert
            Assert.Throws<DomainException>(() => anuncio.Publicar());
        }
    }
}

using System;
using Xunit;
using Viper.Anuncios.Domain.Entities;
using Viper.Common;
using Viper.Anuncios.Domain.ValuesObjects;
using System.Collections.Generic;
using System.Linq;

namespace Viper.Anuncios.Domain.Tests
{
    public class AnuncioTest
    {
        private Anuncio CriarAnuncioValido()
        {
            return new Anuncio("Titulo", "Descricao", 10m);
        }

        private Anuncio CriarAnuncioPorStatus(Status status)
        {
            var anuncio = CriarAnuncioValido();

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
            anuncio.Vender();

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
            anuncio.Vender();

            // Act          
            // Assert
            Assert.Throws<DomainException>(() => anuncio.Vender());
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

        [Fact]
        public void Rejeitar_AnuncioPendente_AnuncioRejeitado()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();

            // Act
            anuncio.Rejeitar();

            // Assert
            Assert.True(anuncio.Status.EhRejeitado());
        }

        [Fact]
        public void Rejeitar_AnuncioJaPublicado_DomainException()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            anuncio.Publicar();

            // Act
            // Assert
            Assert.Throws<DomainException>(() => anuncio.Rejeitar());
        }

        [Fact]
        public void Excluir_AnuncioEmStatusQuePodemSerExcluido_AnuncioExcluido()
        {
            foreach (var status in Status.Todos)
            {
                if (!status.PodeSerExcluido())
                    continue;

                // Arrange
                var anuncio = CriarAnuncioPorStatus(status);

                // Act
                anuncio.Excluir();

                // Assert
                Assert.True(anuncio.Status.EhExcluido());
            }
        }

        [Fact]
        public void Excluir_AnuncioEmStatusQueNaoPodemSerExcluido_DomainException()
        {
            foreach (var status in Status.Todos)
            {
                if (status.PodeSerExcluido())
                    continue;

                // Arrange
                var anuncio = CriarAnuncioPorStatus(status);

                // Act
                // Assert
                Assert.Throws<DomainException>(() => anuncio.Excluir());
            }
        }
    }
}
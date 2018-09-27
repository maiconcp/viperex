using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Celulares.Domain.Entities;
using Viper.Common;
using Xunit;

namespace Viper.Celulares.Domain.Tests
{
    public class AnuncioTest
    {
        private Acessorio capinha = new Acessorio("Capinha");

        [Fact]
        public void AdicionarAcessorio_AnuncioSemAcessorio_AnuncioComUmAcessorio()
        {
            // Arrange
            var anuncio = AnuncioFakeFactory.CriarAnuncioValido();
            var acessorio = capinha;

            // Act
            anuncio.AdicionarAcessorio(acessorio.Id);

            // Assert
            Assert.Equal(1, anuncio.Acessorios.Count);
        }

        [Fact]
        public void AdicionarAcessorio_AcessorioJaIncluido_DomainException()
        {
            // Arrange
            var anuncio = AnuncioFakeFactory.CriarAnuncioValido();
            anuncio.AdicionarAcessorio(capinha.Id);

            // Act
            // Assert
            Assert.Throws<DomainException>(() => anuncio.AdicionarAcessorio(capinha.Id));
        }

        [Theory]
        [InlineData(nameof(Status.Publicado))]
        [InlineData(nameof(Status.Excluido))]
        [InlineData(nameof(Status.Rejeitado))]
        [InlineData(nameof(Status.Vendido))]        
        public void AdicionarAcessorio_StatusNaoEhPendente_DomainException(string descricaoStatus)
        {
            // Arrange
            var status = Status.FromDescription(descricaoStatus);
            var anuncio = AnuncioFakeFactory.CriarAnuncioPorStatus(status);

            // Act
            // Assert
            Assert.Throws<DomainException>(() => anuncio.AdicionarAcessorio(capinha.Id));

        }

    }
}


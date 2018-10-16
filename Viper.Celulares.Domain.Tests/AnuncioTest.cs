using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.Events;
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

        [Fact]
        public void Reidratar_Condicao_Resultado()
        {
            // Arrange
            var anuncioId = Identity.CreateNew();
            var foto = new Foto(new Uri("http://viperex.com.br/foto.png"));
            var anuncioCadastrado = new AnuncioCadastradoEvent(anuncioId, "Titulo", "Descricao", preco: 10.0M, condicaoUso: CondicaoUso.Novo, aceitoTroca: true);
            var fotoIncluida = new FotoAdicionadaAnuncioEvent(anuncioId, foto);
            var anuncioPublicado = new AnuncioPublicadoEvent(anuncioId);

            var eventos = new List<DomainEventBase>() { anuncioCadastrado, fotoIncluida, anuncioPublicado };

            // Act
            var anuncio =  Anuncio.Reidratar<Anuncio>(eventos);

            // Assert
            Assert.Equal(anuncioId, anuncio.Id);
            Assert.Equal("Titulo", anuncio.Titulo);
            Assert.Equal("Descricao", anuncio.Descricao);
            Assert.Equal(10.0m, anuncio.Preco);
            Assert.Equal(CondicaoUso.Novo, anuncio.CondicaoUso);
            Assert.True(anuncio.AceitoTroca);

            Assert.Equal(foto, anuncio.Fotos.Capa);

            Assert.Equal(Status.Publicado, anuncio.Status);
        }
    }
}


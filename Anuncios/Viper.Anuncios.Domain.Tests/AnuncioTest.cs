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
            return new Anuncio("Titulo", "Descricao", 10m, CondicaoUso.Usado);
        }

        private const string URL_PNG_VALIDA = "http://www.viperex.com.br/images/foto1.png";

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
            Assert.Equal(CondicaoUso.Usado, anuncio.CondicaoUso);
            Assert.Equal(1, anuncio.DomainEvents.Count);
        }

        [Theory]
        [InlineData("", "Descricao", 10, "Novo")]
        [InlineData("Titulo", "", 10, "Novo")]
        [InlineData("Título", "Descricao", 0, "Novo")]
        [InlineData("Título", "Descricao", -10, "Novo")]
        [InlineData("Título", "Descricao", 10, null)]
        public void Construtor_DadosInvalido_DomainException(string titulo, string descricao, decimal valor, string condicaoUso)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Anuncio(titulo, descricao, valor, condicaoUso == "Novo" ? CondicaoUso.Novo : null));
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

        [Fact]
        public void AdicionarFoto_AdicionarPrimeiraFoto_FotoDefinidaComoCapa()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();            
            var foto = new Foto(new Uri(URL_PNG_VALIDA));

            // Act
            anuncio.AdicionarFoto(foto);

            // Assert
            Assert.Equal(foto, anuncio.Fotos.Capa);
        }

        [Fact]
        public void AdicionarFoto_AdicionarSegundaFoto_APrimeiraFotoContinuaComoCapa()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            var foto1 = new Foto(new Uri(URL_PNG_VALIDA));
            var foto2 = new Foto(new Uri(URL_PNG_VALIDA + "2.png"));
            anuncio.AdicionarFoto(foto1);

            // Act
            anuncio.AdicionarFoto(foto2);

            // Assert
            Assert.Equal(foto1, anuncio.Fotos.Capa);
        }

        [Fact]
        public void AdicionarFoto_AdicionarSextaFoto_AlbumCompleto()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();

            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "1.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "2.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "3.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "4.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "5.png")));
            
            // Act
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "6.png")));

            // Assert
            Assert.True(anuncio.Fotos.Completo);
        }

        [Fact]
        public void AdicionarFoto_AdicionarSetimaFoto_DomainException()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();

            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "1.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "2.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "3.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "4.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "5.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "6.png")));

            // Act
            // Assert
            Assert.Throws<DomainException>(() => anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "7.png"))));
        }

        [Fact]
        public void Adicionar_AdicionarFotoDiretamentePeloAlbumFoto_NaoDeveAlterarOAlbum()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();

            // Act
            anuncio.Fotos.Adicionar(new Foto(new Uri(URL_PNG_VALIDA + "1.png")));

            // Assert
            Assert.True(anuncio.Fotos.Vazio);
        }

        [Fact]
        public void RemoverFoto_AlbumVazio_DomainException()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            var foto = new Foto(new Uri(URL_PNG_VALIDA));

            // Act
            // Assert
            Assert.Throws<DomainException>(() => anuncio.RemoverFoto(foto));
        }

        [Fact]
        public void RemoverFoto_RemoverFotoQueNaoEhDoAlbum_DomainException()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            var foto = new Foto(new Uri(URL_PNG_VALIDA));
            var foto2 = new Foto(new Uri(URL_PNG_VALIDA + "2.png"));

            // Act
            anuncio.AdicionarFoto(foto);

            // Assert
            Assert.Throws<DomainException>(() => anuncio.RemoverFoto(foto2));
        }

        [Fact]
        public void RemoverFoto_AlbumComDuasFotosAoRemoverPrimeiraFoto_SegundaFotoSeTornaCapa()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            var primeiraFoto = new Foto(new Uri(URL_PNG_VALIDA));
            var segundaFoto = new Foto(new Uri(URL_PNG_VALIDA + "2.png"));
            anuncio.AdicionarFoto(primeiraFoto);
            anuncio.AdicionarFoto(segundaFoto);

            // Act

            anuncio.RemoverFoto(primeiraFoto);

            // Assert
            Assert.Equal(segundaFoto, anuncio.Fotos.Capa);
        }

        [Fact]
        public void RemoverFoto_AlbumComUmaFotoAoRemover_AlbumVazio()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            var primeiraFoto = new Foto(new Uri(URL_PNG_VALIDA));
            anuncio.AdicionarFoto(primeiraFoto);

            // Act
            anuncio.RemoverFoto(primeiraFoto);

            // Assert
            Assert.True(anuncio.Fotos.Vazio);
        }

        [Fact]
        public void Remover_RemoverFotoDiretamentePeloAlbumFoto_NaoDeveAlterarOAlbum()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            var primeiraFoto = new Foto(new Uri(URL_PNG_VALIDA));
            anuncio.AdicionarFoto(primeiraFoto);

            // Act
            anuncio.Fotos.Remover(primeiraFoto);

            // Assert
            Assert.False(anuncio.Fotos.Vazio);
        }

        [Fact]
        public void Limpar_LimparFotoDiretamentePeloAlbumFoto_NaoDeveAlterarOAlbum()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            var primeiraFoto = new Foto(new Uri(URL_PNG_VALIDA));
            anuncio.AdicionarFoto(primeiraFoto);

            // Act
            anuncio.Fotos.Limpar();

            // Assert
            Assert.False(anuncio.Fotos.Vazio);
        }

        [Fact]
        public void RemoverTodasFotos_AlbumComFotos_AlbumVazio()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "1.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "2.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "3.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "4.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "5.png")));
            anuncio.AdicionarFoto(new Foto(new Uri(URL_PNG_VALIDA + "6.png")));

            // Act
            anuncio.RemoverTodasFotos();

            // Assert
            Assert.True(anuncio.Fotos.Vazio);
        }

        [Fact]
        public void RemoverTodasFotos_AlbumSemFotos_AlbumVazioNaoLancaExcecao()
        {
            // Arrange
            var anuncio = CriarAnuncioValido();

            // Act
            anuncio.RemoverTodasFotos();

            // Assert
            Assert.True(anuncio.Fotos.Vazio);
        }
    }
}
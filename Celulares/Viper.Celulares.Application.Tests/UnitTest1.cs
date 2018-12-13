using System;
using Xunit;
using Viper.Celulares.Application.Commands;
using NSubstitute;
using Viper.Common;
using Viper.Celulares.Domain.Entities;
using Viper.Anuncios.Domain.ValuesObjects;

namespace Viper.Celulares.Application.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            string guid_anuncio = "998BA42D-8FA0-405B-AB6A-CF1D5105A663";
            string guid_acessorio = "F72BD96C-3BF9-48D3-9FBD-561BE96FE9E5";
            var command = new AdicionarAcessorioAnuncioCommand()
            {
                AnuncioId = guid_anuncio,
                AcessorioId = guid_acessorio
            };

            // criar um EventStore Fake
            var anuncio = new Anuncio("Titulo", "Descricao", 10.0M, CondicaoUso.Usado, aceitoTroca: true);
            var eventStore = new EventStoreFake<Anuncio>(anuncio);

            // criar um instancia do CommandHandler
            var handler = new AdicionarAcessorioAnuncioCommandHandler(eventStore);

            // Act
            handler.Handle(command);

            // Assert
            // Avaliar se foi salvo no EventStore / meu anuncio tem um evento ()
            Assert.NotNull(eventStore.AggregateStored);
            Assert.Contains(new Identity(guid_acessorio), eventStore.AggregateStored.Acessorios);
        }
    }

    public class EventStoreFake<T> : IEventStore<T> where T : AggregateRoot
    {
        public T AggregateToRead { get; set; }
        public T AggregateStored { get; private set; }

        public EventStoreFake(T aggregateToRead)
        {
            AggregateToRead = aggregateToRead;
        }

        public T Get(Identity id)
        {
            return AggregateToRead;
        }

        public void Store(T aggregateRoot)
        {
            AggregateStored = aggregateRoot;
        }
    }
    /*
    public class AnuncioController // : ApiController
    {
        private AdicionarAcessorioAnuncioCommandHandler _adicionarAcessorioAnuncioCommandHandler;
        public AnuncioController(AdicionarAcessorioAnuncioCommandHandler adicionarAcessorioAnuncioCommandHandler)
        {
            _adicionarAcessorioAnuncioCommandHandler = adicionarAcessorioAnuncioCommandHandler;
        }

        [HttpPost("/api/celulares/{id}/acessorios")]
        public void AdicionarAcessorio(string id, [FromBody] AdicionarAcessorioAnuncioCommand comando)
        {
            _adicionarAcessorioAnuncioCommandHandler.Handle(comando);
        }
    }

    public class Startup
    {
        public void Init()
        {
            // Injeção
            
        }
    }
    */
}

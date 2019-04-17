using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Celulares.Application.Commands;
using Viper.Celulares.Domain.Entities;
using Viper.Common;

namespace Viper.Celulares.Api.Controllers
{
    [Route("api/v1/celulares/[controller]")]
    [ApiController]
    public class AnunciosController : ControllerBase
    {
        private readonly IEventStore<Anuncio> AnuncioEventStore;
        private readonly ICommandHandler<CadastrarAnuncioCommand, Anuncio> CadastrarAnuncioCommandHandler;
        private readonly ICommandHandler<AdicionarAcessorioAnuncioCommand, bool> AdicionarAcessorioAnuncioCommandHandler;

        public AnunciosController(
            IEventStore<Anuncio> anuncioEventStore, 
            ICommandHandler<CadastrarAnuncioCommand, Anuncio> cadastrarAnuncioCommandHandler, 
            ICommandHandler<AdicionarAcessorioAnuncioCommand, bool> adicionarAcessorioAnuncioCommandHandler)
        {
            AnuncioEventStore = anuncioEventStore;
            CadastrarAnuncioCommandHandler = cadastrarAnuncioCommandHandler;
            AdicionarAcessorioAnuncioCommandHandler = adicionarAcessorioAnuncioCommandHandler;
        }

        // api/{versao}/{dominio}/{recurso}
        // POST api/v1/celulares/fabricantes -> inclui fabricante
        // POST api/v1/celulares/modelos -> inclui modelo
        // POST api/v1/celulares/acessorios -> inclui acessorio (capinha)

        [HttpGet("{id}")]    
        public ActionResult ObterAnuncio(string id)
        {
            return Ok(AnuncioEventStore.Get(new Identity(id)));
        }


        [HttpGet]
        public ActionResult ObterTodosAnuncios()
        {
            return Ok(new List<Anuncio>() { AnuncioEventStore.Get(new Identity(Guid.NewGuid())) });
        }

        // POST api/v1/celulares/anuncios -> inclui anuncio
        [HttpPost()]
        public ActionResult CadastrarAnuncio([FromBody] CadastrarAnuncioCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return BadRequest(command.Notifications);            

            var anuncioCriado = (CadastrarAnuncioCommandHandler.Handle(command));

            return Created(Url.Action(nameof(ObterAnuncio), new { id = anuncioCriado.Id.ToString() }), anuncioCriado);
        }

        // POST api/v1/celulares/anuncios/123/acessorios -> inclui um acessorio no anuncio
        [HttpPost("{id}/acessorios")]
        public object AdicionarAcessorioAoAnuncio(string id, [FromBody] AdicionarAcessorioAnuncioCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return BadRequest(command.Notifications);

            if (string.IsNullOrEmpty(command.AnuncioId))
                command.AnuncioId = id;

            if (id != command.AnuncioId)
                throw new InvalidOperationException();

            return AdicionarAcessorioAnuncioCommandHandler.Handle(command);
        }
    }

    public class AnuncioEventStore : IEventStore<Anuncio> 
    {
        public Anuncio AggregateToRead { get; set; }
        public Anuncio AggregateStored { get; private set; }

        public AnuncioEventStore()
        {
            AggregateToRead = new Anuncio("Titulo", "Descricao", 10.0M, CondicaoUso.Usado, aceitoTroca: true);
        }

        public Anuncio Get(Identity id)
        {
            return AggregateToRead;
        }

        public void Store(Anuncio aggregateRoot)
        {
            AggregateStored = aggregateRoot;
        }
    }


    public class EventStore<T> : IEventStore<T> where T: AggregateRoot
    {
        public T AggregateToRead { get; set; }
        public T AggregateStored { get; private set; }

        public EventStore()
        {
            AggregateToRead = new Anuncio("Titulo", "Descricao", 10.0M, CondicaoUso.Usado, aceitoTroca: true) as T;
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
}
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
        // api/{versao}/{dominio}/{recurso}
        // POST api/v1/celulares/fabricantes -> inclui fabricante
        // POST api/v1/celulares/modelos -> inclui modelo
        // POST api/v1/celulares/acessorios -> inclui acessorio (capinha)

        [HttpGet("{id}")]    
        public ActionResult ObterAnuncio(string id)
        {
            var eventStore = new AnuncioEventStore();

            return Ok(eventStore.Get(new Identity(id)));
        }


        [HttpGet]
        public ActionResult ObterTodosAnuncios()
        {
            var eventStore = new AnuncioEventStore();

            return Ok(new List<Anuncio>() { eventStore.Get(new Identity(Guid.NewGuid())) });
        }

        // POST api/v1/celulares/anuncios -> inclui anuncio
        [HttpPost()]
        public ActionResult CadastrarAnuncio([FromBody] CadastrarAnuncioCommand command)
        {
            var eventStore = new AnuncioEventStore();
            var handler = new CadastrarAnuncioCommandHandler(eventStore);

            command.Validate();

            if (command.Invalid)
                return BadRequest(command.Notifications);            

            var anuncioCriado = (handler.Handle(command));

            return Created(Url.Action(nameof(ObterAnuncio), new { id = anuncioCriado.Id.ToString() }), anuncioCriado);
        }

        // POST api/v1/celulares/anuncios/123/acessorios -> inclui um acessorio no anuncio
        [HttpPost("{id}/acessorios")]
        public object AdicionarAcessorioAoAnuncio(string id, [FromBody] AdicionarAcessorioAnuncioCommand command)
        {
            if (string.IsNullOrEmpty(command.AnuncioId))
                command.AnuncioId = id;

            if (id != command.AnuncioId)
                throw new InvalidOperationException();

            var eventStore = new AnuncioEventStore();
            var handler = new AdicionarAcessorioAnuncioCommandHandler(eventStore);

            return handler.Handle(command);
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
}
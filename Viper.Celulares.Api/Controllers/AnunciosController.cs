﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Validations;
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
        private readonly ICommandBus CommandBus;

        public AnunciosController(
            IEventStore<Anuncio> anuncioEventStore,
            ICommandBus commandBus)
        {
            AnuncioEventStore = anuncioEventStore;
            CommandBus = commandBus;
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
            var anuncioCriado = CommandBus.Send<CadastrarAnuncioCommand, Anuncio>(command);

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

            return CommandBus.Send<AdicionarAcessorioAnuncioCommand, bool>(command);
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


    public class EventStore<T> : IEventStore<T> where T : AggregateRoot
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
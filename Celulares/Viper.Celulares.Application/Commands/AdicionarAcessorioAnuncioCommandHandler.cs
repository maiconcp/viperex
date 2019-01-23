using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Viper.Celulares.Domain.Entities;
using Viper.Common;

namespace Viper.Celulares.Application.Commands
{
    public class AdicionarAcessorioAnuncioCommandHandler : ICommandHandler<AdicionarAcessorioAnuncioCommand, bool>
    {
        private readonly IEventStore<Anuncio> _anuncioEventStore;

        public AdicionarAcessorioAnuncioCommandHandler(IEventStore<Anuncio> anuncioEventStore)
        {
            _anuncioEventStore = anuncioEventStore;
        }        

        public bool Handle(AdicionarAcessorioAnuncioCommand command)
        {
            command.Validate();
            command.ThrowExceptionIfInvalid();

            var anuncioId = new Identity(command.AnuncioId);
            var acessorioId = new Identity(command.AcessorioId);

            var anuncio = _anuncioEventStore.Get(anuncioId);

            anuncio.AdicionarAcessorio(acessorioId);

            _anuncioEventStore.Store(anuncio);

            return true;
        }
    }
}

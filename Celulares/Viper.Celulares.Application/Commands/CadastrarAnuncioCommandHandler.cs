using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Celulares.Domain.Entities;
using Viper.Common;

namespace Viper.Celulares.Application.Commands
{
    public class CadastrarAnuncioCommandHandler : ICommandHandler<CadastrarAnuncioCommand, Anuncio>
    {
        private readonly IEventStore<Anuncio> _anuncioEventStore;

        public CadastrarAnuncioCommandHandler(IEventStore<Anuncio> anuncioEventStore)
        {
            _anuncioEventStore = anuncioEventStore;
        }

        public Anuncio Handle(CadastrarAnuncioCommand command)
        {
            command.Validate();
            command.ThrowExceptionIfInvalid();

            var condicaoUso = CondicaoUso.FromValueOrDescription(command.CondicaoUso);

            var anuncio = new Anuncio(command.Titulo, command.Descricao, command.Preco, condicaoUso, command.AceitoTroca);

            _anuncioEventStore.Store(anuncio);

            return anuncio;
        }
 
    }
}

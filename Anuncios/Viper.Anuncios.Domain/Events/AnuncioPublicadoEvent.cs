using System;
using Flunt.Validations;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    public class AnuncioPublicadoEvent : DomainEventBase
    {
        public AnuncioPublicadoEvent(Identity aggregateID) : base(aggregateID)
        {            
        }

        private AnuncioPublicadoEvent(Identity aggregateID, long aggregateVersion) : base(aggregateID, aggregateVersion)
        {            
        }

        public override DomainEventBase WithAggregate(Identity aggregateId, long aggregateVersion)
        {
            return new AnuncioPublicadoEvent(aggregateId, aggregateVersion);
        }
    }
}

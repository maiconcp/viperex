using System;
using Flunt.Validations;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    public class AnuncioPublicadoEvent : DomainEventBase
    {
        public AnuncioPublicadoEvent(Guid aggregateID) : base(aggregateID)
        {            
        }

        private AnuncioPublicadoEvent(Guid aggregateID, long aggregateVersion) : base(aggregateID, aggregateVersion)
        {            
        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new AnuncioPublicadoEvent(aggregateId, aggregateVersion);
        }
    }
}

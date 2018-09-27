using System;
using Flunt.Validations;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    public class AnuncioRejeitadoEvent : DomainEventBase
    {
        public AnuncioRejeitadoEvent(Identity aggregateID) : base(aggregateID)
        {            
        }

        private AnuncioRejeitadoEvent(Identity aggregateID, long aggregateVersion) : base(aggregateID, aggregateVersion)
        {            
        }

        public override DomainEventBase WithAggregate(Identity aggregateId, long aggregateVersion)
        {
            return new AnuncioRejeitadoEvent(aggregateId, aggregateVersion);
        }
    }
}
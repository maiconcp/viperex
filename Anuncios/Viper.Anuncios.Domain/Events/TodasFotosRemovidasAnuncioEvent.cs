using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    
    public class TodasFotosRemovidasAnuncioEvent : DomainEventBase
    {        
        public TodasFotosRemovidasAnuncioEvent(Guid aggregateID) : base(aggregateID)
        {

        }

        private TodasFotosRemovidasAnuncioEvent(Guid aggregateID, long aggregateVersion) : base(aggregateID, aggregateVersion)
        {

        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new TodasFotosRemovidasAnuncioEvent(aggregateId, aggregateVersion);
        }
    }
}

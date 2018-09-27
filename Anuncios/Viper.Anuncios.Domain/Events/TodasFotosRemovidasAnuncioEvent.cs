using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    
    public class TodasFotosRemovidasAnuncioEvent : DomainEventBase
    {        
        public TodasFotosRemovidasAnuncioEvent(Identity aggregateID) : base(aggregateID)
        {

        }

        private TodasFotosRemovidasAnuncioEvent(Identity aggregateID, long aggregateVersion) : base(aggregateID, aggregateVersion)
        {

        }

        public override DomainEventBase WithAggregate(Identity aggregateId, long aggregateVersion)
        {
            return new TodasFotosRemovidasAnuncioEvent(aggregateId, aggregateVersion);
        }
    }
}

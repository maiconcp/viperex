using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Celulares.Domain.Events
{

    public class AcessorioAdicionadoAoAnuncioEvent : DomainEventBase
    {
        public Identity AcessorioId { get; private set; }
        public AcessorioAdicionadoAoAnuncioEvent(Identity aggregateID, Identity acessorioId) : base(aggregateID)
        {
            AcessorioId = acessorioId;
        }

        private AcessorioAdicionadoAoAnuncioEvent(Identity aggregateID, long aggregateVersion, Identity acessorioId) : base(aggregateID, aggregateVersion)
        {
            AcessorioId = acessorioId;
        }

        public override DomainEventBase WithAggregate(Identity aggregateId, long aggregateVersion)
        {
            return new AcessorioAdicionadoAoAnuncioEvent(aggregateId, aggregateVersion, AcessorioId);
        }
    }
}

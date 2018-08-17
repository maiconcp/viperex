using System;
using Flunt.Validations;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    public class AnuncioVendidoEvent : DomainEventBase
    {
        public DateTime DataDaVenda { get; private set; }

        public AnuncioVendidoEvent(Guid aggregateID, DateTime dataDaVenda) : base(aggregateID)
        {
            DataDaVenda = dataDaVenda;
        }

        private AnuncioVendidoEvent(Guid aggregateID, long aggregateVersion, DateTime dataDaVenda) : base(aggregateID, aggregateVersion)
        {
            DataDaVenda = dataDaVenda;
        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new AnuncioVendidoEvent(aggregateId, aggregateVersion, DataDaVenda);
        }
    }
}
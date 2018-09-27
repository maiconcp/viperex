using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Celulares.Domain.Events
{

    public class AcessorioCadastradoEvent : DomainEventBase
    {
        public string Descricao { get; private set; }
        public AcessorioCadastradoEvent(Identity aggregateID, string descricao) : base(aggregateID)
        {
            Descricao = descricao;
        }

        private AcessorioCadastradoEvent(Identity aggregateID, long aggregateVersion, string descricao) : base(aggregateID, aggregateVersion)
        {
            Descricao = descricao;
        }

        public override DomainEventBase WithAggregate(Identity aggregateId, long aggregateVersion)
        {
            return new AcessorioCadastradoEvent(aggregateId, aggregateVersion, Descricao);
        }
    }
}

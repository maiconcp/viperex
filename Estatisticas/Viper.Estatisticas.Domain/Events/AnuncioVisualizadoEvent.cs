using System;
using Flunt.Validations;
using Viper.Estatisticas.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Estatisticas.Domain.Events
{
    public class AnuncioVisualizadoEvent : DomainEventBase
    {
        public Visualizacao Visualizacao { get; private set; }

        public AnuncioVisualizadoEvent(Identity aggregateID, Visualizacao visualizacao) : base(aggregateID)
        {
            Visualizacao = visualizacao;
        }

        private AnuncioVisualizadoEvent(Identity aggregateID, long aggregateVersion, Visualizacao visualizacao) : base(aggregateID, aggregateVersion)
        {
            Visualizacao = visualizacao;
        }

        public override DomainEventBase WithAggregate(Identity aggregateId, long aggregateVersion)
        {
            return new AnuncioVisualizadoEvent(aggregateId, aggregateVersion, Visualizacao);
        }
    }
}
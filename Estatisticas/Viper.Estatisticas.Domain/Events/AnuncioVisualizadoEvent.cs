using System;
using Flunt.Validations;
using Viper.Estatisticas.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Estatisticas.Domain.Events
{
    public class AnuncioVisualizadoEvent : DomainEventBase
    {
        public Visualizacao Visualizacao { get; private set; }

        public AnuncioVisualizadoEvent(Guid aggregateID, Visualizacao visualizacao) : base(aggregateID)
        {
            Visualizacao = visualizacao;
        }

        private AnuncioVisualizadoEvent(Guid aggregateID, long aggregateVersion, Visualizacao visualizacao) : base(aggregateID, aggregateVersion)
        {
            Visualizacao = visualizacao;
        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new AnuncioVisualizadoEvent(aggregateId, aggregateVersion, Visualizacao);
        }
    }
}
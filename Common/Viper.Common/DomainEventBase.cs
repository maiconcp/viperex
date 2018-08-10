using System;
using System.Collections.Generic;
using System.Linq;

namespace Viper.Common
{
    public abstract class DomainEventBase : ValueObject<DomainEventBase>, IDomainEvent
    {
        protected DomainEventBase()
        {
            EventId = Guid.NewGuid();
        }

        protected DomainEventBase(Guid aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEventBase(Guid aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Guid EventId { get; private set; }

        public Guid AggregateId { get; private set; }

        public long AggregateVersion { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EventId;
            yield return AggregateId;
            yield return AggregateVersion;
        }
        public abstract DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion);
    }
}
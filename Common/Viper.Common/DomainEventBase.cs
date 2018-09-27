using System;
using System.Collections.Generic;
using System.Linq;

namespace Viper.Common
{
    public abstract class DomainEventBase : ValueObject<DomainEventBase>, IDomainEvent
    {
        protected DomainEventBase()
        {
            EventId = Identity.CreateNew();
        }

        protected DomainEventBase(Identity aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEventBase(Identity aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Identity EventId { get; private set; }

        public Identity AggregateId { get; private set; }

        public long AggregateVersion { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EventId;
            yield return AggregateId;
            yield return AggregateVersion;
        }
        public abstract DomainEventBase WithAggregate(Identity aggregateId, long aggregateVersion);
    }
}
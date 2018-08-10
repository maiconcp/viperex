using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

namespace Viper.Common
{
    public abstract class AggregateRoot : Entity
    {
        public const long NewAggregateVersion = -1;
        private long _version = NewAggregateVersion;
        private readonly List<DomainEventBase> _domainEvents = new List<DomainEventBase>();
        public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents;
        private void ApplyEvent(DomainEventBase @event, long version)
        {
            if (!_domainEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                _version = version;
            }
        }
        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent: DomainEventBase
        {
            DomainEventBase eventWithAggregate = @event.WithAggregate(
                Equals(Id, default(Guid)) ? @event.AggregateId : Id, 
                _version);

            ApplyEvent(eventWithAggregate, _version + 1);
            _domainEvents.Add(eventWithAggregate);
        }        
    }
}
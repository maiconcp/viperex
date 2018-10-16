using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Reflection;

namespace Viper.Common
{
    public abstract class AggregateRoot : Entity
    {
        public const long NewAggregateVersion = -1;
        private long _version = NewAggregateVersion;
        private readonly List<DomainEventBase> _domainEvents = new List<DomainEventBase>();
        public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents;
        protected AggregateRoot(Identity aggregateId) : base()
        {
            Id = aggregateId;
        }

        public AggregateRoot() : base()
        {

        }

        protected void ApplyEvent(DomainEventBase @event, long version)
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
            DomainEventBase eventWithAggregate = @event.WithAggregate(Id, _version);

            ApplyEvent(eventWithAggregate, _version + 1);
            _domainEvents.Add(eventWithAggregate);
        }

        public static T Reidratar<T>(List<DomainEventBase> events) where T : AggregateRoot
        {
            var type = typeof(T);

            var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                  null, new Type[] { typeof(Identity) }, null);

            if (constructor == null)
                throw new InvalidOperationException("The AggregateRoot have been not a constructor with Identity parameter.");

            var aggregate = (T)constructor.Invoke(new object[] { events.First().AggregateId });

            foreach (var @event in events)
            {
                aggregate.ApplyEvent(@event, @event.AggregateVersion);
            }

            return aggregate;
        }
    }
}
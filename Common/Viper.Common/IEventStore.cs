using System;
using System.Collections.Generic;
using System.Text;

namespace Viper.Common
{
    public interface IEventStore<T> where T : AggregateRoot
    {
        T Get(Identity id);
        void Store(T aggregateRoot);
    }
}
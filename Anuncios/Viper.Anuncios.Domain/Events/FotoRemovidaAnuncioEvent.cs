using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    public class FotoRemovidaAnuncioEvent : DomainEventBase
    {
        public Foto Foto { get; private set; }

        public FotoRemovidaAnuncioEvent(Guid aggregateID, Foto foto) : base(aggregateID)
        {
            Foto = foto;
        }

        private FotoRemovidaAnuncioEvent(Guid aggregateID, long aggregateVersion, Foto foto) : base(aggregateID, aggregateVersion)
        {
            Foto = foto;
        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new FotoRemovidaAnuncioEvent(aggregateId, aggregateVersion, Foto);
        }
    }
}

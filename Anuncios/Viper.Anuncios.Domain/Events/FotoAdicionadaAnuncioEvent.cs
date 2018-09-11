using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    public class FotoAdicionadaAnuncioEvent : DomainEventBase
    {
        public Foto Foto { get; private set; }

        public FotoAdicionadaAnuncioEvent(Guid aggregateID, Foto foto) : base(aggregateID)
        {
            Foto = foto;
        }

        private FotoAdicionadaAnuncioEvent(Guid aggregateID, long aggregateVersion, Foto foto) : base(aggregateID, aggregateVersion)
        {
            Foto = foto;
        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new FotoAdicionadaAnuncioEvent(aggregateId, aggregateVersion, Foto);
        }
    }
}

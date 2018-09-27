using System;
using System.Collections.Generic;
using System.Text;
using Viper.Celulares.Domain.Events;
using Viper.Common;

namespace Viper.Celulares.Domain.Entities
{
    public sealed partial class Acessorio : AggregateRoot
    {
        public void Apply(AcessorioCadastradoEvent @event)
        {
            Id = @event.AggregateId;
            Descricao = @event.Descricao;
        }
    }
}

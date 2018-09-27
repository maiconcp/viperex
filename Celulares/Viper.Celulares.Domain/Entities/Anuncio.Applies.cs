using System;
using System.Collections.Generic;
using System.Text;
using Viper.Celulares.Domain.Events;

namespace Viper.Celulares.Domain.Entities
{
    public sealed partial class Anuncio
    {
        public void Apply(AcessorioAdicionadoAoAnuncioEvent @event)
        {
            _acessorios.Add(@event.AcessorioId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Viper.Celulares.Domain.Events;

namespace Viper.Celulares.Domain.Entities
{
    public sealed partial class Anuncio
    {
        protected override void RegisterEventHandlers()
        {
            base.RegisterEventHandlers();

            Register<AcessorioAdicionadoAoAnuncioEvent>(Apply);
        }

        private void Apply(AcessorioAdicionadoAoAnuncioEvent @event)
        {
            _acessorios.Add(@event.AcessorioId);
        }
    }
}

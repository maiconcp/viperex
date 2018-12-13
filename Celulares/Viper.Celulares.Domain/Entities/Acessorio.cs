using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Viper.Celulares.Domain.Events;
using Viper.Common;

namespace Viper.Celulares.Domain.Entities
{
    public sealed partial class Acessorio : AggregateRoot
    {
        public string Descricao { get; private set; }
        public Acessorio(string descricao)
        {
            new Contract().Requires()
                          .IsNotNullOrWhiteSpace(descricao, nameof(Descricao),Messages.RequiredField("Descrição"))
                          .ThrowExceptionIfInvalid();            

            RaiseEvent(new AcessorioCadastradoEvent(Id, descricao));
        }
    }
}

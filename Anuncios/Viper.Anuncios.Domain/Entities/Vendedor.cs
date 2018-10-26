using System;
using Flunt.Validations;
using Viper.Common;

namespace Viper.Anuncios.Domain.Entities
{
    public class Vendedor : AggregateRoot
    {
        public Identity IdUsuario { get; private set; }
        public string Telefone {get; private set;}
        public string EnderecoCompleto {get; private set;}

        public Vendedor(Identity idUsuario, string telefone, string enderecoCompleto)
        {
            IdUsuario = idUsuario;
            Telefone = telefone;
            EnderecoCompleto = enderecoCompleto;

            new Contract().Requires()
                          .IsNotNull(idUsuario, nameof(IdUsuario), Messages.RequiredField("Usuário"))
                          .IsNotNullOrWhiteSpace(telefone, nameof(telefone), Messages.RequiredField("Telefone"))
                          .IsNotNullOrWhiteSpace(enderecoCompleto, nameof(EnderecoCompleto), Messages.RequiredField("Endereço Completo"))
                          .Check();
        }

        protected override void RegisterEventHandlers()
        {
            
        }
    }
}
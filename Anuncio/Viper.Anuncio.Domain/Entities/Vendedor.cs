using System;
using Flunt.Validations;
using Viper.Common;

namespace Viper.Anuncio.Domain.Entities
{
    public class Vendedor : Entity
    {
        public Guid IdUsuario { get; private set; }
        public string Telefone {get; private set;}
        public string EnderecoCompleto {get; private set;}

        public Vendedor(Guid idUsuario, string telefone, string enderecoCompleto)
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
    }
}
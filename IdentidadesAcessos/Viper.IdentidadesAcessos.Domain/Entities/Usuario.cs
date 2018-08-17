using Flunt.Validations;
using Viper.Common;
using Viper.IdentidadesAcessos.Domain.ValuesObjects;
using Viper.SharedKernel.ValuesObjects;

namespace Viper.IdentidadesAcessos.Domain.Entities
{
    public class Usuario : AggregateRoot
    {
        public NomeCompleto NomeCompleto { get; private set; }
        public Email Email { get; private set; }
        public Senha Senha { get; private set; }

        public Usuario(NomeCompleto nomeCompleto, Email email, Senha senha)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            Senha = senha;

            new Contract().Requires()
                          .IsNotNull(nomeCompleto, nameof(NomeCompleto), Messages.RequiredField("Nome Completo"))
                          .IsNotNull(email, nameof(Email), Messages.RequiredField("E-mail"))
                          .IsNotNull(senha, nameof(Senha), Messages.RequiredField("Senha"))
                          .Check();

            new Contract().Requires()
                         .IsNotNullOrWhiteSpace(email?.ToString(), nameof(Email), Messages.RequiredField("E-mail"))
                         .Check();                         
        }
    }
}
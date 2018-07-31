using Viper.Common;
using Viper.IdentidadeAcesso.Domain.ValuesObjects;
using Viper.SharedKernel.ValuesObjects;

namespace Viper.IdentidadeAcesso.Domain.Entities
{
    public class Usuario : Entity
    {
        public NomeCompleto NomeCompleto { get; set; }
        public Email Email { get; set; }
        public Senha Senha { get; set; }
    }
}
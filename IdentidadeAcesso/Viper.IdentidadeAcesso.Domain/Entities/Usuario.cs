using Viper.Common;

namespace Viper.IdentidadeAcesso.Domain.Entities
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public string SobreNome {get; private set;}

    }
}
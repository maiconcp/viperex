using Viper.Common;
using System.Collections.Generic;
using System.Linq;

namespace Viper.SharedKernel.ValuesObjects
{
    public class NomeCompleto : ValueObject<NomeCompleto>
    {
        public NomeCompleto(string nome, string sobrenome) 
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }
        
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome;
            yield return Sobrenome;
        }
    }
}
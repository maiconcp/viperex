using Viper.Common;
using System.Collections.Generic;
using System.Linq;
using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace Viper.SharedKernel.ValuesObjects
{
    public class NomeCompleto : ValueObject<NomeCompleto>
    {
        public NomeCompleto(string nome, string sobrenome) 
        {           
            new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(nome, nameof(Nome), "O 'Nome' é obrigatório.")
                .IsNotNullOrWhiteSpace(sobrenome, nameof(Sobrenome), "O 'Sobrenome' é obrigatório.")
                .IsFalse(TemCaracteresInvalidos(nome), nameof(nome), "O 'Nome' informado contém caracteres inválidos.")
                .IsFalse(TemCaracteresInvalidos(sobrenome), nameof(sobrenome), "O 'Sobrenome' informado contém caracteres inválidos.")
                .Check();
                
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

        public override string ToString()
        {
            return Nome.Trim() + " " + Sobrenome.Trim();
        }

        private bool TemCaracteresInvalidos(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto) &&
                   texto.ToArray().Any(c => !Char.IsLetter(c) && !Char.IsWhiteSpace(c));
        }
    }
}
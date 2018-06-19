using Viper.Common;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Viper.SharedKernel.ValuesObjects
{
    public class NomeCompleto : ValueObject<NomeCompleto>
    {
        public NomeCompleto(string nome, string sobrenome) 
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O 'Nome' é obrigatório.", nameof(nome));
            
            if (string.IsNullOrWhiteSpace(sobrenome))
                throw new ArgumentException("O 'Sobrenome' é obrigatório.", nameof(sobrenome));                

            if (TemCaracteresInvalidos(nome))
                throw new ArgumentException("O 'Nome' informado contém caracteres inválidos.", nameof(nome));
            
            if (TemCaracteresInvalidos(sobrenome))
                throw new ArgumentException("O 'Sobrenome' informado contém caracteres inválidos.", nameof(sobrenome));

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
            return texto.ToArray().Any(c => !Char.IsLetter(c) && !Char.IsWhiteSpace(c));
        }
    }
}
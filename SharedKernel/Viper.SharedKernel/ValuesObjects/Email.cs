using System.Collections.Generic;
using Flunt.Validations;
using Viper.Common;

namespace Viper.SharedKernel.ValuesObjects
{
    public class Email : ValueObject<Email>
    {
        public static readonly Email Empty = new Email("");
        public string EnderecoDeEmail { get; private set; }
        public bool IsEmpty { get => this == Empty; }
        public Email(string enderecoDeEmail)
        {
            new Contract().Requires()
                          .IsEmailOrEmpty(enderecoDeEmail, "E-mail", "E-mail inválido.")
                          .Check();

            EnderecoDeEmail = enderecoDeEmail;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EnderecoDeEmail;
        }

        public override string ToString()
        {
            return EnderecoDeEmail;
        }
    }
}
using System;
using System.Collections.Generic;
using Viper.Common;

namespace Viper.Anuncios.Domain.ValuesObjects
{
    public class Status : ValueObject<Status>
    {
        public string Descricao { get; private set; }

        public static Status Pendente => new Status(nameof(Pendente));
        public static Status Vendido => new Status(nameof(Vendido));
        public static Status Publicado => new Status(nameof(Publicado));
        public static Status Rejeitado => new Status(nameof(Rejeitado));

        protected Status(string descricao)
        {
            Descricao = descricao;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Descricao;
        }

        public bool EhPendente()
        {
            return Equals(Pendente);
        }

        public bool EhPublicado()
        {
            return Equals(Publicado);
        }

        public bool EhRejeitado()
        {
            return Equals(Rejeitado);
        }
        
        public bool EhVendido()
        {
            return Equals(Vendido);
        }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
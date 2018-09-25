using System;
using System.Collections.Generic;
using System.Linq;
using Viper.Common;

namespace Viper.Anuncios.Domain.ValuesObjects
{
    public class Status : Enumeration<Status>
    {       

        public static readonly Status Pendente = new Status(1, nameof(Pendente));
        public static readonly Status Vendido = new Status(2, nameof(Vendido));
        public static readonly Status Publicado = new Status(3, nameof(Publicado));
        public static readonly Status Rejeitado = new Status(4, nameof(Rejeitado));
        public static readonly Status Excluido = new Status(5, nameof(Excluido));
   

        private Status(int id, string descricao)
            : base(id, descricao)
        {
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

        public bool EhExcluido()
        {
            return Equals(Excluido);
        }
        
        public bool PodeSerExcluido()
        {
            return !EhExcluido() && !EhVendido();
        }

    }
}
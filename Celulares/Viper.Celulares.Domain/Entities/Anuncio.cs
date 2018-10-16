using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Viper.Anuncios.Domain.Events;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Celulares.Domain.Events;
using Viper.Common;

namespace Viper.Celulares.Domain.Entities
{
    public sealed partial class Anuncio : Anuncios.Domain.Entities.Anuncio
    {
        private readonly  List<Identity> _acessorios;
        public IReadOnlyCollection<Identity> Acessorios => _acessorios;

        public Anuncio(string titulo, string descricao, decimal preco, CondicaoUso condicaoUso, bool aceitoTroca) 
            : base(titulo, descricao, preco, condicaoUso, aceitoTroca)
        {
            _acessorios = new List<Identity>();
        }

        protected Anuncio(Identity aggregateId)
            : base(aggregateId)
        {

        }

        public void AdicionarAcessorio(Identity acessorioId)
        {
            new Contract().Requires()
                          .IsFalse(_acessorios.Contains(acessorioId), nameof(Acessorios), "Acessório já incluído.")
                          .IsTrue(Status.EhPendente(), nameof(Status), $"Acessório não pode ser incluído no status: { Status }")
                          .Check();

            RaiseEvent(new AcessorioAdicionadoAoAnuncioEvent(Id, acessorioId));
        }
    }
}

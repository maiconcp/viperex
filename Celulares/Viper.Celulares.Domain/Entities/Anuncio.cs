using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Celulares.Domain.Entities
{
    public class Anuncio : Anuncios.Domain.Entities.Anuncio
    {
        private readonly  List<Identity> _acessorios;
        public IReadOnlyCollection<Identity> Acessorios => _acessorios;

        public Anuncio(string titulo, string descricao, decimal preco, CondicaoUso condicaoUso, bool aceitoTroca) 
            : base(titulo, descricao, preco, condicaoUso, aceitoTroca)
        {
            _acessorios = new List<Identity>();
        }

        public void AdicionarAcessorio(Identity id)
        {
            new Contract().Requires()
                          .IsFalse(_acessorios.Contains(id), nameof(Acessorios), "Acessório já incluído.")
                          .IsTrue(Status.EhPendente(), nameof(Status), $"Acessório não pode ser incluído no status: { Status }")
                          .Check();

            _acessorios.Add(id);
        }
    }
}

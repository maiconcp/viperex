using System;
using Flunt.Validations;
using Viper.Anuncios.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.Anuncios.Domain.Events
{
    public class AnuncioCadastradoEvent : DomainEventBase
    {
        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public decimal Preco { get; private set; }

        public CondicaoUso CondicaoUso { get; private set; }

        public AnuncioCadastradoEvent(Guid aggregateID, string titulo, string descricao, decimal preco, CondicaoUso condicaoUso) : base(aggregateID)
        {
            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
            CondicaoUso = condicaoUso;
        }

        private AnuncioCadastradoEvent(Guid aggregateID, long aggregateVersion, string titulo, string descricao, decimal preco, CondicaoUso condicaoUso) : base(aggregateID, aggregateVersion)
        {
            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
            CondicaoUso = condicaoUso;
        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new AnuncioCadastradoEvent(aggregateId, aggregateVersion, Titulo, Descricao, Preco, CondicaoUso);
        }
    }
}
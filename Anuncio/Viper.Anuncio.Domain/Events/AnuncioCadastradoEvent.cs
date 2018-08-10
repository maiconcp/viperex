using System;
using Flunt.Validations;
using Viper.Common;

namespace Viper.Anuncio.Domain.Events
{
    public class AnuncioCadastradoEvent : DomainEventBase
    {
        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public decimal Preco { get; private set; }

        public AnuncioCadastradoEvent(Guid aggregateID, string titulo, string descricao, decimal preco) : base(aggregateID)
        {
            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
        }

        private AnuncioCadastradoEvent(Guid aggregateID, long aggregateVersion, string titulo, string descricao, decimal preco) : base(aggregateID, aggregateVersion)
        {
            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
        }

        public override DomainEventBase WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new AnuncioCadastradoEvent(aggregateId, aggregateVersion, Titulo, Descricao, Preco);
        }
    }
}
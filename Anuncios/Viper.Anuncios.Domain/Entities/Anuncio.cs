using System;
using Flunt.Validations;
using Viper.Anuncios.Domain.Events;
using Viper.Common;
using System.Collections.Generic;
using Viper.Anuncios.Domain.ValuesObjects;

namespace Viper.Anuncios.Domain.Entities
{
    public sealed partial class Anuncio : AggregateRoot
    {
        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public decimal Preco { get; private set; }

        public DateTime DataDaVenda { get; private set; }

        public Status Status { get; private set; }

        public Anuncio(string titulo, string descricao, decimal preco)
        {
            new Contract().Requires()
                        .HasMaxLen(titulo, 100, nameof(titulo), "Título pode ter até cem caracteres")
                        .IsNotNullOrWhiteSpace(titulo, nameof(titulo), Messages.RequiredField(titulo))
                        .IsNotNullOrWhiteSpace(descricao, nameof(descricao), Messages.RequiredField(descricao))
                        .IsGreaterThan(preco, 0, nameof(Preco), "O Preço deve ser maior do que zero.")
                        .Check();

            RaiseEvent(new AnuncioCadastradoEvent(Id, titulo, descricao, preco));
        }

        public void Vender()
        {
            new Contract().Requires()
                          .IsTrue(Status.EhPublicado(), nameof(Status), "Anúncio não está mais disponível.")
                          .Check();

            RaiseEvent(new AnuncioVendidoEvent(Id, DateTime.Now));            
        }

        public void Publicar()
        {
            new Contract().Requires()
                          .IsTrue(Status.EhPendente(), nameof(Status), $"O anúncio deve estar {Status.Pendente}.")
                          .Check();
            
            RaiseEvent(new AnuncioPublicadoEvent(Id));
        }

        public void Rejeitar()
        {
            new Contract().Requires()
                          .IsTrue(Status.EhPendente(), nameof(Status), $"O anúncio deve estar {Status.Pendente}.")
                          .Check();
            
            RaiseEvent(new AnuncioRejeitadoEvent(Id));
        }

        public void Excluir()
        {
            new Contract().Requires()
                          .IsTrue(Status.PodeSerExcluido(), nameof(Status), $"O anúncio não pode estar {Status}.")
                          .Check();

            Status = Status.Excluido;
        }
    }
}
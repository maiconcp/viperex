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

        private List<Visualizacao> _visualizacoes = new List<Visualizacao>();
        
        public IReadOnlyList<Visualizacao> Visualizacoes => _visualizacoes; 

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

        public void MarcarComoVendido()
        {
            new Contract().Requires()
                          .IsTrue(Status.EhPublicado(), nameof(Status), "Anúncio não está mais disponível.")
                          .Check();

            RaiseEvent(new AnuncioVendidoEvent(Id, DateTime.Now));            
        }

        public void Visualizar()
        {
            var visualizacao = new Visualizacao(DateTime.Now);
            RaiseEvent(new AnuncioVisualizadoEvent(Id, visualizacao));
        }

        public void Publicar()
        {
            new Contract().Requires()
                          .IsTrue(Status.EhPendente(), nameof(Status), $"O anúncio deve estar {Status.Pendente}.")
                          .Check();
            
            RaiseEvent(new AnuncioPublicadoEvent(Id));
        }
    }
}
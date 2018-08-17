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
        public void Apply(AnuncioCadastradoEvent @event)
        {
            Titulo = @event.Titulo;
            Descricao = @event.Descricao;
            Preco = @event.Preco;
            Status = Status.Pendente;
        }        

        public void Apply(AnuncioVendidoEvent @event)
        {
            DataDaVenda = @event.DataDaVenda;
            Status = Status.Vendido;
        }

        public void Apply(AnuncioVisualizadoEvent @event)
        {
            _visualizacoes.Add(@event.Visualizacao);
        }

        public void Apply(AnuncioPublicadoEvent @event)
        {
            Status = Status.Publicado;
        }
    }
}
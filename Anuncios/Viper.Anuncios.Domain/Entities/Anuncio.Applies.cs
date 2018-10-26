using System;
using Flunt.Validations;
using Viper.Anuncios.Domain.Events;
using Viper.Common;
using System.Collections.Generic;
using Viper.Anuncios.Domain.ValuesObjects;

namespace Viper.Anuncios.Domain.Entities
{
    public partial class Anuncio : AggregateRoot
    {
        protected override void RegisterEventHandlers()
        {
            Register<AnuncioCadastradoEvent>(Apply);
            Register<AnuncioVendidoEvent>(Apply);
            Register<AnuncioPublicadoEvent>(Apply);
            Register<AnuncioRejeitadoEvent>(Apply);
            Register<FotoAdicionadaAnuncioEvent>(Apply);
            Register<FotoRemovidaAnuncioEvent>(Apply);
            Register<TodasFotosRemovidasAnuncioEvent>(Apply);
        }

        private void Apply(AnuncioCadastradoEvent @event)
        {
            Titulo = @event.Titulo;
            Descricao = @event.Descricao;
            Preco = @event.Preco;
            Status = Status.Pendente;
            CondicaoUso = @event.CondicaoUso;
            Fotos = new AlbumFotos();
            AceitoTroca = @event.AceitoTroca;
        }

        private void Apply(AnuncioVendidoEvent @event)
        {
            DataDaVenda = @event.DataDaVenda;
            Status = Status.Vendido;
        }

        private void Apply(AnuncioPublicadoEvent @event)
        {
            Status = Status.Publicado;
        }

        private void Apply(AnuncioRejeitadoEvent @event)
        {
            Status = Status.Rejeitado;
        }

        private void Apply(FotoAdicionadaAnuncioEvent @event)
        {
            Fotos = Fotos.Adicionar(@event.Foto);
        }

        private void Apply(FotoRemovidaAnuncioEvent @event)
        {
            Fotos = Fotos.Remover(@event.Foto);
        }

        private void Apply(TodasFotosRemovidasAnuncioEvent @event)
        {
            Fotos = Fotos.Limpar();
        }
    }
}
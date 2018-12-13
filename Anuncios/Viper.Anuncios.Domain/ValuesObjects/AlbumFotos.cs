using Flunt.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Viper.Common;

namespace Viper.Anuncios.Domain.ValuesObjects
{

    public class AlbumFotos : ValueObject<AlbumFotos>, IEnumerable<Foto>
    {
        public const int QUANTIDADE_MAXIMA_FOTOS = 6;

        private List<Foto> Fotos;

        public Foto Capa => Fotos.FirstOrDefault();

        public bool Completo => Fotos.Count == QUANTIDADE_MAXIMA_FOTOS;

        public bool Vazio => !Fotos.Any();

        public AlbumFotos()
        {
            Fotos = new List<Foto>();
        }

        protected AlbumFotos(List<Foto> fotos)
        {
            Fotos = fotos;
        }

        internal AlbumFotos Adicionar(Foto foto)
        {
            new Contract().Requires()
                          .IsFalse(Completo, nameof(Fotos), "O Álbum já está completo.")
                          .ThrowExceptionIfInvalid();

            var fotos = Fotos.ToList();
            fotos.Add(foto);

            return new AlbumFotos(fotos);
        }

        internal AlbumFotos Remover(Foto foto)
        {
            new Contract().Requires()
                          .IsFalse(Vazio, nameof(Fotos), "O Álbum está vazio.")
                          .ThrowExceptionIfInvalid();

            var fotos = Fotos.ToList();

            new Contract().Requires()
                          .IsTrue(fotos.Remove(foto), nameof(Fotos), "A foto não pertence ao álbum.")
                          .ThrowExceptionIfInvalid();

            return new AlbumFotos(fotos);
        }

        internal AlbumFotos Limpar()
        {
            return new AlbumFotos();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Capa;
            yield return Fotos;
        }

        public IEnumerator<Foto> GetEnumerator()
        {
            return Fotos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Fotos.GetEnumerator();
        }
    }
}

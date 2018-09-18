using System;
using System.Collections.Generic;
using System.Text;
using Viper.Celulares.Domain.Entities;
using Viper.Common;
using Xunit;

namespace Viper.Celulares.Domain.Tests
{
    public class AcessorioTest
    {
        [Theory]
        [InlineData("Capa")]
        [InlineData("Fone de Ouvido")]
        public void Construtor_DadosValidos_ObjetoCriado(string descricao)
        {
            // Arrange
            // Act
            var acessorio = new Acessorio(descricao);

            // Assert
            Assert.Equal(descricao, acessorio.Descricao);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void Construtor_DescricaoInvalida_DomainException(string descricaoInvalida)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Acessorio(descricaoInvalida));
        }
    }
}

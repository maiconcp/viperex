using System;
using Xunit;
using Viper.Anuncios.Domain.Entities;
using Viper.Common;

namespace Viper.Anuncios.Domain.Tests
{
    public class VendedorTest
    {
        [Fact]
        public void Construtor_DadosValido_ObjetoCriado()
        {
            // Arrange
            // Act
            var vendedor = new Vendedor(Identity.CreateNew(), "4799917774", "Rua Dublin");
            
            // Assert
            Assert.NotNull(vendedor);
        }

        [Theory]
        [InlineData("", "Rua Dublin")]
        [InlineData("4799991111", "")]
        public void Construtor_DadosInvalido_DomainException(string numeroTelefone, string endereco)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Vendedor(Identity.CreateNew(), numeroTelefone, endereco));
        }        
    }
}

using Flunt.Validations;
using Viper.Common;
using Xunit;

namespace Viper.Common.Tests
{
    public class ContractExtensionTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void IsNotNullOrWhiteSpace_NullOrWhitspace_UmaNotificacao(string texto)
        {
            // Arrange
            var contract = new Contract();

            // Act
            contract.IsNotNullOrWhiteSpace(texto, "Propriedade Qualquer", "Mensagem Qualquer");
            
            // Assert
            Assert.Equal(1, contract.Notifications.Count);
        }
        [Theory]
        [InlineData("Texto")]
        [InlineData(" Texto ")]
        public void IsNotNullOrWhiteSpace_TextoValido_SemNotificacao(string texto)
        {
            // Arrange
            var contract = new Contract();

            // Act
            contract.IsNotNullOrWhiteSpace(texto, "Propriedade Qualquer", "Mensagem Qualquer");
            
            // Assert
            Assert.Equal(0, contract.Notifications.Count);           
        }    
        [Fact]
        public void Check_ContratoValido_SemExcecao()
        {
            // Arrange
            var contract = new Contract();

            // Act            
            // Assert
            contract.ThrowExceptionIfInvalid();
        }

        [Fact]
        public void Check_ContratoInvalido_RetornaUmaExcecao()
        {
            // Arrange
            var contract = new Contract();
            contract.AddNotification("Prop Qualquer", "Notificacao Qualquer");

            // Act
            var exception = Assert.Throws<DomainException>(() =>
            {
                contract.ThrowExceptionIfInvalid();
            });

            // Assert
            Assert.True(exception.Message.Contains("Notificacao Qualquer"));
        }

        [Fact]
        public void IsGuid_StringEhGuid_SemNotificacao()
        {
            // Arrange
            var contract = new Contract();

            // Act
            contract.IsGuid("6F54CD75-399F-4975-B9CB-9A7D5A631C2A", "Propriedade Qualquer", "Mensagem Qualquer");

            // Assert
            Assert.Equal(0, contract.Notifications.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("6F54CD75")]
        [InlineData("6F54CD75-399F")]
        [InlineData("6F54CD75-399F-4975-B9CB-9A7D5A631C2AA")]
        public void IsGuid_StringNaoEhGuid_UmaNotificacao(string texto)
        {
            // Arrange
            var contract = new Contract();

            // Act
            contract.IsGuid(texto, "Propriedade Qualquer", "Mensagem Qualquer");

            // Assert
            Assert.Equal(1, contract.Notifications.Count);
        }
    }
}
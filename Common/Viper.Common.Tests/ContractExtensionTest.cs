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
            contract.Check();
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
                contract.Check();
            });

            // Assert
            Assert.True(exception.Message.Contains("Notificacao Qualquer"));
        }
    }
}
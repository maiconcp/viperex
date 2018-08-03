using System;
using System.Linq;
using Xunit;
using Viper.Common;
using Flunt.Validations;

namespace Viper.Common.Tests
{
    public class MessagesTests
    {
        [Fact]
        public void Deve_Fornecer_Mensagens()
        {
            // Arrange
            // Act
            var notificacoes = new Contract().Requires()
                                             .IsFalse(true, "Nome", Messages.RequiredField("Nome"));

            // Assert
            Assert.Equal("O campo Nome é obrigatório.", notificacoes.Notifications.First().Message);
        }
    }
}

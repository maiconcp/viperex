using System;
using Xunit;
using Viper.IdentidadeAcesso.Domain.ValuesObjects;
using Viper.Common;

namespace Viper.IdentidadeAcesso.Tests
{
    public class SenhaTest
    {
        [Theory]
        [InlineData ("123456")]
        [InlineData ("12345678")]
        [InlineData ("123456ABCD")]
        [InlineData ("ABCDEF")]
        public void Construtor_SenhaComMinimoDeSeisCaracteres_ObjetoSenhaCriado(string conteudoSenha)
        {
            // Arrange
            // Act 
            // Assert  
            var senha = new Senha(conteudoSenha);
        }

        [Theory]
        [InlineData ("")]
        [InlineData ("1")]
        [InlineData ("12345")]
        [InlineData ("ABCDE")]
        public void Construtor_SenhaInvalida_DomainException(string conteudoSenha)
        {
            // Arrange
            // Act 
            // Assert  
            Assert.Throws<DomainException>(() => new Senha(conteudoSenha));
        }
    }
}
using Viper.Common;
using Viper.SharedKernel.ValuesObjects;
using Xunit;

namespace Viper.SharedKernel.Tests
{
    public class EmailTest
    {
        [Theory]
        [InlineData ("leandroavieira@gmail.com")]
        [InlineData ("maicon.CEO@viperex.com.br")]
        public void Construtor_EmailValido_ObjetoEmailCriado(string enderecoDeEmail)
        {
            // Arrange
            // Act
            var email = new Email(enderecoDeEmail);

            // Assert
            Assert.Equal(enderecoDeEmail, email.EnderecoDeEmail);
        }

        [Theory]
        [InlineData ("leandro@")]
        [InlineData ("maicon.cpereira")]
        [InlineData ("jose@gmail")]        
        public void Construtor_EmailInvalido_DomainException(string enderecoDeEmail)
        {
            // Arrange
            // Act          
            // Assert
            Assert.Throws<DomainException>(() => new Email(enderecoDeEmail));
        }

        [Fact]
        public void Construtor_StringVazio_ObjetoEmailCriado()
        {
            // Arrange
            // Act
            var email = new Email(string.Empty);

            // Assert
            Assert.Equal(string.Empty, email.EnderecoDeEmail);
        }

        [Fact]
        public void Empty_Uso_EnderecoDeEmailVazio()
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(string.Empty, Email.Empty.EnderecoDeEmail);
        }

        [Fact]
        public void Deve_Retornar_Endereco_De_Email_No_ToString()
        {
            // Arrange
            string enderecoDeEmail = "maicon.pereira@viperex.com.br";
            var email = new Email(enderecoDeEmail);
            
            // Act
            string saidaToString = email.ToString();

            // Assert
            Assert.Equal(enderecoDeEmail, saidaToString);  
        }

        [Fact]
        public void IsEmpty_EmailVazio_Verdadeiro()
        {
            // Arrange                                   
            // Act            
            // Assert            
            Assert.True(Email.Empty.IsEmpty);
        }

        
        [Fact]
        public void IsEmpty_EmailValido_Falso()
        {
            // Arrange                                   
            // Act            
            // Assert           
            Assert.False(new Email("leandro@viperex.com.br").IsEmpty);
        }
    }
}
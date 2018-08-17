using Viper.Common;
using Viper.IdentidadesAcessos.Domain.Entities;
using Viper.IdentidadesAcessos.Domain.ValuesObjects;
using Viper.SharedKernel.ValuesObjects;
using Xunit;

namespace Viper.IdentidadesAcessos.Tests
{
    public class UsuarioTest
    {
        [Theory]
        [InlineData ("Leandro", "Alberto Vieira", "leandro@viperex.com.br", "123456")]
        [InlineData ("Maicon Carlos", "Pereira", "maicon@viperex.com.br", "123aed6")]
        public void Construtor_DadosValidos_ObjetoCriado(string nome, string sobrenome, string enderecoEmail, string conteudoSenha)
        {
            // Arrange
            var nomeCompleto = new NomeCompleto(nome, sobrenome);
            var email = new Email(enderecoEmail);
            var senha = new Senha(conteudoSenha);            

            // Act
            var usuario = new Usuario(nomeCompleto, email, senha);

            // Assert
            Assert.NotNull(usuario);
        }
        [Theory]
        [InlineData ("", "", "leandro@viperex.com.br", "123456")]
        [InlineData ("Leandro", "Alberto Vieira", "", "123456")]
        [InlineData ("Leandro", "Alberto Vieira", "leandro@viperex.com.br", "")]        

        public void Construtor_DadosInvalidos_DomainException(string nome, string sobrenome, string enderecoEmail, string conteudoSenha)
        {
            // Arrange
            var nomeCompleto = string.IsNullOrWhiteSpace(nome) ? null : new NomeCompleto(nome, sobrenome);
            var email = string.IsNullOrEmpty(enderecoEmail) ? null : new Email(enderecoEmail);
            var senha = string.IsNullOrEmpty(conteudoSenha) ? null : new Senha(conteudoSenha);            

            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Usuario(nomeCompleto, email, senha));
        }        
    }
}
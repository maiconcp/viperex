using System;
using Xunit;
using Viper.SharedKernel.ValuesObjects;

namespace Viper.SharedKernel.Tests
{
    public class NomeCompletoTest
    {
        [Theory]
        [InlineData("Maicon", "Pereira", "Maicon Pereira")]
        [InlineData("Leandro", "Vieira", "Leandro Vieira")]
        [InlineData("Zé A", "Vieira", "Zé A Vieira")]
        [InlineData("A", "B", "A B")]
        public void Construtor_NomeESobrenomeValido_ObjetoNomeCompleto(string nome, string sobrenome, string resultadoEsperado)
        {
            // Arrange
            // Act
            var nomeCompleto = new NomeCompleto(nome, sobrenome);

            // Assert
            Assert.Equal(resultadoEsperado, nomeCompleto.ToString());
        }

        [Theory]
        [InlineData (" Maicon", " Pereira", "Maicon Pereira")]
        [InlineData ("Leandro ", "Vieira ", "Leandro Vieira")]
        [InlineData (" Zé ", " A ", "Zé A")]
        [InlineData ("   A", "B   ", "A B")]
        public void Construtor_NomeESobrenomeComMaisDeUmEspaco_ObjetoNomeCompletoComApenasUmEspaco(string nome, string sobrenome, string resultadoEsperado)
        {
            // Arrange
            // Act            
            var nomeCompleto = new NomeCompleto(nome, sobrenome);

            // Assert
            Assert.Equal(resultadoEsperado, nomeCompleto.ToString());
        }

        [Theory]
        [InlineData ("A", " ")]
        [InlineData (" ", "A")]
        [InlineData (" ", " ")]
        [InlineData ("A", "")]
        [InlineData ("", "A")]        
        [InlineData ("", "")]
        [InlineData ("A", null)]
        [InlineData (null, "A")]
        [InlineData (null, null)]
        public void Construtor_NomeOuSobrenomeVazioOuApenasEspacoOuNulo_ArgumentException(string nome, string sobrenome)
        {
            // Arrange
            // Act  
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                new NomeCompleto(nome, sobrenome);
            });

            // Assert
            Assert.True(exception.Message.Contains("obrigatório"));
        }

        [Theory]
        [InlineData ("12345", "67890")]
        [InlineData ("!@#$%¨&*", "_+{}[]")]
        public void Construtor_NomeOuSobrenomeNaoPodeConterNumerosOuCaracteresEspeciais_ArgumentException(string nome, string sobrenome)
        {
            // Arrange
            // Act              
            var exception = Assert.Throws<ArgumentException>(() => 
            {
                new NomeCompleto(nome, sobrenome);
            });

            // Assert
            Assert.True(exception.Message.Contains("caracteres"));            
        }
    }
}

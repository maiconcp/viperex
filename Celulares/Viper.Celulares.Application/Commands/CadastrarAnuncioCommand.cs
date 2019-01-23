using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Viper.Common;

namespace Viper.Celulares.Application.Commands
{
    public class CadastrarAnuncioCommand : Command
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string CondicaoUso  { get; set; }
        public bool AceitoTroca { get; set; }
        
        public override void Validate()
        {
            AddNotifications(
                new Contract().Requires()
                              .IsNotNullOrWhiteSpace(Titulo, nameof(Titulo), Messages.RequiredField(Titulo))
                              .IsNotNullOrWhiteSpace(Descricao, nameof(Descricao), Messages.RequiredField(Descricao))
                              .IsGreaterThan(Preco, 0, nameof(Preco), "O Preço deve ser maior do que zero.")
                              .IsNotNull(CondicaoUso, nameof(CondicaoUso), Messages.RequiredField("Condição de Uso")));
        }
    }
}

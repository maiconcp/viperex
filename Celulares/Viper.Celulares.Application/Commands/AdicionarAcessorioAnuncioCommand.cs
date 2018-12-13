using Flunt.Notifications;
using Flunt.Validations;
using Viper.Common;

namespace Viper.Celulares.Application.Commands
{
    public class AdicionarAcessorioAnuncioCommand : Command
    {
        public string AnuncioId { get; set; }

        public string AcessorioId { get; set; }

        public override void Validate()
        {
            AddNotifications
            (
                new Contract().Requires()
                              .IsGuid(AnuncioId, nameof(AnuncioId), $"O {nameof(AnuncioId)} é inválido.")
                              .IsGuid(AcessorioId, nameof(AcessorioId), $"O {nameof(AcessorioId)} é inválido.")
            );
        }
    }
}

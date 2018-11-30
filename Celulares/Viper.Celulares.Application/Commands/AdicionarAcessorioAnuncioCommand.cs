using Flunt.Notifications;
using Flunt.Validations;
using Viper.Common;

namespace Viper.Celulares.Application.Commands
{
    public class AdicionarAcessorioAnuncioCommand : Notifiable, ICommand
    {
        public AdicionarAcessorioAnuncioCommand(string anuncioId, string acessorioId)
        {
            AnuncioId = anuncioId;
            AcessorioId = acessorioId;

            AddNotifications(new Contract().Requires()
                                          .IsGuid(anuncioId, nameof(AnuncioId), $"O {nameof(AnuncioId)} é inválido.")
                                          .IsGuid(acessorioId, nameof(AcessorioId), $"O {nameof(AcessorioId)} é inválido."));                     
        }

        public string AnuncioId { get; private set; }

        public string AcessorioId { get; private set; }

        Decidir se será Always Valid ou Not Aways Valid
        // Balta é Not always
        // Pires é Always
    }
}

using System;
using Flunt;
using Flunt.Validations;
using Viper.Common;

namespace Flunt.Validations
{
    public static class ContractExtension
    {
        public static Contract IsNotNullOrWhiteSpace(this Contract contract, string val, string property, string message)
        {
            if (string.IsNullOrWhiteSpace(val))
                contract.AddNotification(property, message);

            return contract;
        }

        /// <summary>
        /// Verifica se o contrato possue notificações, caso sim, lança uma DomainException.       
        /// </summary>
        public static void Check(this Contract contract)
        {
            if (contract.Invalid)
                throw new DomainException(contract.Notifications);
        }
    }
}
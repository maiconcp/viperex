using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viper.Common
{
    public abstract class Command : Notifiable, IValidatable
    {
        public abstract void Validate();
    }
}

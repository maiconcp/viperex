using System;
using System.Collections.Generic;
using System.Text;

namespace Viper.Common
{
    public interface ICommandBus
    {
        TCommandResult Send<TCommand,TCommandResult>(TCommand command) where TCommand : Command;
    }
}

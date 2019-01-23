using System;
using System.Collections.Generic;
using System.Text;

namespace Viper.Common
{
    public interface ICommandHandler<CommandType, ResultType> where CommandType : Command
    {
        ResultType Handle(CommandType command);
    }
}

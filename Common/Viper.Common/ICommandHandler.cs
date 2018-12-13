using System;
using System.Collections.Generic;
using System.Text;

namespace Viper.Common
{
    public interface ICommandHandler<T> where T : Command
    {
        void Handle(T command);
    }
}

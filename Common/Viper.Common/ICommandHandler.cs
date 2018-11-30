using System;
using System.Collections.Generic;
using System.Text;

namespace Viper.Common
{
    public interface ICommandHandler<T> where T : ICommand
    {
        void Handle(T command);
    }
}

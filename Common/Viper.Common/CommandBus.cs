using System;
using System.Collections.Generic;
using System.Text;
using Flunt.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Viper.Common
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider ServiceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public TCommandResult Send<TCommand, TCommandResult>(TCommand command) where TCommand : Command
        {
            command.Validate();
            command.ThrowExceptionIfInvalid();

            var handler = ServiceProvider.GetService<ICommandHandler<TCommand, TCommandResult>>();

            return handler.Handle(command);
        }
    }
}

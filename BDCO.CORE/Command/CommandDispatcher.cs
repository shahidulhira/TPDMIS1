using Autofac;
using BDCO.Core.Validation;
using System;

namespace BDCO.Core.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext context;

        public CommandDispatcher(IComponentContext context)
        {
            this.context = context;
        }

        public CommandResult Dispatch<TParameter>(TParameter command) where TParameter : class, ICommand
        {
            ICommandHandler<TParameter> commandHandler = null;
            IValidationHandler<TParameter> vlidationHandler = null;

            commandHandler = this.context.Resolve<ICommandHandler<TParameter>>();

            try
            {
                vlidationHandler = this.context.Resolve<IValidationHandler<TParameter>>();
            }
            catch (Exception) { }

            if (vlidationHandler != null)
            {
                ValidationResult validationResult = vlidationHandler.Validate(command);
                if (!string.IsNullOrEmpty(validationResult.Message))
                    return new CommandResult { Success = false, Message = validationResult.Message };
            }

            return commandHandler.Execute(command);
        }
    }
}

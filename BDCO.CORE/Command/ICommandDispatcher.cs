namespace BDCO.Core.Command
{
    public interface ICommandDispatcher
    {
        CommandResult Dispatch<TParameter>(TParameter command) where TParameter : class, ICommand;
    }
}

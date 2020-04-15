namespace BDCO.Core.Command
{
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        CommandResult Execute(TParameter command);
    }
}

namespace Vulcan.Exports.Commands
{
    public interface ICommand
    {
        ICommandBehavior Behavior { get; }
        ICommandIdentity Identity { get; }
    }
}
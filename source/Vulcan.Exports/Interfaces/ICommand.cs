using Vulcan.Exports.Commands;

namespace Vulcan.Exports.Interfaces
{
    public interface ICommand
    {
        ICommandBehavior Behavior { get; }
        ICommandIdentity Identity { get; }
    }
}
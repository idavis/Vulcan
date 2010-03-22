#region Using Directives

using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Exports
{
    public class CommandViewModel<T> where T : ICommand
    {
    }

    public class CommandView<T> where T : ICommand
    {
    }
}
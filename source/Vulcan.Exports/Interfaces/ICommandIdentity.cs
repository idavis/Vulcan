namespace Vulcan.Exports.Commands
{
    public interface ICommandIdentity
    {
        string Name { get; }
        string Description { get; }
        string Comment { get; }
    }
}
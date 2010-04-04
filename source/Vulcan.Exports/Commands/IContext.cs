namespace Vulcan.Exports.Commands
{
    public interface IContext
    {
        T Resolve<T>( T directory );
    }
}
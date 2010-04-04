namespace Vulcan.Exports.Interfaces
{
    public interface IContext
    {
        T Resolve<T>( T value );
    }
}
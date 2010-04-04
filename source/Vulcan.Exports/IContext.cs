public interface IContext
{
    T Resolve<T>( T value );
}
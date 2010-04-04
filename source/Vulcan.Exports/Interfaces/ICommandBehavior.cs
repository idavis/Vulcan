namespace Vulcan.Exports.Interfaces
{
    public interface ICommandBehavior
    {
        bool Enabled { get; }
        bool IgnoreFailue { get; }
        int NumberOfRetries { get; }
        int DelayBetweenRetries { get; }
        bool SupportsCancelation { get; }
    }
}
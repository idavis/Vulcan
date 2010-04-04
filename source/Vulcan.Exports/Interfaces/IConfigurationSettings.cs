namespace Vulcan.Exports.Interfaces
{
    public interface IConfigurationSettings
    {
        string Environment { get; }
        string ConnectionString { get; }
        string ConnectionStringName { get; }
        string OutputDirectory { get; }
        string OutputFileName { get; }
        string EmailRecipients { get; }
        string EmailFromAddress { get; }
        string EmailSmtpHost { get; }
        string EmailSubject { get; }
        int EmailSmtpPort { get; }
        bool UseDefaultCredentialsForEmail { get; }
        string EmailSmtpUserName { get; }
        string EmailSmtpPassword { get; }

        T Get<T>( string key, T defaultValue );

        void Set( string key, object value );
    }
}
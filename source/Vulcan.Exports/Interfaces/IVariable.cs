namespace Vulcan.Exports
{
    public interface IVariable<T>
    {
        string Name { get; set; }
        T Value { get; set; }

        /// <summary>
        /// Determines whether the data returned in <see cref="Value"/> has its 
        /// internal variables expanded prior to being returned.
        /// </summary>
        bool Expand { get; set; }

        /// <summary>
        /// Determines whether command line variables override the default value.
        /// </summary>
        bool Override { get; set; }

        /// <summary>
        /// The initial value of the variable.
        /// </summary>
        T DefaultValue { get; set; }
    }
}
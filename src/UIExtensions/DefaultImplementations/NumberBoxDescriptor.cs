namespace UIFramework.UiExtensions;

///<inheritdoc cref="INumberBoxDescriptor"/>
public class NumberBoxDescriptor : DefaultValidator, INumberBoxDescriptor
{
    /// <inheritdoc/>
    public float Steps { get; set; } = 0;
    /// <inheritdoc/>
    public int DecimalPlaces { get; set; } = 1;

}
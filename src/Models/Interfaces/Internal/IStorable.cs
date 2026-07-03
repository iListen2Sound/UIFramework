namespace UIFramework.Models;
/// <summary>
/// implemented by models that store data
/// </summary>
public interface IStorable
{
    public object BoxedValue { get; set; }
    public bool TryApply(object value)
    {
        bool result = false;
        try
        {
            BoxedValue = value;
            result = true;
        }
        catch (Exception ex)
        {
            Debug.Log($"MelonEntryModel TryApply: {ex.Message}", false, 2);
            result = false;

        }
        return result;

    }
}
namespace UIFramework.UiExtensions;


public interface IButtonDescriptor : IUiExtension
{
    public string ButtonText { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public Action Handler { get; set; }
}
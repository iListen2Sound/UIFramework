using Il2CppTMPro;

namespace UIFramework.UiExtensions;

/// <summary>
/// Describes properties of text inputs that define how it looks
/// </summary>
public interface ITextInputAppearanceDescriptor : IUiExtension
{
    public int FontSize { get; set; }
    /// <summary>
    /// Set to true to have the text auto size between AutoSizeMin and AutoSizeMax. If false, FontSize will be used as the font size.
    /// </summary>
    public bool IsAutoSizing { get; set; }
    public int AutoSizeMin { get; set; }
    public int AutoSizeMax { get; set; }

    public FontStyles FontStyle { get; set; }
    public bool IsRichText { get; set; }
}
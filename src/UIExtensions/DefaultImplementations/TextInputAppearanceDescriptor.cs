using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UIExtensions;

/// <inheritdoc cref="ITextInputAppearanceDescriptor"/>
public class TextInputAppearanceDescriptor : DefaultValidator, ITextInputAppearanceDescriptor
{
    public int FontSize { get; set; } = 18;
    public bool IsAutoSizing { get; set; } = false;
    public int AutoSizeMin { get; set; } = 14;
    public int AutoSizeMax { get; set; } = 30;
    public FontStyles FontStyle { get; set; } = FontStyles.Normal;
    public bool IsRichText { get; set; } = true;
}
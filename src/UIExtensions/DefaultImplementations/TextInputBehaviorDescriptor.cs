using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UiExtensions;

/// <inheritdoc cref="ITextInputBehaviorDescriptor"/>
public class TextInputBehaviorDescriptor : DefaultValidator, ITextInputBehaviorDescriptor
{
    public TMP_InputField.ContentType ContentType { get; set; } = TMP_InputField.ContentType.Standard;
    /// <inheritdoc/>
    public char PasswordChar { get; set; } = '•';
    /// <inheritdoc/>
    public int CharacterLimit { get; set; } = 0;
    /// <inheritdoc/>
    public bool IsReadOnly { get; set; } = false;
}
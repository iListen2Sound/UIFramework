using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UiExtensions;

/// <summary>
/// Describes properties for text inputs that define its behavior.
/// </summary>
public interface ITextInputBehaviorDescriptor : IUiExtension
{
    /// <summary>
    /// What type of content is going into the textinput
    /// Maps to TMP_InputField.contentType
    /// </summary>
    /// <remarks>Implemented</remarks>
    public TMP_InputField.ContentType ContentType { get; set; }
    /// <summary>
    /// Character that shows instead on password fields
    /// Maps to TMP_InputField.asteriskChar
    /// </summary>
    public char PasswordChar { get; set; }
    /// <summary>
    /// Number of characters to limit the text input to.
    /// Maps to TMP_InputField.characterLimit
    /// </summary>
    public int CharacterLimit { get; set; }
    /// <summary>
    /// Sets the input field as read only
    /// In slider and numeric entries, this also makes the slider and buttons uninteractable.
    /// Maps to TMP_InputField.readOnly
    /// </summary>
    public bool IsReadOnly { get; set; }
}
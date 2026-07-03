using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UIExtensions;

/// <summary>
/// Default implementation of the MelonLoader ValueValidator class.
/// This satisfies the required members but just acts as a passthrough.
/// It's the equivalent of not having a validator at all, but it allows for the use of the other descriptor interfaces without needing to implement a custom validator.
/// </summary>
public partial class DefaultValidator : ValueValidator, IUiExtension
{
    public override bool IsValid(object value) { return true; }
    public override object EnsureValid(object value) { return value; }
}
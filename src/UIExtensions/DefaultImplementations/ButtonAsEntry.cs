using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UIExtensions;

internal class ButtonAsEntry : DefaultValidator, IButtonDescriptor
{
    public override bool IsValid(object value) { return true; }
    public override object EnsureValid(object value) { return false; }
    public string ButtonText { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Description { get; set; } = "";
    public Action Handler { get; set; }
}

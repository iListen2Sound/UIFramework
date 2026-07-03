using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UIExtensions;


/// <summary>
/// Default implementation of IUserEditedNotifier
/// Use this if you wanna be informed of edits made by the user that aren't applied to the Value property yet
/// </summary>
public class UserEditNotifier : DefaultValidator, IUserEditedNotifier
{
    ///<inheritdoc/>
    public Action<object> OnUserEdit { get; set; }
}
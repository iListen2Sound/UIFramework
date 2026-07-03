using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UiExtensions;

/// <summary>
/// Default implementation of IRefreshInhibitor
/// Prevent entry from triggering a refresh when a user edits values in the UI or when the entry value changes in code
/// Useful for making sure the UI doesn't refresh while the user is actively editing an entry
/// <br/>
/// This might be removed in a future version. Use this only if you can't find a way to prevent the UI from updating
/// when the user uses a control that has continuous triggers (e.g. sliders) and you can't find a way to defer value application
/// (e.g. using an event trigger for OnPointerUp)
/// </summary>
/// <remarks>
/// These prevent the entry from <em>causing</em> the UI to refresh
/// This does not mean the entry prevents interruptions from refreshes
/// </remarks>
public class RefreshInhibitor : DefaultValidator, IRefreshInhibitor
{
    ///<inheritdoc/>
    public bool InhibitRefreshOnEdit {get; set;} = false;
    ///<inheritdoc/>
    public bool InhibitRefreshOnValueChange {get; set;} = false;
}
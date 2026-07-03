using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UIExtensions;

/// <summary>
/// Prevent entry from triggering a refresh when a user edits values in the UI or when the entry value changes in code
/// Useful for making sure the UI doesn't refresh while the user is actively editing an entry
/// This might be removed in a future version. Use this only if you can't find a way to prevent the UI from updating
/// when the user uses a control that has continuous triggers (e.g. sliders) and you can't find a way to defer value application
/// (e.g. using an event trigger for OnPointerUp)
/// </summary>
/// <remarks>
/// These prevent the entry from <em>causing</em> the UI to refresh
/// This does not mean the entry prevents interruptions from refreshes
///
/// </remarks>
public interface IRefreshInhibitor : IUiExtension
{
    /// <summary>
    /// Prevents the UI from automatically refreshing when the user edits an entry
    /// Use this when the entry involves a control that has continuous input with no easy way to detect when user input has ended
    /// </summary>
    public bool InhibitRefreshOnEdit { get; set; }
    /// <summary>
    /// Prevents the UI from refreshing when the value of the entry changes in the background.
    /// Use this on entries where your code might change its values while the user is using the UI
    /// <remarks>
    /// This also means that the entry's value won't be reflected in the UI.
    /// </remarks>
    /// </summary>
    public bool InhibitRefreshOnValueChange { get; set; }
}
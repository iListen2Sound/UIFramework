using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UIExtensions;

/// <summary>
/// Use this if you wanna be informed of edits made by the user that aren't applied to the Value property yet
/// </summary>
/// <remarks>Released</remarks>
public interface IUserEditedNotifier : IUiExtension
{
    /// <summary>
    /// Subscribe to this action the method you want to run when the edits a value in the UI.
    /// It must take an object parameter for the new value
    /// </summary>
    public abstract Action<object> OnUserEdit { get; set; }
}


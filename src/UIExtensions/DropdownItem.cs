using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UIExtensions;

/// <summary>
/// Describes an option displayed in the dropdown. Each one has a name and a Value.
/// Note that this isn't the value type for your entry.
/// This is only used to describe what options show up in the dropdown
/// </summary>
/// <remarks>Released</remarks>
public class DropdownItem
{
    /// <summary>
    /// How the item shows in the dropdown
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// The actual value that gets stored in your entry
    /// </summary>
    public object Value { get; set; }
    public DropdownItem(string displayName, object value)
    {
        DisplayName = displayName;
        Value = value;
    }

    public DropdownItem(string value)
    {
        DisplayName = value;
        Value = value;
    }
}
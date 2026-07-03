using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UiExtensions;

///	<summary>
/// Presents the entry as a dropdown and describes the options within it.
/// Important to note that your data shouldn't be stored as dropdown items.
/// Dropdown items are just descriptions purely for UI Framework to know what to display to users in the dropdown.
/// </summary>
/// <remarks>Released</remarks>
public interface IDynamicDropdownDescriptor : IUiExtension
{
    /// <summary>
    /// Returns the list of dropdown items
    /// </summary>
    /// <returns></returns>
    public List <DropdownItem> GetDropdownItems();
    /// <summary>
    /// <para>Sets the items to be displayed in the dropdown. </para>
    /// <para>When doing a custom implementation, make sure to fire OnDropdownItemsUpdated after storing your data</para>
    /// </summary>
    /// <param name="items"></param>
    public void SetDropdownItems(List<DropdownItem> items);
    /// <summary>
    /// Invoking this action signals to the dropdown entry to update its dropdown list without having to update the whole UI
    /// </summary>
    public Action OnDropdownItemsUpdated { get; set; }

}

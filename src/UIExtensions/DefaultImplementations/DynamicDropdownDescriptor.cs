using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UiExtensions;

///<summary>
///Default implementation for IDynamicDropdownDescriptor
/// </summary>
/// <see cref="IDynamicDropdownDescriptor"/>
public class DynamicDropdownDescriptor : DefaultValidator, IDynamicDropdownDescriptor
{
    private List<DropdownItem> _dropdownItems = new();
    /// <inheritdoc/>
    public List<DropdownItem> GetDropdownItems() { return _dropdownItems; }
    /// <inheritdoc/>
    public void SetDropdownItems(List<DropdownItem> items)
    {
        _dropdownItems = items;
        OnDropdownItemsUpdated?.Invoke();
    }
    /// <summary>
    /// Adds an item to the dropdown item list
    /// </summary>
    /// <param name="item"></param>
    public void AddDropdownItem(DropdownItem item)
    {
        _dropdownItems.Add(item);
        OnDropdownItemsUpdated?.Invoke();
    }
    /// <summary>
    /// Removes an item from the dropdown list
    /// </summary>
    /// <param name="item"></param>
    public void RemoveDropdownItem(DropdownItem item)
    {
        _dropdownItems?.Remove(item);
        OnDropdownItemsUpdated?.Invoke();
    }

    /// <summary>
    /// Builder-like function that you can chain to a statement that adds a dropdown item list and returns the instance of this class
    /// </summary>
    /// <param name="dropdownItemList"></param>
    /// <returns></returns>
    public DynamicDropdownDescriptor WithDropdownItemList(List<DropdownItem> dropdownItemList)
    {
        _dropdownItems = dropdownItemList;
        return this;
    }
    /// <inheritdoc/>
    public DynamicDropdownDescriptor(List<DropdownItem> items)
    {
        _dropdownItems = items;
    }
    /// <inheritdoc/>
    public DynamicDropdownDescriptor()
    {
        _dropdownItems = new();
    }
    /// <inheritdoc/>
    public Action OnDropdownItemsUpdated { get; set; }
}
using MelonLoader;
using UIFramework.UiExtensions;

namespace UIFramework.Adapters;




/// <summary>
/// Dropdown adapter for dynamic dropdowns
/// </summary>
[RegisterTypeInIl2Cpp]
public class DynamicDopdownAdapter : DropDownAdapterBase
{
	/// <summary>
	/// Get the UI Extension as a dynamic dropdown descriptor
	/// </summary>
	public IDynamicDropdownDescriptor DropdownContents => DataEntry.UiExtension as IDynamicDropdownDescriptor;

	/// <summary>
	/// Gets the data from the dynamic dropdown descriptor to populate the dropdown
	/// </summary>
	protected override void GetDropdownData()
	{
		//Subscribe this method to the OnDropdownItemsUpdated event from the descriptor
		DropdownContents.OnDropdownItemsUpdated = GetDropdownData;
		try
		{
			Il2CppSystem.Collections.Generic.List<string> dropdownItems = new();
			foreach (DropdownItem item in DropdownContents.GetDropdownItems())
			{
				dropdownItems.Add(item.DisplayName);
			}
			dropdown.ClearOptions();
			dropdown.AddOptions(dropdownItems);
		}

		catch (Exception ex) { Debug.Log($"{ex}"); }
		SelectDropDownItem(EntryValue);

	}
	/// <summary>
	/// Select the display name from dropdown contents that corresponds to the model boxed value
	/// </summary>
	protected override void SelectDropDownItem(object boxedValue)
	{
		int itemToLoad = DropdownContents.GetDropdownItems().FindIndex(x => object.Equals(x.Value, boxedValue));
		dropdown.value = itemToLoad;
	}
	/// <summary>
	/// Submit the value represented by the selected item in the dropdown
	/// </summary>
	public override void SubmitDropdownValue()
	{
		SubmitValue((DropdownContents.GetDropdownItems()[dropdown.value]).Value);
	}
	/// <summary>
	/// Unsubscribe from the dropdown descriptor when this view gets destroyed
	/// </summary>
	void OnDestroy()
	{
		DropdownContents.OnDropdownItemsUpdated -= GetDropdownData;
	}
}
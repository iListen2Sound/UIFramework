using MelonLoader;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace UIFramework.Adapters;

/// <summary>
/// Dropdown Adapter for enums
/// </summary>
[RegisterTypeInIl2Cpp]
public class EnumDropdownAdapter : DropDownAdapterBase
{
	/// <summary>
	/// The enum type of the model value
	/// </summary>
	protected Type prefEnum;
	/// <summary>
	/// Gets the data from the enum to populate the dropdown
	/// </summary>
	protected override void GetDropdownData()
	{
		prefEnum = EntryValue.GetType();

		//Get a list of display name attributes or the enum name if not available
		Il2CppSystem.Collections.Generic.List<string> enumNames = new();
		foreach (var value in Enum.GetValues(prefEnum))
		{
			FieldInfo info = prefEnum.GetField(value.ToString());
			DisplayAttribute attr = info?.GetCustomAttribute<DisplayAttribute>();
			enumNames.Add(attr?.GetName() ?? value.ToString());
			_indexToValueMap.Add(Convert.ToInt32(value));
		}

		dropdown.ClearOptions();
		dropdown.AddOptions(enumNames);

	}
	/// <summary>
	/// Takes the boxedValue from the model and sets the dropdown value accordingly
	/// This allows for enum selections that might not be in order 4
	/// or doesn't start at 0
	/// </summary>
	protected override void SelectDropDownItem(object boxedValue)
	{
		//Get the boxed value as int, then select it from the indextovalue map and select that value in the dropdown
		dropdown.value = _indexToValueMap.IndexOf((int)boxedValue);
	}
	/// <summary>
	/// Take the int from _indexToValue map corresponding to the index of the selection of the dropdown.
	/// Convert that to an enum then submit it to the model
	/// </summary>
	public override void SubmitDropdownValue()
	{
		SubmitValue(Enum.ToObject(prefEnum, _indexToValueMap[dropdown.value]));
	}
}
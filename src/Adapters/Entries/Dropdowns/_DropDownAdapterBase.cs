using Il2CppRUMBLE.Combat.ShiftStones;
using Il2CppTMPro;
using MelonLoader;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Tomlet;
using Tomlet.Models;
using UIFramework.Models;
using UIFramework.UiExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UIFramework.Debug;

namespace UIFramework.Adapters;

/// <summary>
/// The base class for dropdown adapters
/// </summary>
[RegisterTypeInIl2Cpp]
public abstract class DropDownAdapterBase : DataEntryAdapter
{
	//protected DataEntryModelBase _prefModel => (DataEntryModelBase)EntryModel;
	/// <summary>
	/// List of ints serving to map the selected index to the the actual entry value of that index in the dropdown
	/// </summary>
	protected System.Collections.Generic.List<int> _indexToValueMap = new();
	/// <summary>
	/// The actual dropdown control
	/// </summary>
	protected TMP_Dropdown dropdown;
	/// <summary>
	/// The value of the model entry
	/// </summary>
	protected object EntryValue { get; set; }
	/// <summary>
	/// <inheritdoc/>
	/// Sets the dropdown object and the EntryValue from the model
	/// Also calls the select dropdown item method
	/// Adds the OnValueChanged listener
	/// </summary>
	protected override void DisplayData(object boxedValue)
	{

		dropdown = this.gameObject.transform.Find("Data/DropdownControl").GetComponent<TMP_Dropdown>();
		EntryValue = boxedValue;
		GetDropdownData();
		SelectDropDownItem(boxedValue);


		dropdown.onValueChanged.AddListener((UnityAction<int>)OnValueChanged);
	}
	/// <summary>
	/// Submits the dropdown selection
	/// </summary>
	public void OnValueChanged(int index)
	{
		SubmitDropdownValue();
	}
	/// <summary>
	/// Virtual overridable method for submitting values
	/// </summary>
	public virtual void SubmitDropdownValue()
	{

	}
	/// <summary>
	/// Virtual Overridable Method for gettting dropdown data. Difference between dynamic vs enum dropdowns
	/// </summary>
	protected virtual void GetDropdownData()
	{
		Debug.Log("GetDropdownData not implemented for " + this.GetType().Name, false, 2);
	}
	/// <summary>
	/// Virtual overridable method for selecting dropdown data. Difference between dynamic vs enum dropdowns
	/// </summary>
	protected virtual void SelectDropDownItem(object boxedValue)
	{
		Debug.Log("SelectDropDownItem not implemented for " + this.GetType().Name, false, 2);
	}
	void Start()
	{

	}
}

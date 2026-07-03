using MelonLoader;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIFramework.Adapters;

/// <summary>
/// <inheritdoc/>
/// Adapter for booleans shown as toggles
/// </summary>
[RegisterTypeInIl2Cpp]
public class BoolToggleAdapter : DataEntryAdapter
{
	/// <summary>
	/// The actual toggle control component
	/// </summary>
	protected Toggle Toggle => this.gameObject.transform.Find("Data/ToggleControl").gameObject.GetComponent<Toggle>();
	//protected override DataEntryModelBase DataEntry => (DataEntryModelBase)EntryModel;
	/// <summary>
	/// Gets the value of the toggle
	/// </summary>
	public bool EnteredValue => this.gameObject.transform.Find("Data/ToggleControl").gameObject.GetComponent<Toggle>().isOn;
	/// <inheritdoc/>
	protected override void DisplayData(object boxedValue)
	{
		Toggle.isOn = (bool)boxedValue;
		Toggle.onValueChanged.AddListener((UnityAction<bool>)OnValueChanged);
	}

	public void OnValueChanged(bool newValue)
	{
		SubmitValue(newValue);
	}
}
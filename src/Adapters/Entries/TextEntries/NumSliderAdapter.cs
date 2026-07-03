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
/// <inheritdoc/>
/// Adapter for slider entries. Inherits from Text inputs
/// </summary>
[RegisterTypeInIl2Cpp]
public class NumSliderAdapter : TextEntryAdapter
{
	/// <summary>
	/// Control component for slider
	/// </summary>
	protected Slider Slider => gameObject.transform.Find("Data/SliderControl").gameObject.GetComponent<UnityEngine.UI.Slider>();
	/// <summary>
	///UI Extension for the slider descriptor
	/// </summary>
	protected virtual ISliderDescriptor SliderSettings => UiExtension as ISliderDescriptor;

	/// <summary>
	/// <inheritdoc/>
	/// Gets the data from the model then set the value of the slider to match
	/// </summary>
	protected override void DisplayData(object boxedValue)
	{
		base.DisplayData(boxedValue);
		//Make slider uninteractable if behavior descriptor calls for a readonly entry
		if (BehaviorDescriptor?.IsReadOnly == true)
		{
			Slider.interactable = false;
		}
		//Add events
		TextField.onEndEdit.AddListener((UnityAction<string>)EditEnd);
		TextField.onSelect.AddListener((UnityAction<string>)EditStart);
		//Sets the values according to the slider descriptor
		Slider.minValue = SliderSettings?.Min ?? 0;
		Slider.maxValue = SliderSettings?.Max ?? 100;
		Slider.value = Convert.ToSingle(boxedValue);
		//Set an event for a slider's onValueChanged to update the text input controls
		Slider.onValueChanged.AddListener((UnityAction<float>)OnValueChanged);
		//Add the onPointerUP event handler
		AddPointerUp();
		//sets the sliders to whole numbers only for integral values. 
		if (boxedValue is int or byte or short or long or sbyte or ushort or uint or ulong)
		{
			Slider.wholeNumbers = true;
			TextField.contentType = TMP_InputField.ContentType.IntegerNumber;
			TextField.text = Slider.value.ToString("F0");
		}
		else
		{
			Slider.wholeNumbers = false;
			TextField.contentType = TMP_InputField.ContentType.DecimalNumber;
			TextField.text = Slider.value.ToString("F" + SliderSettings?.DecimalPlaces);
		}
	}
	/// <summary>
	///Updates the text input with the slider's values
	/// </summary>
	protected void OnValueChanged(float newValue)
	{
		TextField.text = newValue.ToString(TextField.contentType == TMP_InputField.ContentType.IntegerNumber ? "F0" : "F" + SliderSettings?.DecimalPlaces);
		SliderSettings?.OnSliderValueChanged?.Invoke((float)newValue);
		//ApplyValueToPref();
		//Debug.Log($"Slider value changed to {newValue}", true);
	}
	/// <summary>
	/// Submits the slider's value to the model
	/// </summary>
	public void SubmitSliderValue()
	{
		SubmitValue(Convert.ChangeType(Slider.value, DataEntry.ModelBoxedValue.GetType()));
	}
	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	protected override void EditStart(string s)
	{
		TextField.textComponent.fontStyle = FontStyles.Normal;
	}
	/// <summary>
	/// <inheritdoc/>
	/// After editing the text input, apply the value in it to the slider position
	/// </summary>
	protected override void EditEnd(string s)
	{
		TextField.textComponent.fontStyle = FontStyles.Italic;

		if (float.TryParse(s, out float result))
		{
			if (SliderSettings != null)
			{
				result = Mathf.Clamp(result, SliderSettings.Min, SliderSettings.Max);
			}
			Slider.value = result;
			SubmitSliderValue();
		}
		else
		{
			Debug.Log($"Invalid input for slider: {s}", false, 2);
			TextField.text = Slider.value.ToString(TextField.contentType == TMP_InputField.ContentType.IntegerNumber ? "F0" : "F" + SliderSettings?.DecimalPlaces);
		}
	}
	/// <summary>
	/// Adds a pointer up event toe the slider which does the actual submission
	/// </summary>
	public void AddPointerUp()
	{
		EventTrigger trigger = Slider.gameObject.AddComponent<EventTrigger>();

		EventTrigger.Entry entry = new()
		{
			eventID = EventTriggerType.PointerUp
		};
		entry.callback.AddListener((UnityAction<BaseEventData>)PointerUP);

		trigger.triggers.Add(entry);

	}
	/// <summary>
	/// Only submit the slider value after the user stops dragging it
	/// Otherwise, UI will continously try to refresh making the user only
	/// able to edit one frame.
	/// </summary>
	protected void PointerUP(BaseEventData eventData)
	{
		SubmitSliderValue();
	}
}
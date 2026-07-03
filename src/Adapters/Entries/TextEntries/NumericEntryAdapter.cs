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
/// Entry adapter for a numeric view. Derives from text inputs
/// </summary>
[RegisterTypeInIl2Cpp]
public class NumericEntryAdapter : TextEntryAdapter
{
	/// <summary>
	/// Button for incrementing
	/// </summary>
	protected Button AddButton =>
		gameObject.transform.Find("Data/ButtonGroup/Add").gameObject.GetComponent<Button>();
	/// <summary>
	/// Button for decrementing
	/// </summary>
	protected Button SubtractButton =>
		gameObject.transform.Find("Data/ButtonGroup/Sub").gameObject.GetComponent<Button>();

	/// <summary>
	/// Gets the defined numeric steps from the descriptor. 
	/// If it doesn't exist, or is zero, it goes back to the default depending on the numeric type.
	/// </summary>
	protected float IncrementStep
	{
		get
		{
			float step = (NumericSettings)?.Steps ?? 0;

			if (step == 0)
			{
				step = CurrentBoxedValue switch
				{
					byte or sbyte or short or ushort or int or uint or long or ulong => 1f,
					_ => 0.1f
				};
			}

			return step;
		}
	}

	/// <summary>
	/// UI Extension as NumberboxDescriptor NumericSettings
	/// </summary>
	protected virtual INumberBoxDescriptor NumericSettings => UiExtension as INumberBoxDescriptor;

	/// <summary>
	/// <inheritdoc/>
	/// Subscribes the button click events to increment/decrement specifically.
	/// </summary>
	protected override void Start()
	{
		base.Start();

		AddButton.onClick.AddListener((UnityAction)Increment);
		SubtractButton.onClick.AddListener((UnityAction)Decrement);

	}

	protected override void DisplayData(object boxedValue)
	{
		//Call the original displaydata method
		base.DisplayData(boxedValue);

		//Take the string, convert it to the correct type, then convert it back to a string with the correct number of decimal places
		if (boxedValue.GetType() != typeof(int))
		{
			TextField.text = Convert.ToSingle(boxedValue).ToString($"F{NumericSettings?.DecimalPlaces ?? 1}");
		}
		//If marked as readonly, disable the increment and decrement buttons
		if (BehaviorDescriptor?.IsReadOnly == true)
		{
			AddButton.interactable = false;
			SubtractButton.interactable = false;
		}

	}
	/// <summary>
	/// Increment button event
	/// </summary>
	protected virtual void Increment()
	{
		//Cancel operation if behavior descriptor marks it as readonly
		if (BehaviorDescriptor?.IsReadOnly == true) return;
		SubmitValue(CurrentBoxedValue == null ? IncrementStep : Convert.ChangeType(Convert.ToSingle(CurrentBoxedValue) + IncrementStep, CurrentBoxedValue.GetType()));
	}
	/// <summary>
	/// Decerement button event
	/// </summary>
	protected virtual void Decrement()
	{
		//Cancel operation if behavior descriptor marks it as readonly
		if (BehaviorDescriptor?.IsReadOnly == true) return;
		SubmitValue(CurrentBoxedValue == null ? -IncrementStep : Convert.ChangeType(Convert.ToSingle(CurrentBoxedValue) - IncrementStep, CurrentBoxedValue.GetType()));
	}
}
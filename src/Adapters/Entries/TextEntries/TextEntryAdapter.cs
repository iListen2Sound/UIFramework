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
/// Base controller for text fields
/// </summary>
[RegisterTypeInIl2Cpp]
public class TextEntryAdapter : DataEntryAdapter
{
	/// <summary>
	/// Returns the textfield
	/// </summary>
	protected TMP_InputField TextField => this.gameObject.transform.Find("Data/TextControl").gameObject.GetComponent<TMP_InputField>();
	/// <summary>
	/// Sets the placeholder text in the TextField
	/// </summary>
	protected string PlaceHolderText { set { this.gameObject.transform.Find("Data/TextControl/Text Area/Placeholder").gameObject.GetComponent<TextMeshProUGUI>().text = value; } }

	/// <summary>
	/// A reference to the UI Extension as a behavior descriptor
	/// </summary>
	protected ITextInputBehaviorDescriptor BehaviorDescriptor => UiExtension as ITextInputBehaviorDescriptor;
	protected ITextInputAppearanceDescriptor AppearanceDescriptor => UiExtension as ITextInputAppearanceDescriptor;

	Type boxedValueType = null;

	/// <summary>
	/// <inheritdoc/>
	/// Assigns the behavior descriptor properties if present. 
	/// </summary>
	protected override void DisplayData(object boxedValue)
	{
		if (BehaviorDescriptor is not null)
		{
			TextField.contentType = BehaviorDescriptor.ContentType;
			TextField.characterLimit = BehaviorDescriptor.CharacterLimit;
			TextField.readOnly = BehaviorDescriptor.IsReadOnly;
			TextField.asteriskChar = BehaviorDescriptor.PasswordChar;
		}

		boxedValueType = boxedValue.GetType();
		//If boxedvaluetype is a string, display directly
		if (boxedValueType == typeof(string))
		{
			TextField.text = (string)boxedValue;
		}
		//Otherwise resort to TOML
		else
		{
			try
			{
				TextField.text = ToTomlString(boxedValue);

			}
			catch (Exception ex)
			{
				Debug.Log($"{ex.Message}\n{ex.StackTrace}");
			}
		}
	}
	/// <summary>
	/// Parse the contents in the text field through toml then submit it.
	/// </summary>
	protected void ParseThenSubmit()
	{
		if (boxedValueType == typeof(string))
		{
			SubmitValue(TextField.text);
		}
		else
		{
			try
			{
				if (TextField.text.Trim() != "")
				{
					SubmitValue(FromTomlString(TextField.text, DataEntry.ModelBoxedValue.GetType()));
				}
			}
			catch (Exception ex)
			{
				Log(ex.Message, false, 2);
			}
		}
	}

	/// <summary>
	/// Sets the text to non-italicized font style when editing. 
	/// Replaces the need for managing the placeholder text
	/// </summary>
	protected virtual void EditStart(string s)
	{
		if (BehaviorDescriptor?.IsReadOnly == true) return;
		TextField.textComponent.fontStyle = FontStyles.Normal;
	}
	/// <summary>
	/// Sets the text to italicized when done editing
	/// </summary>
	protected virtual void EditEnd(string s)
	{
		if (BehaviorDescriptor?.IsReadOnly == true) return;
		TextField.textComponent.fontStyle = FontStyles.Italic;
		ParseThenSubmit();
	}
	/// <summary>
	/// Unity callback. Subscribes EditStart and EditEnd to the appropriate events
	/// </summary>
	protected virtual void Start()
	{

		TextField.onSelect.AddListener((System.Action<string>)EditStart);
		TextField.onDeselect.AddListener((System.Action<string>)EditEnd);
	}
}
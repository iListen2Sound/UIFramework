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
/// Inherit this class to create your own custom entry controllers for your own input controls.
/// </summary>
/// <remarks>Released</remarks>
[RegisterTypeInIl2Cpp]
public abstract class DataEntryAdapter : PrefEntryAdapterBase
{
	//public override void ModelSet() { base.ModelSet(); }
	/// <summary>
	/// Model for the data entry
	/// </summary>
	private protected DataEntryModelBase DataEntry => (DataEntryModelBase)EntryModel;
	/// <summary>
	/// The boxed value loaded from the model when it was first set
	/// </summary>
	protected object CurrentBoxedValue;
	/// <summary>
	/// Reference to the UI Extension
	/// </summary>
	protected IUiExtension UiExtension => DataEntry.UiExtension;

	/// <inheritdoc/>
	public override void PreSaveAction()
	{

	}
	/// <inheritdoc/>
	protected sealed override void DisplayContents()
	{
		CurrentBoxedValue = DataEntry.ModelBoxedValue;
		DisplayData(DataEntry.ModelBoxedValue);
	}
	/// <summary>
	/// Passes the data from the model for adapting into a format displayed to the user
	/// </summary>
	/// <param name="boxedValue"></param>
	protected virtual void DisplayData(object boxedValue)
	{

	}
	/// <summary>
	/// Submits the value to the model
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	protected bool SubmitValue(object value)
	{
		return DataEntry.TryApply(value);
	}
}
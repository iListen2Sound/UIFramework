using Il2CppTMPro;
using MelonLoader;
using Tomlet;
using Tomlet.Models;
using UIFramework.Models;

namespace UIFramework.Adapters;



[RegisterTypeInIl2Cpp]
public abstract class PrefEntryAdapterBase : SubModelAdapter
{
	/// <summary>
	/// Sets the description text
	/// </summary>
	protected virtual string DescriptionText
	{
		get { return this.gameObject.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>().text; }
		set { this.gameObject.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>().text = value; }
	}
	/// <summary>
	/// Sets the identifier text
	/// </summary>
	protected virtual string DisplayName
	{
		get { return this.gameObject.gameObject.transform.Find("Data/Label").gameObject.GetComponent<TextMeshProUGUI>().text; }
		set { this.gameObject.gameObject.transform.Find("Data/Label").gameObject.GetComponent<TextMeshProUGUI>().text = value; }
	}

	/// <summary>
	/// The model for the entry
	/// </summary>
	protected private EntryModelBase EntryModel => (EntryModelBase)_internalModel;
	/// <summary>
	/// This is called when the Save button is pressed. Override to create custom behaviour.
	/// </summary>
	/// <remarks>Generally MelonPreferences are saved from the category, not the indivial entries.</remarks>
	public virtual void PreSaveAction()
	{
	}
	/// <summary>
	/// Called when the model has been set. Calls DisplayMetadata and DisplayContents in that order
	/// </summary>
	protected sealed override void ModelSet()
	{
		DisplayMetadata();
		DisplayContents();
	}
	/// <summary>
	/// Displays the entry model's display name and description by invoking DisplayEntryInfo.
	/// </summary>
	protected sealed override void DisplayMetadata()
	{
		DisplayEntryInfo(EntryModel.DisplayName, EntryModel.Description);
	}
	/// <summary>
	/// Displays the name and description on to the View object
	/// </summary>
	/// <param name="displayName"></param>
	/// <param name="description"></param>
	protected virtual void DisplayEntryInfo(string displayName, string description)
	{
		DescriptionText = description;
		DisplayName = displayName;
	}
	/// <summary>
	/// Displays actual data content
	/// </summary>
	protected virtual void DisplayContents()
	{

	}
	/// <summary>
	/// Serializes an object into the toml string representation of that object
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public string ToTomlString(object input)
	{
		return TomletMain.ValueFrom(input).SerializedValue;
	}
	/// <summary>
	/// Parses a toml string into an object of type with its value.
	/// </summary>
	/// <param name="input"></param>
	/// <param name="targetType"></param>
	/// <returns></returns>
	public object FromTomlString(string input, Type targetType)
	{
		string wrappedEntry = $"temp = {input.Trim()}";

		TomlParser parser = new();
		TomlDocument inputToml = parser.Parse(wrappedEntry);
		TomlValue inputVal = inputToml.GetValue("temp");

		return TomletMain.To(targetType, inputVal);
	}
}
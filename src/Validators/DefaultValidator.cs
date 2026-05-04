using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;
namespace UIFramework.ValidatorExtensions
{
	public interface IUiExtension
	{
		
	}
	/// <summary>
	/// Default implementation of the MelonLoader ValueValidator class.
	/// This satisfies the required members but just acts as a passthrough. 
	/// It's the equivalent of not having a validator at all, but it allows for the use of the other descriptor interfaces without needing to implement a custom validator.
	/// </summary>
	public partial class DefaultValidator : ValueValidator, IUiExtension
	{
		public override bool IsValid(object value) { return true; }
		public override object EnsureValid(object value) { return value; }
	}
	/// <summary>
	/// Describes properties for text inputs that define its behavior.
	/// </summary>
	public interface ITextInputBehaviorDescriptor : IUiExtension
	{
		/// <summary>
		/// What type of content is going into the textinput
		/// </summary>
		public TMP_InputField.ContentType ContentType { get; set; }
		public int CharacterLimit { get; set; }
		public bool IsReadOnly { get; set; }
	}
	/// <inheritdoc cref="ITextInputBehaviorDescriptor"/>
	public class TextInputBehaviorDescriptor : DefaultValidator, ITextInputBehaviorDescriptor
	{
		public TMP_InputField.ContentType ContentType { get; set; } = TMP_InputField.ContentType.Standard;
		public int CharacterLimit { get; set; } = 0;
		public bool IsReadOnly { get; set; } = false;
	}


	/// <summary>
	/// Describes properties of text inputs that define how it looks
	/// </summary>
	public interface ITextInputAppearanceDescriptor : IUiExtension
	{
		public int FontSize { get; set; }
		/// <summary>
		/// Set to true to have the text auto size between AutoSizeMin and AutoSizeMax. If false, FontSize will be used as the font size.
		/// </summary>
		public bool IsAutoSizing { get; set; }
		public int AutoSizeMin { get; set; }
		public int AutoSizeMax { get; set; }

		public FontStyles FontStyle { get; set; }
		public bool IsRichText { get; set; }
	}
	/// <inheritdoc cref="ITextInputAppearanceDescriptor"/>
	public class TextInputAppearanceDescriptor : DefaultValidator, ITextInputAppearanceDescriptor
	{
		public int FontSize { get; set; } = 18;
		public bool IsAutoSizing { get; set; } = false;
		public int AutoSizeMin { get; set; } = 14;
		public int AutoSizeMax { get; set; } = 30;
		public FontStyles FontStyle { get; set; } = FontStyles.Normal;
		public bool IsRichText { get; set; } = true;
	}


	///	<summary>
	/// Presents the entry as a dropdown and describes the options within it.
	/// </summary>
	public interface IDynamicDropdownDescriptor : IUiExtension
	{
		public List<string> DropdownOptionNames { get; set; }
	}
	/// <see cref="IDynamicDropdownDescriptor"/>
	public class DynamicDropdownDescriptor : DefaultValidator, IDynamicDropdownDescriptor
	{
		public List<string> DropdownOptionNames { get; set; } = new List<string>();
	}


	/// <summary>
	/// Implementing this will present the entry as a slider in the UI
	/// </summary>
	public interface ISliderDescriptor : IUiExtension
	{
		public float Min { get; set; }
		public float Max { get; set; }
		public int DecimalPlaces { get; set; }
	}
	/// <summary>
	/// Default implementation of ISliderDescriptor. Used for numeric inputs that want to be sliders. DecimalPlaces defaults to 5, Min defaults to 0, Max defaults to 1.
	/// </summary>
	/// <see cref="ISliderDescriptor"/>
	public class SliderDescriptor : DefaultValidator, ISliderDescriptor
	{
		/// <summary>
		/// Minimum value. Defaults 0
		/// </summary>
		public float Min { get; set; } = 0;
		/// <summary>
		/// Max value. Defaults 1.
		/// </summary>
		public float Max { get; set; } = 1;
		/// <summary>
		/// Decimal Places. Defaults 5
		/// </summary>
		public int DecimalPlaces { get; set; } = 5;
	}

	/// <summary>
	/// Describes numeric up down controls
	/// </summary>
	public interface INumericUpDownDescriptor : IUiExtension
	{
		/// <summary>
		/// 0 = default (1 for ints, 0.1 for floats). 
		/// </summary>
		public float Increments { get; set; }
	}
	///<inheritdoc cref="INumericUpDownDescriptor"/>
	public class NumericUpDownDescriptor : DefaultValidator, INumericUpDownDescriptor
	{
		/// <inheritdoc/>
		public float Increments { get; set; }
	}

	public interface IInteractable : IUiExtension
	{
		public event Action<EventArgs> Interaction;
	}

	/// <summary>
	/// Use this if you wanna be informed of edits made by the user that aren't applied to the Value property yet
	/// </summary>
	public interface IUserEditedNotifier : IUiExtension
	{
		/// <summary>
		/// Subscribe to this action the method you want to run when the edits a value in the UI.
		/// It must take an object parameter for the new value
		/// </summary>
		public abstract Action<object> OnUserEdit { get; set; }
	}


	/// <summary>
	/// Default implementation of IUserEditedNotifier
	/// Use this if you wanna be informed of edits made by the user that aren't applied to the Value property yet
	/// </summary>
	public class UserEditDefaultNotifier : DefaultValidator, IUserEditedNotifier
	{ 
		///<inheritdoc/>
		public Action<object> OnUserEdit { get; set; }
	}

	/// <summary>
	/// Prevent entry from triggering a refresh when a user edits values in the UI or when the entry value changes in code
	/// Useful for making sure the UI doesn't refresh while the user is actively editing an entry 
	/// 
	/// This might be removed in a future version. Use this only if you can't find a way to prevent the UI from updating
	/// when the user uses a control that has continuous triggers (e.g. sliders) and you can't find a way to defer value application
	/// (e.g. using an event trigger for OnPointerUp)
	/// </summary>
	/// <remarks>
	/// These prevent the entry from <em>causing</em> the UI to refresh
	/// This does not mean the entry prevents interruptions from refreshes
	/// 
	/// </remarks>
	public interface IRefreshInhibitor : IUiExtension
	{
		/// <summary>
		/// Prevents the UI from automatically refreshing when the user edits an entry
		/// Use this when the entry involves a control that has continuous input with no easy way to detect when user input has ended
		/// </summary>
		public bool InhibitRefreshOnEdit {get; set;}
		/// <summary>
		/// Prevents the UI from refreshing when the value of the entry changes in the background. 
		/// Use this on entries where your code might change its values while the user is using the UI
		/// <remarks>
		/// This also means that the entry's value won't be reflected in the UI.
		/// </remarks>
		/// </summary>
		public bool InhibitRefreshOnValueChange {get; set;}
	}
	/// <summary>
	/// Default implementation of IRefreshInhibitor
	/// Prevent entry from triggering a refresh when a user edits values in the UI or when the entry value changes in code
	/// Useful for making sure the UI doesn't refresh while the user is actively editing an entry 
	/// <br/>
	/// This might be removed in a future version. Use this only if you can't find a way to prevent the UI from updating
	/// when the user uses a control that has continuous triggers (e.g. sliders) and you can't find a way to defer value application
	/// (e.g. using an event trigger for OnPointerUp)
	/// </summary>
	/// <remarks>
	/// These prevent the entry from <em>causing</em> the UI to refresh
	/// This does not mean the entry prevents interruptions from refreshes
	/// </remarks>
	public class DefaultRefreshInhibitor : DefaultValidator, IRefreshInhibitor
	{
		///<inheritdoc/>	
		public bool InhibitRefreshOnEdit {get; set;} = false;
		///<inheritdoc/>
		public bool InhibitRefreshOnValueChange {get; set;} = false;
	}

	public interface ICustomUIProvider : IUiExtension
	{
		public GameObject WidgetPrefab { get; set; }
	}


	public interface IButtonDescriptor : IUiExtension
	{
		public string ButtonText { get; set; }
		public string DisplayName { get; set; }
		public string Description { get; set; }
		public Action Handler { get; set; }
	}

	internal class ButtonAsEntry : DefaultValidator, IButtonDescriptor
	{
		public override bool IsValid(object value) { return true; }
		public override object EnsureValid(object value) { return false; }
		public string ButtonText { get; set; } = "";
		public string DisplayName { get; set; } = "";
		public string Description { get; set; } = "";
		public Action Handler { get; set; }
	}

}
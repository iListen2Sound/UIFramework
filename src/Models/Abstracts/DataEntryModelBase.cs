using MelonLoader;
using UnityEngine;
using UIFramework.UiExtensions;
using UIFramework.Adapters;
namespace UIFramework.Models;

/// <summary>
/// A model for interfacing with a piece of data.
/// </summary>
public abstract class DataEntryModelBase : EntryModelBase
{
    protected DataEntryModelBase(CategoryModelBase parentCategory) : base(parentCategory) { }


    public abstract object ModelBoxedValue { get; protected set; }
    public virtual bool TryApply(object value)
    {
        bool result = false;
        try
        {
            //ModelBoxedValue = value;
            SetDataValue(value);
            result = true;

            //ParentCategory.ParentMod.RequestUpdateUI();
        }
        catch (Exception ex)
        {
            Debug.Log($"ModelDataEntry TryApply: {ex.Message}\n{ex.StackTrace}", false, 2);
            result = false;

        }
        return result;
    }

    protected virtual void OnDataValueChanged(object newValue)
    {
        if (!(RefreshInhibitor?.InhibitRefreshOnValueChange ?? false))
        {
            UI.RequestRefresh(ParentCategory.ParentMod);
        }
        else
            Debug.Log($"UI Refresh inhibited when entry value changes", true);
    }

    public abstract IUiExtension UiExtension { get; }
    public virtual IUserEditedNotifier EditNotifier => UiExtension as IUserEditedNotifier;
    public virtual IRefreshInhibitor RefreshInhibitor => UiExtension as IRefreshInhibitor;


    protected void SetDataValue(object newValue)
    {

        Debug.Log($"New Value Applied {newValue}", true);
        ModelBoxedValue = newValue;
        EditNotifier?.OnUserEdit?.Invoke(ModelBoxedValue);
        //Block refresh only if RefreshInhibitorExists with the InhibitRefresh property set to true.
        if (!(RefreshInhibitor?.InhibitRefreshOnEdit ?? false))
        {
            ParentCategory.ParentMod.RequestUpdateUI();
        }
        else
            Debug.Log($"UI Refresh inhibited when user edits values", true);

    }
    protected GameObject _uiPrefabSource;
    /// <summary>
    /// Returns an instance of the game object associated with the MelonPreferences_Entry type.
    /// If a custom one is provided, it will return an instance of that instead
    /// </summary>
    /// <returns>TODO: Move this to the UI builders as this is ui logic</returns>
    public override GameObject GetNewUIInstance()
    {
        //Make the custom UI provided by the validator the first priority if it exists.	
        if (UiExtension is ICustomViewProvider uiProvider)
        {
            if (uiProvider?.EntryViewPrefab is not null)
                return GameObject.Instantiate(uiProvider.EntryViewPrefab);
        }

        if (UiExtension is DynamicDropdownDescriptor)
        {
            GameObject dropdown = UI.GetPrefabInstance(PrefabType.Dropdown);
            dropdown.AddComponent<DynamicDopdownAdapter>();
            return dropdown;
        }

        if (UiExtension is ISliderDescriptor)
        {
            GameObject slider = UI.GetPrefabInstance(PrefabType.Slider);
            slider.AddComponent<NumSliderAdapter>();
            return slider;
        }
        if (UiExtension is IButtonDescriptor)
        {
            GameObject button = UI.GetPrefabInstance(PrefabType.Button);
            button.AddComponent<ButtonEntryAdapter>();
            return button;
        }



        switch (ModelBoxedValue)
        {
            case bool:
                GameObject toggle = UI.GetPrefabInstance(PrefabType.Toggle);
                toggle.AddComponent<BoolToggleAdapter>();
                return toggle;
            case string:
                GameObject textField = UI.GetPrefabInstance(PrefabType.TextField);
                textField.AddComponent<TextEntryAdapter>();
                return textField;
            case Enum:
                GameObject dropdown = UI.GetPrefabInstance(PrefabType.Dropdown);
                dropdown.AddComponent<EnumDropdownAdapter>();
                return dropdown;

            //numerics
            //integer types
            case sbyte:
            case byte:
            case short:
            case ushort:
            case int:
            case uint:
            case long:
            case ulong:
                GameObject intInput = UI.GetPrefabInstance(PrefabType.NumericInt);
                intInput.AddComponent<NumericEntryAdapter>();
                return intInput;
            //floating point types
            case float:
            case double:
            case decimal:
                GameObject floatInput = UI.GetPrefabInstance(PrefabType.NumericFloat);
                floatInput.AddComponent<NumericEntryAdapter>();
                return floatInput;
            default:
                //Debug.Log("Unsupported type detected with no custom widget prefab provided. Defaulting to text input. Creating custom component recommended", true, 1);
                GameObject defaultInput = UI.GetPrefabInstance(PrefabType.TextField);
                defaultInput.AddComponent<TextEntryAdapter>();
                return defaultInput;
        }

    }

}
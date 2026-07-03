using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.UiExtensions;

namespace UIFramework.Models;

/// <summary>
///
/// </summary>
public class MelonEntryModel : DataEntryModelBase
{

    /// <summary>
    /// MelonPreferences_Entry this model is meant to adapt
    /// </summary>
    public MelonPreferences_Entry PrefEntry { get; set; }
    public override bool IsHidden
    {
        get => PrefEntry.IsHidden;
        set => PrefEntry.IsHidden = value;
    }
    /// <inheritdoc/>
    public override string Identifier => PrefEntry.Identifier;
    /// <inheritdoc/>
    public override string DisplayName => PrefEntry.DisplayName;
    /// <inheritdoc/>
    public override string Description => PrefEntry.Description;

    public ValueValidator MelonValidator => PrefEntry.Validator;
    public override IUiExtension UiExtension => MelonValidator as IUiExtension;

    /// <summary>
    /// Direct access to the PrefEntry boxedvalue property
    /// </summary>
    public override object ModelBoxedValue
    {
        get => PrefEntry.BoxedEditedValue;
        protected set => PrefEntry.BoxedEditedValue = value;
    }
    /// <summary>
    /// Creates a new instance of this object based around a MelonPreferences_Entry object
    /// </summary>
    public MelonEntryModel(MelonPreferences_Entry prefEntry, CategoryModelBase parentCategory)
        : base(parentCategory)
    {
        PrefEntry = prefEntry;
        SavedValue = prefEntry.BoxedValue;
        PrefEntry.OnEntryValueChangedUntyped.Subscribe(OnValueChanged);



    }
    protected void OnValueChanged(object oldVal, object newVal)
    {

        OnDataValueChanged(newVal);

    }
    /// <summary>
    /// The value actually saved to the file.
    /// </summary>
    public virtual object SavedValue { get; set; }
    /// <inheritdoc/>
    public override void SaveAction()
    {
        SavedValue = ModelBoxedValue;
    }
    ///	<inheritdoc/>
    public override void DiscardAction()
    {
        //Debug.Log($"MelonEntry discard action called. Current BoxedEditedValue: {PrefEntry.BoxedEditedValue}, actual ModelBoxedValue: {PrefEntry.ModelBoxedValue}", true);
        //Discard the BoxedEditedValue and reset it to the actual value of the preference
        PrefEntry.BoxedEditedValue = PrefEntry.BoxedValue;
    }
}
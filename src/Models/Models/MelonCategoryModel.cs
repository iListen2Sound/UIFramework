using Il2CppRUMBLE.Utilities;
using MelonLoader;
using MelonLoader.Preferences;
using System.Reflection;
using UIFramework.UiExtensions;
using UnityEngine;

namespace UIFramework.Models;

public class MelonCategoryModel : CategoryModelBase
{

    /// <summary>
    /// The MelonPreferences_Category object this adapts into the framework
    /// </summary>
    public MelonPreferences_Category PrefCat;
    public override bool IsHidden
    {
        get => PrefCat.IsHidden;
        set => PrefCat.IsHidden = value;
    }
    /// <inheritdoc/>
    public override string Identifier => PrefCat.Identifier;
    /// <inheritdoc/>
    public override string DisplayName => PrefCat.DisplayName.Trim() == "" ? PrefCat.Identifier : PrefCat.DisplayName;

    /// <summary>
    /// Creates a new instance of this class based on a MelonPreferences_Category
    /// </summary>
    public MelonCategoryModel(MelonPreferences_Category cat, ModModelBase parentMod)
        : base(parentMod)
    {
        PrefCat = cat;
        foreach (MelonPreferences_Entry entry in PrefCat.Entries)
        {
            SubModels.Add(new MelonEntryModel(entry, this));
        }

    }
    /// <inheritdoc/>
    public override void SaveAction()
    {
        PrefCat.SaveToFile(false);
    }

    public override void DiscardAction()
    {
        PrefCat.LoadFromFile(false);
        foreach (EntryModelBase entry in SubModels)
        {
            entry.DiscardAction();
        }
    }
}
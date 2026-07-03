using UnityEngine;

namespace UIFramework.Models;

/// <summary>
/// Models define how the UI is built. The heirarchy is simple and follows melonpreferences basic structure
/// MelonModel ->  MelonCategoryModel -> MelonEntryModel
/// Modders can use the default model just by calling UIF.Register(modInstance, categories) in their OnLateInitializeMelon.
/// The default model will use simple input methods: bools will be toggles, strings will be text input fields and so would numerics.
/// More options will eventually be available: sliders, dropdowns, multi checkboxes, radio buttons, etc.
///
/// Those will be developed after the default model is functional
/// </summary>

public class RootModel : IHoldSubmodels
{

    public virtual List<IModelable> SubModels { get; set; } = new();

    private string _name = string.Empty;

    public string Identifier => _name;
    public string DisplayName => _name;
    public bool IsHidden { get; set; } = false;

    public void SetName(string name)
    {
        _name = name;
    }

    public void AddSubmodel(IModelable mod)
    {
        int index = SubModels.FindIndex(m => m.Identifier == mod.Identifier);
        if (index == -1)
            SubModels.Add(mod);
        else
            SubModels[index] = mod;
    }


    public IModelable GetSubmodel(string name)
    {
        return SubModels.FirstOrDefault(m => m.Identifier == name);
    }
    public ModModelBase GetModModel(string identifier)
    {
        return (ModModelBase)GetSubmodel(identifier);
    }

    public GameObject GetNewUIInstance() { return null; }

    public void SaveAction() { }
    public void DiscardAction() { }

}

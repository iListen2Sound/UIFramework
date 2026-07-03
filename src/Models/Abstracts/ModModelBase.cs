using MelonLoader;
using UnityEngine;
namespace UIFramework.Models;

public abstract class ModModelBase : SelectableModelBase
{

    public List<CategoryModelBase> Categories => SubModels.Cast<CategoryModelBase>().ToList();
    public abstract MelonBase Instance { get; set; }
    public override string Identifier => Instance.Info.Name;
    public string _displayName;
    public override string DisplayName => _displayName;

    public virtual string Version => Instance.Info.Version;
    /// <inheritdoc/>
    public override GameObject GetNewUIInstance()
    {
        return GameObject.Instantiate(Prefabs.ModTab);
    }
    /// <summary>
    ///
    /// </summary>
    public virtual CategoryModelBase GetModelCategory(string identifier)
    {
        return (CategoryModelBase)GetSubmodel(identifier);
    }
    /// <summary>
    ///
    /// </summary>
    public virtual void AddModelCategory(params CategoryModelBase[] categoryModel)
    {
        AddSubmodel(categoryModel.Cast<IModelable>().ToArray());
    }
    /// <summary>
    /// Calls individual category models' PreSaveAction method.
    /// </summary>
    public override void SaveAction()
    {
        foreach (IModelable model in SubModels)
        {
            try
            {
                model.SaveAction();
            }
            catch (Exception ex)
            {
                Debug.Log($"Error saving category {model.Identifier} for mod {Instance.Info.Name}: {ex.Message}", false, 2);
            }
        }
        OnModSaved?.Invoke();
    }
    public override void DiscardAction()
    {
        foreach (IModelable model in SubModels)
        {
            try
            {
                model.DiscardAction();
            }
            catch (Exception ex)
            {
                Debug.Log($"Error loading category {model.Identifier} for mod {Instance.Info.Name}: {ex.Message}", false, 2);
            }
        }
    }
    /// <summary>
    /// Subscribe to this event to run code after all the categories for the mod have been saved.
    /// This will only run if your mod is the currently selelcted mod.
    /// </summary>
    public event Action OnModSaved;
    public void RequestUpdateUI() => UI.RequestRefresh(this);
}
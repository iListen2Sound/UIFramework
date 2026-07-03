using UnityEngine;
namespace UIFramework.Models;

public abstract class CategoryModelBase : SelectableModelBase
{
    //public List<EntryModelBase> Entries => SubModels.Cast<EntryModelBase>().ToList();
    public ModModelBase ParentMod { get; set; }
    public override bool IsHidden { get; set; }
    protected CategoryModelBase(ModModelBase parentMod)
    {
        ParentMod = parentMod;
    }
    /// <inheritdoc/>
    public override GameObject GetNewUIInstance()
    {
        return GameObject.Instantiate(Prefabs.CatTab);
    }
    public virtual void AddEntry(params IEntry[] entryModel)
    {
        AddSubmodel(entryModel.Cast<IModelable>().ToArray());
    }
    //public override void DiscardAction() { }
}

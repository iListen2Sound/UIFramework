using UIFramework.Adapters;
namespace UIFramework.Models;

public abstract class EntryModelBase : ModelBase, IEntry
{
    public CategoryModelBase ParentCategory { get; set; }
    public EntryModelBase(CategoryModelBase parentCategory)
    {
        ParentCategory = parentCategory;
    }
    /// <inheritdoc/>
    public abstract string Description { get; }
    public override bool IsHidden { get; set; }

    /// <summary>
    ///
    /// </summary>
    public virtual EntryState SaveState { get; set; }

    public override void DiscardAction() { }


    #region UI Commands

    #endregion
}
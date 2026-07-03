namespace UIFramework.Models;

/// <summary>
/// Models that represent buttons on the sidebar and topbar
/// </summary>
public abstract class SelectableModelBase : ModelBase, IHoldSubmodels
{

    /// <summary>
    /// List of submodels for the model
    /// </summary>
    public virtual List<IModelable> SubModels { get; set; } = new();

    /// <summary>
    /// General submodel finder.
    /// </summary>
    public IModelable GetSubmodel(string name)
    {
        return SubModels.FirstOrDefault(m => m.Identifier == name);
    }

    /// <summary>
    /// Add a new submodel to the list
    /// </summary>
    public virtual void AddSubmodel(params IModelable[] submodel)
    {
        SubModels.AddRange(submodel);
    }

    /*public virtual void AddSubmodel(List<IModelable> submodels)
    {
        SubModels.AddRange(submodels);
    }*/

    /// <summary>
    /// Called when the discard button is pressed
    /// </summary>
    public override void DiscardAction()
    {

    }
    /// <inheritdoc/>
    public override void SaveAction() { }

}
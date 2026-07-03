using UnityEngine;
namespace UIFramework.Models;

public abstract class ModelBase : IModelable
{

    /// <inheritdoc/>
    public abstract string Identifier { get; }
    /// <inheritdoc/>
    public abstract GameObject GetNewUIInstance();
    /// <inheritdoc/>
    public abstract string DisplayName { get; }
    public virtual bool IsHidden { get; set; } = false;


    /// <inheritdoc/>
    public virtual void SaveAction()
    {

    }
    public virtual void DiscardAction()
    {
    }
}
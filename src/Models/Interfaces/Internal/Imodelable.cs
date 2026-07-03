using UnityEngine;
using UIFramework.Adapters;
namespace UIFramework.Models;
/// <summary>
/// Implemented by all models
/// </summary>
public interface IModelable
{
    /// <summary>
    /// DisplayName for the model
    /// </summary>
    public string Identifier { get; }
    /// <summary>
    /// User-facing display name. Should return Identifier if not assigned to a value
    /// </summary>
    public string DisplayName { get; }
    public bool IsHidden { get; set; }
    /// <summary>
    /// Instantiates a new Game object associated with them model
    /// </summary>
    /// <returns> UI Game Object</returns>
    public GameObject GetNewUIInstance();
    /// <summary>
    /// Should be called when save button is pressed. Runs after all ancestor's save actions have been run
    /// </summary>
    public void SaveAction();
    public void DiscardAction();
}
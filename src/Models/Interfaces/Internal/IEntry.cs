using UnityEngine;
using UIFramework.Adapters;
namespace UIFramework.Models;
/// <summary>
/// Goes on the main panel. Contains controls for manipulating preferences or just general UI controls
/// </summary>
public interface IEntry
{

    /// <summary>
    /// DisplayName/ID of the entry
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// DisplayName of the entry
    /// </summary>
    public string Description { get; }
    /// <summary>
    /// Ideally called by the controller to define a save action
    /// </summary>
    public void SaveAction();
    public void DiscardAction();
    public string DisplayName { get; }
    //public object ModelBoxedValue { get; set; }


}
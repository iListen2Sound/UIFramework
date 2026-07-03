using UnityEngine;
using UIFramework.Adapters;
namespace UIFramework.Models;
/// <summary>
/// Models that contain submodels. Generally these are mods and categories representing tabs
/// </summary>
public interface IHoldSubmodels : IModelable
{
    /// <summary>
    /// A list of submodels
    /// </summary>
    public List<IModelable> SubModels { get; set; }
    /// <summary>
    /// Finds submodel by identifier
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public IModelable GetSubmodel(string identifier);
}

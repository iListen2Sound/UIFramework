using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UIExtensions;

/// <summary>
/// Provide your own custom entry presentation.
/// You need to add a custom component that inherits from DataEntryAdapter
/// </summary>
public interface ICustomViewProvider : IUiExtension
{
    /// <summary>
    /// Prefab that gets instantiated by UI Framework. Make sure to assign a custom component that inherits from DataEntryAdapter to the prefab's root gameobject so the UI Framework can communicate with it.
    /// </summary>
    public GameObject EntryViewPrefab { get; set; }
}

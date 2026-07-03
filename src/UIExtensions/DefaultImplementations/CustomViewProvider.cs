using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UIExtensions;

///<inheritdoc cref="ICustomViewProvider"/>
public class CustomViewProvider : DefaultValidator, ICustomViewProvider
{
    public GameObject EntryViewPrefab { get; set; }
}
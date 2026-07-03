using UnityEngine;

namespace UIFramework.UiExtensions;

///<inheritdoc cref="ICustomViewProvider"/>
public class CustomViewProvider : DefaultValidator, ICustomViewProvider
{
    public GameObject EntryViewPrefab { get; set; }
}
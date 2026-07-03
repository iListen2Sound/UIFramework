using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UIExtensions;

///<inheritdoc cref="INumberBoxDescriptor"/>
public class NumberBoxDescriptor : DefaultValidator, INumberBoxDescriptor
{
    /// <inheritdoc/>
    public float Steps { get; set; } = 0;
    /// <inheritdoc/>
    public int DecimalPlaces { get; set; } = 1;

}
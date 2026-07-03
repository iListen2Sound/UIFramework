using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Preferences;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.UiExtensions;

/// <summary>
/// Default implementation of ISliderDescriptor. Used for numeric inputs that want to be sliders. DecimalPlaces defaults to 5, Min defaults to 0, Max defaults to 1.
/// </summary>
/// <see cref="ISliderDescriptor"/>
public class SliderDescriptor : DefaultValidator, ISliderDescriptor, IUserEditedNotifier
{
    /// <summary>
    /// Minimum value. Defaults 0
    /// </summary>
    public float Min { get; set; } = 0;
    /// <summary>
    /// Max value. Defaults 1.
    /// </summary>
    public float Max { get; set; } = 1;
    /// <summary>
    /// Decimal Places. Defaults 5
    /// </summary>
    public int DecimalPlaces { get; set; } = 1;
    /// <summary>
    /// Triggered when the slider value changes.
    /// </summary>
    public Action<float> OnSliderValueChanged { get; set; }

    ///<inheritdoc/>
    public Action<object> OnUserEdit { get; set;  }
}   
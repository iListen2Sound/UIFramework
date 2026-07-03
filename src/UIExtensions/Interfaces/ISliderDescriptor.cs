using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UiExtensions;

/// <summary>
/// Implementing this will present the entry as a slider in the UI
/// </summary>
/// <remarks>Released</remarks>
public interface ISliderDescriptor : IUiExtension
{
    /// <summary>
    ///
    /// </summary>
    public float Min { get; set; }
    /// <summary>
    ///
    /// </summary>
    public float Max { get; set; }
    /// <summary>
    ///
    /// </summary>
    public int DecimalPlaces { get; set; }
    /// <summary>
    /// Added in 0.10.3. Defualt implementation so it doesn't break existing implementations.
    /// 
    /// </summary>
    public Action<float> OnSliderValueChanged	
    {
        get => (value) => { };
        set { }
    }
}
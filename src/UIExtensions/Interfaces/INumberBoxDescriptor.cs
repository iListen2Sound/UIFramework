using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UIExtensions;

/// <summary>
/// Describes numeric up down controls
/// </summary>
/// <remarks>Released</remarks>
public interface INumberBoxDescriptor : IUiExtension
{
    /// <summary>
    /// 0 = default (1 for ints, 0.1 for floats).
    /// </summary>
    public float Steps { get; set; }
    /// <summary>
    ///
    /// </summary>
    public int DecimalPlaces { get; set; }
}
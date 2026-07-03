using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UIFramework.UIExtensions;


public interface IButtonDescriptor : IUiExtension
{
    public string ButtonText { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public Action Handler { get; set; }
}
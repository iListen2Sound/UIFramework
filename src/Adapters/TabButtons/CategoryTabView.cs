using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Logging;
using UnityEngine.Events;
using UnityEngine.UI;
using UIFramework.Models;
namespace UIFramework.Adapters;



/// <summary>
/// </summary>
[RegisterTypeInIl2Cpp]
public class CategoryTabView : TabButtonController
{
	protected override void OnSelect()
	{
		ParentWindow.SetSelectedCategory((CategoryModelBase)_internalModel);
	}
}


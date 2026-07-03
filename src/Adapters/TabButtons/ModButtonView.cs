using MelonLoader;
using UIFramework.Models;
namespace UIFramework.Adapters;

/// <summary>
///
/// </summary>
[RegisterTypeInIl2Cpp]
public class ModButtonView : TabButtonController
{

	protected MelonModel ModModel => (MelonModel)_internalModel;

	protected override void OnSelect()
	{

		ParentWindow.SetSelectedMod(ModModel);
	}

}
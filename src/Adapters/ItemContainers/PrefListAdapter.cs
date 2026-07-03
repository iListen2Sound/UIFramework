using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using static UIFramework.Debug;
using UIFramework.Models;

namespace UIFramework.Adapters;



/// <summary>
/// Main body of the UI. Lists individual preferences
/// </summary>
[RegisterTypeInIl2Cpp]

public class PrefListAdapter : ListAreaAdapterBase
{
	public CategoryModelBase SelectedCategory => Model as CategoryModelBase;
	/// <summary>
	/// When the save button is clicked, the selected category save action will be called. The model is now in charge of what that means
	/// </summary>
	public override void SaveAction()
	{
		SelectedCategory?.SaveAction();



	}
	/// <inheritdoc/>
	public override void DiscardAction()
	{
		SelectedCategory.DiscardAction();
		BuildFromModelList();
	}

}
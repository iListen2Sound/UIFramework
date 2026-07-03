using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Logging;
using UnityEngine.Events;
using UnityEngine.UI;
using UIFramework.Models;
namespace UIFramework.Adapters;

[RegisterTypeInIl2Cpp]
public abstract class TabButtonController : SubModelAdapter
{

	protected WindowCoordinator ParentWindow;
	protected IHoldSubmodels _model => (IHoldSubmodels)_internalModel;

	protected override void ModelSet()
	{
		Label = _model.DisplayName;
		ParentWindow = _rootWindow;
	}

	private string Label { set { gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; } }
	private ColorARGB TabColor { get; set; }
	/// <summary>
	/// Runs when the button is clicked. Implement this in inheriting classes.
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
	/// <remarks>IL2CPP does not like abstract methods 😭</remarks>
	protected virtual void OnSelect()
	{
		throw new NotImplementedException("Implement OnSelect in inheriting class");
		//this.gameObject.GetComponent<Image>().color = ParentWindow.openTabColor;
	}



	void Start()
	{
		gameObject.GetComponent<Button>().onClick.AddListener((UnityAction)OnSelect);
	}
	void OnDestroy()
	{
		gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
	}


}

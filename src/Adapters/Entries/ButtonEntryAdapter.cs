using Il2CppTMPro;
using MelonLoader;
using UIFramework.UiExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIFramework.Adapters;



[RegisterTypeInIl2Cpp]
public class ButtonEntryAdapter : DataEntryAdapter
{
	GameObject _buttonGo;
	Button _buttonComponent;
	IButtonDescriptor ButtonDescriptor => DataEntry?.UiExtension as IButtonDescriptor;
	protected override void DisplayData(object boxedValue)
	{
		_buttonGo = this.gameObject.transform.Find("Data/ButtonControl").gameObject;

		_buttonComponent = _buttonGo.GetComponent<Button>();
		_buttonComponent.onClick.AddListener((UnityAction)ButtonDescriptor?.Handler);
		TextMeshProUGUI buttonText = _buttonGo.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		buttonText.text = ButtonDescriptor?.ButtonText ?? "Button";
	}
}

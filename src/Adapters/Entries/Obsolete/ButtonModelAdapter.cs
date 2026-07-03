using MelonLoader;
using UIFramework.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIFramework.Adapters;


[Obsolete("Use ButtonEntryAdapter instead")]
[RegisterTypeInIl2Cpp]
public class ButtonModelAdapter : PrefEntryAdapterBase
{
	ButtonEntry ButtonModel => (ButtonEntry)EntryModel;
	public GameObject ButtonGo;
	/// <inheritdoc/>
	protected override void DisplayContents()
	{
		ButtonGo = this.gameObject.transform.Find("Data/ButtonControl").gameObject;
		ButtonGo.GetComponent<Button>().onClick.AddListener((UnityAction)OnClickRelay);
		base.DisplayContents();
	}

	public void OnClickRelay()
	{
		ButtonModel.OnClick?.Invoke(this);
	}

	void OnDestroy()
	{
	}
}
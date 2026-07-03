//using Il2CppSystem.Collections.Generic;
//using System.Collections.Generic;
//using static UI.UIFController;
using UIFramework.Models;
using UnityEngine;

namespace UIFramework.Adapters;


public abstract class SubModelAdapter : MonoBehaviour
{
	protected IModelable _internalModel;
	public IModelable Model
	{
		get
		{
			return _internalModel;
		}
		set
		{
			_internalModel = value;
			ModelSet();
		}
	}

	protected WindowCoordinator _rootWindow;

	protected WindowCoordinator FindRootWindow()
	{
		WindowCoordinator foundRoot = null; ;
		Transform ancestor = this.gameObject.transform.parent;
		while (ancestor != null)
		{
			if (ancestor.name.Contains("MainWindow"))
			{
				return ancestor.GetComponent<WindowCoordinator>();
			}
			ancestor = ancestor.parent;
		}

		return foundRoot;
	}
	void OnTransformParentChanged()
	{
		_rootWindow = FindRootWindow();
	}
	void Start()
	{
		_rootWindow = FindRootWindow();
	}
	protected virtual void ModelSet() { }
	protected virtual void DisplayMetadata() { }
}

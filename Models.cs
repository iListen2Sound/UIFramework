using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UIFramework.UIFController;

namespace UIFramework
{
	//TODO: Too much repitition Reinstate base model classq
	/// <summary>
	/// Models define how the UI is built. The heirarchy is simple and follows melonpreferences basic structure
	/// ModelMod ->  ModelMelonCategory -> ModelMelonEntry
	/// Modders can use the default model just by calling UIF.Register(modInstance, categories) in their OnLateInitializeMelon. 
	/// The default model will use simple input methods: bools will be toggles, strings will be text input fields and so would numerics.
	/// More options will eventually be available: sliders, dropdowns, multi checkboxes, radio buttons, etc.
	/// 
	/// Those will be developed after the default model is functional
	/// </summary>
	public class UIFModel
	{
		#region Interfaces
		/// <summary>
		/// Implemented by all models
		/// </summary>
		public interface IModelable
		{
			/// <summary>
			/// Identifier
			/// </summary>
			public string Identifier { get; }
			public string DisplayName { get; }
			/// <summary>
			/// Instantiates a new Game object associated with them model
			/// </summary>
			/// <returns> UI Game Object</returns>
			public GameObject GetNewUIInstance();
			/// <summary>
			/// Should be called when save button is pressed. Runs after all ancestor's save actions have been run
			/// </summary>
			public void SaveAction();
			/// <summary>
			/// Describes the parent for where the parent container should be
			/// </summary>
			//public UIPanel TargetParent { get; }
		}
		/// <summary>
		/// Models that contain submodels. Generally these are mods and categories representing tabs
		/// </summary>
		public interface IHoldSubmodels : IModelable
		{
			public List<IModelable> SubModels { get; set; }
			public IModelable GetSubmodel(string identifier);
		}
		/// <summary>
		/// Goes on the main panel. Contains controls for manipulating preferences or just general UI controls
		/// </summary>
		public interface IEntry : IModelable
		{
			public string Identifier { get; }
			public string Description { get; }
			/// <summary>
			/// If the 
			/// </summary>
			public void SaveAction();
			//public string DisplayName { get; }
			//public object BoxedValue { get; set; }


		}
		#endregion











		#region Abstracts
		public abstract class ModelBase : IModelable
		{

			public abstract string Identifier { get; }
			public abstract GameObject GetNewUIInstance();
			public abstract string DisplayName { get; }


			public virtual void SaveAction()
			{

			}
		}

		public abstract class SelectableModelBase : ModelBase, IHoldSubmodels
		{
			public virtual List<IModelable> SubModels { get; set; } = new();
			public IModelable GetSubmodel(string name)
			{
				return SubModels.FirstOrDefault(m => m.Identifier == name);
			}
		}

		public abstract class ModelModItem : SelectableModelBase
		{

			public override GameObject GetNewUIInstance()
			{
				return GameObject.Instantiate(Prefabs.ModTab);
			}

		}
		public abstract class ModelCategoryItem : SelectableModelBase
		{
			public override GameObject GetNewUIInstance()
			{
				return GameObject.Instantiate(Prefabs.CatTab);
			}
		}

		public abstract class ModelEntryItem : ModelBase, IEntry
		{
			public abstract string Description {get; }


		}

		#endregion













		public class RootModel : IHoldSubmodels
		{

			public virtual List<IModelable> SubModels { get; set; } = new();
			
			private string _name = string.Empty;
			
			public string Identifier => _name;
			public string DisplayName => _name;
			
			public void SetName(string name)
			{
				_name = name;
			}
			
			public void AddModModel(ModelMod mod)
			{
				SubModels.Add(mod);
			}

			public IModelable GetSubmodel(string name)
			{
				return SubModels.FirstOrDefault(m => m.Identifier == name);
			}
			public GameObject GetNewUIInstance() { return null; }

			public void SaveAction() { }

		}

		public class ModelMod : ModelModItem
		{
			//public List<IModelable> SubModels { get; set; } = new();
			public MelonMod Instance { get; set; }
			//public string ModName => Instance.Info.Identifier;

			public override string Identifier => Instance.Info.Name;
			public override string DisplayName => Identifier;

			//internal List<ModelBase> catModelList = new();


			public ModelMod(MelonMod instance, List<MelonPreferences_Category> catList)
			{
				Instance = instance;

				foreach (MelonPreferences_Category cat in catList)
				{
					SubModels.Add(new ModelMelonCategory(cat));
				}
			}


			public void AddSubmodel(IModelable model)
			{
				SubModels.Add(model);
			}

		}




		public class ModelMelonCategory : ModelCategoryItem
		{
			//public List<IModelable> SubModels { get; set; }
			public MelonPreferences_Category PrefCat;
			public override string Identifier => PrefCat.Identifier;
			public override string DisplayName => PrefCat.DisplayName.Trim() == "" ? PrefCat.Identifier : PrefCat.DisplayName;

			public ModelMelonCategory(MelonPreferences_Category cat)
			{
				PrefCat = cat;
				foreach (MelonPreferences_Entry entry in PrefCat.Entries)
				{
					SubModels.Add(new ModelMelonEntry(entry));
				}

			}

			public override void SaveAction()
			{
				PrefCat.SaveToFile();
			}

			public void AddSubModel(IEntry model)
			{
				SubModels.Add((IModelable)model);
			}

		}





		/// <summary>
		/// 
		/// </summary>
		public class ModelMelonEntry : IModelable, IEntry
		{
			public UIPanel TargetParent { get => UIPanel.EntryPanel; }
			public MelonPreferences_Entry PrefEntry;
			public string Identifier => PrefEntry.Identifier;
			public string DisplayName => PrefEntry.DisplayName.Trim() == "" ? PrefEntry.Identifier : PrefEntry.DisplayName;
			public virtual string Description => PrefEntry.Description;

			public object BoxedValue
			{
				get => PrefEntry.BoxedValue;
				set => PrefEntry.BoxedValue = value;
			}
			public ModelMelonEntry(MelonPreferences_Entry prefEntry)
			{
				PrefEntry = prefEntry;

			}

			private GameObject _uiPrefabSource;
			/// <summary>
			/// Use this function to provide your own prefab for this entry. 
			/// The prefab must have a component that implements IUIFrameworkEntry and properly handles the value changes and saving. 
			/// If no prefab is provided, a default one will be used based on the type of the preference 
			/// (bools will be toggles, strings will be text input fields and so would numerics).
			/// 
			/// </summary>
			/// <param name="prefab"></param>
			public void SetUIPrefabSource(GameObject prefab)
			{
				_uiPrefabSource = prefab;
			}


			/// <summary>
			/// Returns an instance of the game object associated with the MelonPreferences_Entry type.
			/// If a custom one is provided, it will return an instance of that instead
			/// </summary>
			/// <returns></returns>
			public GameObject GetNewUIInstance()
			{
				if (_uiPrefabSource == null)
				{

					switch (PrefEntry.BoxedValue)
					{
						case bool:
							return UIFramework.GetPrefab(InputType.Toggle);
							break;
						case string:
							return UIFramework.GetPrefab(InputType.TextField);
							break;
						case int:
							return UIFramework.GetPrefab(InputType.NumericInt);
						case float:
							return UIFramework.GetPrefab(InputType.NumericFloat);
						case double:
							return UIFramework.GetPrefab(InputType.NumericDouble);
						default:
							Debug.Log("Unsupported type detected with no custom widget prefab provided. Defaulting to text input. Creating custom component recommended", false, 1);
							return UIFramework.GetPrefab(InputType.TextField);


					}
				}
				else
				{
					return GameObject.Instantiate(_uiPrefabSource);
				}
			}

			public void SaveAction()
			{

			}


		}










		#region customs
		public class ButtonEntry : IModelable, IEntry
		{

			private string _name;
			public string Identifier => _name;

			private string _description;
			public string Description => _description;

			private string _displayName;
			public string DisplayName => _displayName;
			/// <summary>
			/// This is only to satisfy the contract for IEntry. 
			/// </summary>
			public object BoxedValue { get; set; }

			public void SaveAction() { }

			public Action<IEntry> OnClick;
			public ButtonEntry(string name, string description = "", string displayName = "")
			{
				_name = name;
				_description = description;
				_displayName = displayName;
			}

			public GameObject GetNewUIInstance() => UIFramework.GetPrefab(InputType.Button);
			public virtual void OnClickRelay()
			{
				OnClick?.Invoke(this);
			}



		}
		#endregion







#pragma warning disable CS1591
		public enum UIPanel
		{
			Window,
			Sidebar,
			Topbar,
			EntryPanel,
		}
	}

}

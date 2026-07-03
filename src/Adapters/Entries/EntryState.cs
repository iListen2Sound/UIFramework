using Tomlet;
using Tomlet.Models;
using UIFramework.Models;
using UIFramework.UiExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIFramework.Adapters;


public enum EntryState
{
	Untouched,
	Edited,
	Saved,
	Errored,

}
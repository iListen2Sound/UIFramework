using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.UI;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace UIFramework
{
	public class CustomCategoryTab
	{
        internal UIFModel.ModelCategoryItem CategoryModel {get; set;}
        public readonly List<CustomUIEntry> Entries = new List<MelonPreferences_Entry>();

        public string Identifier { get; internal set; }
        public string DisplayName { get; set; }
        public bool IsHidden { get; set; }
        public bool IsInlined { get; set; }
    
        internal CustomCategoryTab(string identifier, string display_name, bool is_hidden = false, bool is_inlined = false)
        {
            Identifier = identifier;
            DisplayName = display_name;
            IsHidden = is_hidden;
            IsInlined = is_inlined;
        }

        public CustomUIEntry<T> CreateEntry<T>(string identifier, T default_value, string display_name = null, string description = null, bool is_hidden = false, bool dont_save_default = false, Preferences.ValueValidator validator = null, string oldIdentifier = null)
        {
                 if (string.IsNullOrEmpty(identifier))
                throw new Exception("identifier is null or empty when calling CreateEntry");

            if (display_name == null)
                display_name = identifier;

            var entry = GetEntry<T>(identifier);
            if (entry != null)
                throw new Exception($"Calling CreateEntry for { display_name } when it Already Exists");

            if (validator != null && !validator.IsValid(default_value))
                throw new ArgumentException($"Default value '{default_value}' is invalid according to the provided ValueValidator!");

            if (oldIdentifier != null)
            {
                if (HasEntry(oldIdentifier))
                    throw new Exception($"Unable to rename '{oldIdentifier}' when it got already loaded");

                RenameEntry(oldIdentifier, identifier);
            }

            entry = new CustomUIEntry<T>
            {
                Identifier = identifier,
                DisplayName = display_name,
                Description = description,
                IsHidden = is_hidden,
                DontSaveDefault = dont_save_default,
                Category = this,
                DefaultValue = default_value,
                Value = default_value,
                Validator = validator,
            };

            Preferences.IO.File currentFile = File;
            if (currentFile == null)
                currentFile = MelonPreferences.DefaultFile;
            currentFile.SetupEntryFromRawValue(entry);

            Entries.Add(entry);

            return entry;
        }
    }
}
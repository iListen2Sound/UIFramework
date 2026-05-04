# UI Framework API overview
UI Framework builds a UI for modders' configuration for their mods. Particularly MelonPreferences but hopefully expanded to more
## MelonPreferences
MelonPreferences are the built-in configuration and persistent storage system in MelonLoader. 

<details><summary>Expand to see how to initialize and use MelonPreferences</summary>
### Categories
Categories are a base MelonPreferences feature to group entries together. 
#### Variable declarotion
```cs 
private MelonPreferences_Category TestCategory1;
private MelonPreferences_Category TestCategory2;
```
#### Intantiation
You can have make your categories have a different display name to its actual identifier
Please prefix your category identifiers with your mod name

```cs
//* Please prefix your categories with your mod's name
TestCategory1 = MelonPreferences.CreateCategory("MyMod_TestCat1", "DisplayName 1");
TestCategory2 = MelonPreferences.CreateCategory("MyMod_TestCat2", "DisplayName 2");
```
#### File path customization
You can assign each category its own filepath.
```cs
private const string USER_DATA = "UserData/MyMod/";
private const string CONFIG_FILE = "config.cfg";
...
//Make sure the directory exists before setting the file path
if (!Directory.Exists(USER_DATA))
    Directory.CreateDirectory(USER_DATA);

TestCategory1.SetFilePath(Path.Combine(USER_DATA, CONFIG_FILE));
TestCategory2.SetFilePath(Path.Combine(USER_DATA, CONFIG_FILE));
```
<details><summary> A note on custom file paths</summary>
Please make sure the directory exists before setting the file path. If the directory doesn't exist, it will fail silently and your settings won't save with no explanation
</details>

### Entries
Entries are the actual settings that you want to save. 
#### Variable declaration
```cs
private MelonPreferences_Entry<string> TestEntry11;
private MelonPreferences_Entry<int> TestEntry12;

private MelonPreferences_Entry<float> TestEntry21;
private MelonPreferences_Entry<bool> TestEntry22;
```
#### Entry Creation 
```cs
TestEntry11 = TestCategory1.CreateEntry("Entry 1-1", "Test Val", "Display Name1", "Test String");
TestEntry12 = TestCategory1.CreateEntry("Entry 1-2", 1, "Display Name2", "Test Int");

TestEntry21 = TestCategory2.CreateEntry("Entry 2-1", "0.5126", "Display Name 3", "Test float");
TestEntry22 = TestCategory2.CreateEntry("Entry 2-2", true, "Display Name 4", "Test bool");
```

#### Acessing and modifying values:
Entries are objects. You need to access their Value property to get or set the actual value. 
```cs
string value1 = TestEntry11.Value;
TestEntry11.Value = "New Value";
```
</details>

## UI Framework
### Registration

Once you have your MelonPreferences set up, registering to UI Framework is simple.
Just call 
```cs
UI.RegisterMelon(MelonBase modInstance, params MelonPreferences_Category[])
```
So `UI.RegisterMelon(this, TestCategory1, TestCategory2);`
if you're calling it from your main MelonMod class.

Your categories will display as tabs in the order that you put them in the parameter array 
and your entries will show in the order thatw you created them.


**For most usage, this will be enough.**
The UI will automatically present your entries according to their data types. Bools will show as toggles, enums will show as dropdowns, strings and numbers will show as text inputs with the appropriate filters, etc.

## UI Presentation control: Validator Extensions

UI Framework piggybacks off of the existing MelonPreferences custom validator system. 
You can influence how the UI is presented by using custom validator classes that implement certain interfaces.

<details><summary>  <strong>MelonPreferences custom validator system details</strong> </summary>

CreateEntry takes an optional parameter for a custom validator
```cs 
CreateEntry(string identifier, T default_value, string display_name = null, string description = null, bool is_hidden = false, bool dont_save_default = false, Preferences.ValueValidator validator = null)
```

You can make your own custom validator by inheriting the ValueValidator class.

```cs 
public class CustomValidator : ValueValidator
{
    //These two are required members. If you don't care about validation, you can just always say that the passed object is valid
    public override bool IsValid(object value)
    {
        return true;
    }
    public override object EnsureValid(object value)
    {
        return value;
    }
}
```
But UI Framework already has its own default implementation that validates everything in the `UIFramework.ValidatorExtensions` namespace as `DevaultValidator`.
</details>

-----

<details><summary><strong>UI Framework Validator Extensions details</strong></summary>


</details>

### Implemented Extensions
#### Sliders
##### Interface: `ISliderDescriptor`
##### Default extended validator: `SliderDescriptor`
The UI will represent your entry with a slider if you add a validator that implements `SliderDescriptor`.


```cs
MySlider = Category.CreateEntry("MySlider", 0.5f, "My Slider", "Float Slider",false, false, new SliderDescriptor { Min = 0, Max = 1, DecimalPlaces = 3 });
```


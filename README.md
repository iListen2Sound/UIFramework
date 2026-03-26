# For Users
Drop the dll in your mod folder. Default toggle is the `F9` key

# For Modders 
Declare your MelonPreferences and then register to UI in `OnLateInitializeMelon();` with 
```cs
UI.Register(this, TestCategory1, TestCategory2...);
```

## If you haven't used melonpreferences before
Below is a link to the documentation as well as instructions for basic usage.
**<details><summary> Standard Melon preferences declaration example </summary>**
https://melonwiki.xyz/#/modders/preferences?id=melon-preferences
1. Set a file location. Make sure the directory exists for your mod because it will not error but your preferences don't save at all.
```cs
private const string USER_DATA = "UserData/TestMod/";
private const string CONFIG_FILE = "config.cfg";
if (!Directory.Exists(USER_DATA))
    Directory.CreateDirectory(USER_DATA);
```
2.  Declare, create, and set a file path for your categories
```cs
private MelonPreferences_Category TestCategory1;
TestCategory1 = MelonPreferences.CreateCategory("Test Cat 1");
TestCategory1.SetFilePath(Path.Combine(USER_DATA, CONFIG_FILE));

private MelonPreferences_Category TestCategory2;
TestCategory2 = MelonPreferences.CreateCategory("Test Cat 2");
TestCategory2.SetFilePath(Path.Combine(USER_DATA, CONFIG_FILE));
```
3. Declare your entries.
```cs
private MelonPreferences_Entry<string> TestEntry11;
private MelonPreferences_Entry<int> TestEntry12;

private MelonPreferences_Entry<float> TestEntry21;
private MelonPreferences_Entry<bool> TestEntry22;
```
4. Create Entries by calling the .CreateEntry method on the category they go in
```cs
TestEntry11 = TestCategory1.CreateEntry("Entry 1-1", "Test Val", "Display Name1", "Test String");
TestEntry12 = TestCategory1.CreateEntry("Entry 1-2", 1, "Display Name2", "Test Int");

TestEntry21 = TestCategory2.CreateEntry("Entry 2-1", "0.5126", "Display Name 3", "Test float");
TestEntry22 = TestCategory2.CreateEntry("Entry 2-2", true, "Display Name 4", "Test bool");
```
</details>

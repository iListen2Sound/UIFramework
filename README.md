<sup><sup> btw: The changelog doubles as a feature list </sup></sup>
### New in 0.10.1
<details><summary>New feature: Made resize handles visible</summary>

- Right handle for scale
- Bottom handle to stretch downwards

</details>

<details><summary>Bug fix: Nullref error when the player doesn't have ModUI</summary>
Wasn't really motivated to be rigorous because everybody had ModUI😅
</details>

<details><summary>Bug fix: Continuous refresh error</summary>
Fixed the refresh method to not keep trying every frame when a part of the UI fails to build
</details>

### New in 0.10.0

<details><summary>New Feature: Window is now resizable</summary>
You can now scale the window or adjust its height. 

- Scale: Click and drag the right edge to make the window scale uniformly.
- Height: Click and drag the bottom edge to adjust the height of the window. 

</details>

<details><summary>New Feature: Increment and Decrement buttons in number fields </summary>
You can now click on buttons to increment or decrement values in number fields.

- Integral types like ints, bytes, longs, etc. will increment and decrement by 1 by default.
- Floating point types like floats and doubles will increment and decrement by 0.1 by default.

<sup>Note: This change has made floating point errors more obvious. I've opted to limit floating point types to display
1 decimal place by default. This can be easily overridden with the `INumberBoxDescriptor` UI Extension class.</sup>
</details>


<details><summary>New Feature: Advanced UI Customization for modders </summary>

You can now use the `ICustomViewProvider` interface to be able to pass your own custom representations to the UI Framework.
Default implementation: `CustomViewProvider`. More details in [CustomUI](https://github.com/iListen2Sound/UIFramework/blob/main/CustomUI.md)
</details>



# For Users
Drop the dll in your mods folder. 
### **Default toggle is the `F9` key**


~~Changing a value of an entry automatically updates the value of the preference and is applied. How that preference's parent mod reacts depends on the modder's implementation.~~

<sup>* the above is no longer true as of 0.8.0. Values will not update until the saved button has been clicked</sup>


The save button writes it to the file for permanent storage. Closing your game might also save preferences to file automatically depending on whether it's closed from the game window or through Steam. **Stopping through Steam doesn't save because it force closes it.**

------

# For Modders 

A (more) detailed API Overview exists over at https://github.com/Reverb-And-Spice/UIFramework/blob/main/API_OverView.md
## Basic Registration
Add `[assembly: MelonAdditionalDependencies("UIFramework")]` to your AssemblyInfo. This prevents your mod from calling on UIFramework before it's been initialized.

[Define](#If-you-havent-used-melonpreferences-before) your MelonPreferences in `OnInitializeMelon` and then register them to the UI.
```cs
UI.Register(this, TestCategory1, TestCategory2...);
```
~~Right now, support is limited to common types like `string`, `int`, `bool`, `double`, `float`, and `enums` without the flags attribute. Working on expanding this.~~

### Type Support: Whatever works with Tomlet
<details> <summary>Expanded Type Support details</summary>

Support is no longer limited to the types mentioned above. Serialization and parsing is now handled by [Tomlet](https://github.com/SamboyCoding/Tomlet). 
This means that it supports types described in [Toml 1.0.0](https://toml.io/en/v1.0.0) and whatever Tomlet supports. [You can even make your own custom mappers](https://github.com/SamboyCoding/Tomlet/blob/master/README.md#creating-your-own-mappers)

Caveat: Types handled by Tomlet will be presented as regular text inputs and they might not always look good. Numerics will have the appropriate filters.
I do plan to continue expanding the number of custom UI presenters like I did with enums and booleans.
 </details>


### Optional: OnSave Event Handler
You can add an event handler that gets called when the save button is clicked while your mod is selected.
```cs
private void MyModSaved()
{
    // Do something when the save button is clicked while your mod is selected
}
```
```cs
UI.Register((MelonBase)this, OBSAutoRecorderSettings, TestCategory1, TestCategory2...).OnModSaved += MyModSaved;
```
<sup>Casting to melonbase isn't necessary but it forces your compiler to use the newer MelonBase registration instead of the obsolete MelonMod registration
In the future, all mods will be registered as MelonBase by default and the cast won't be needed. 
But the cast makes sure that your mod won't break when the old MelonMod registration gets removed</sup>


### Optional: Custom display names


Add `[assembly: UIInfo("My Mod's Better\nDisplay Name")]` to your assembly attributes to change how the mod's name is displayed
in the UI. Line breaks are supported.

-----

# Advanced Usage
I moved this section the [API Overview](https://github.com/Reverb-And-Spice/UIFramework/blob/main/API_OverView.md#ui-presentation-control-validator-extensions)


-----

## If you haven't used melonpreferences before

I detail usage and creation here: 
https://github.com/Reverb-And-Spice/UIFramework/blob/main/API_OverView.md#melonpreferences

And the official docs are here: 
https://melonwiki.xyz/#/modders/preferences?id=melon-preferences





# Ongoing Development Disclosure
This mod is in active development. The plan is to increase extensibility. 
**<ins>~~Basic MelonPreferences registration is stable and should always be backwards compatible.~~</ins>** 
So while advanced API usage will have a lot of changes for the time that this mod is in [Version 0.x.x](https://semver.org/#spec-item-4),
mods that implement the basic use case of this framework don't have to worry about breaking in the future (as long as I don't mess up too bad). 

### Oops (0.6.2)
Well, so much for always be backwards compatible. In order to support plugins, I'm having to change .Register to use MelonBase as the instance instead of MelonMod
Currently, I have an obsolete bridging function for backwards compatibility. 
But that will be removed in a future version if I'm confident that enough mods have migrated that it won't be too big of a problem. 

In order to make your mod future proof, explicitly cast your MelonMod instance to MelonBase in your next update. 
You don't need to publish that update now for this small change but it makes it so that when the old function does become actually deprecated, you won't need to push an update specific to it.

```cs
UI.Register((MelonBase)this, TestCategory1, TestCategory2);
```

Okay, so for real this time: **<ins>Basic MelonPreferences registration is stable and should always be backwards compatible.</ins>** 

### XML Documentation File
You can place the .xml documentation file for UIFramework in the same folder as the dll to get intellisense documentation for the API. 
It is currently incomplete, however but I do add to it every update.
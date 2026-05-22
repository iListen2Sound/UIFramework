# Advanced UI Customization
## Creating Custom Views

### Entry View
Each representation of an entry in the UI is called an `EntryView`. 
In general, it is a Unity UI panel that contains labels for the `DisplayName`,and `Description`. 
It also contains a `Control` element that's used to actually interact with the data. 
This could be a TextInput, a Toggle, etc. whichever might be appropriate for the data type being represented. 

UI Framework has some built in ones that cover the most common data types and will default to TextInput for things it doesn't know how to handle 
and hope that the entry can be serialized and deserialized using TOML.

![Basic text input view](_Misc/Images/EntryEXample.png)

### Entry Model
Won't go into too much detail here because I haven't finalized the design for this class yet.
But this is the part of the program that actually interacts with MelonPreferences. 

An `EntryAdapter` interacts with the model to retrieve the data and present it to the user through the view
then takes the user's input and submits it back to the model which updates the MelonPreference Entry value

### EntryAdapters
This is a custom component added to the `Entry View` root panel. 

#### Name and Description Labels
These are the game objects that should display the name and the description of the entry.
By default the `DescriptionText` and `DisplayName` properties reference a GameObjct called `"Description"` and `"Data/Label"` 
respectivey in your view prefab's hierarchy. But both properties can be overridden.
#### Data Hooks
These are the methods for moving data between the model and the view.
`DisplayMetadata()` - This method is called first when the view is being built. this is what assigns 
the name and the description to the appropriate labels. 

`DisplayData(object boxedValue)` - This method is called after `DisplayMetadata` and passes the value from the model to the Adapter. 
Override this method to interpret the boxed value and display it in the control the user interacts with for this entry of your mod

`SubmitValue(object value)` - Call this method when you want the user input to be passed to the model. Unlike the other methods, this can't be overridden.

`PreSaveAction()` - Called right after the user clicks the save button before any saving actually happens. Gives you a last chance to call `SubmitValue` to make sure the user's entry has been submitted
to the model by the time its value gets saved. 

<details> <summary> Current (summmary from memory)</summary>
# Abstract Model
- Identifier
- DisplayName
- IsHidden

- GetUIInstance()

- Abstract public OnSave()
- Abstract public OnDiscard
# ICointainable
- (IContainer)Parent

# IContainer
- List<IContainable> Submodels

# RootModel : Model, IContainer
- Mods => Submodels as List<ModModel>
- Save()
    OnSave()
    loop Mods.OnRootSave()
- Discard()
    loop Mods.OnRootDiscard()
    OnDiscard()

- override public OnSave()
- override public OnDiscard()
## ModModel : Model, IContainer, IContainable
- Categories => Submodels as List<Category>
- public OnRootSave()
    OnSave()
    loop Categories.OnModSave()
- public OnRootDiscard()
    loop Categories.OnModDiscard
    OnDiscard()

- override public OnSave()
- override public OnDiscard()

### Abstract Category : Model, IContainer, IContainable
- Entries => Submodels as List<Entry>
- public OnModSave()
    loop Entries.OnCategorySave()
    OnSave()
- public OnModDiscard()
    OnDiscard()
    loop Entries.OnCategoryDiscard()

### MelonCategory : Category
- private MelonPreferences_Category SourceCategory
- Override OnSave()
    SourceCategory.SaveToFile()
- Override OnDiscard()
    SourceCategory.LoadFromFile()

#### Abstract Entry : Model, IContainable
- public OnCategorySave()
    OnSave()
- public OnCategoryDiscard()
    OnDiscard()

#### Abstract DataEntry : Entry
- Abstract protected EntryValue {get; protected set;}
- Abstract public SetData(object value)
#### MelonEntry : DataEntry
- private MelonPreferences_Entry DataSource
- Override EntryValue {get => DataSource.BoxedEditedValue ; protected set => DataSource.BoxedEditedValue = value;}
- Override public SetData(object data)
    EntryValue = data;
- Override OnSave()
    //Handled by MelonPreferences_Category.SaveToFile()
- Override OnDiscard()
    //ResetEditedValue in data source which stays untouched during LoadFromFile()
    EntryValue = DataSource.Value 

</details>

-----

# Settings Model Architecture (Proposed)

This structure models settings as a tree of nodes with:
- explicit lifecycle flow (Save / Discard)
- backend-agnostic persistence
- explicit refresh scoping via Mod ownership
- minimal, intentional traversal

---

## Core Lifecycle

### ILifecycle
- Save()
- Discard()

Rules:
- Save propagates **children → self**
- Discard propagates **self → children**

---

## Tree Structure

### Abstract SettingsNode : ILifecycle
- Identifier
- DisplayName
- IsHidden

- GetUIInstance()

- public virtual Save()
    calls OnSave()

- public virtual Discard()
    calls OnDiscard()

- protected virtual OnSave()
- protected virtual OnDiscard()

Notes:
- No parent traversal logic here
- No backend knowledge here
- This is the base identity + lifecycle node

---

### Abstract ContainerNode : SettingsNode
- IReadOnlyList<SettingsNode> Children

- override Save()
    foreach child in Children:
        child.Save()
    OnSave()

- override Discard()
    OnDiscard()
    foreach child in Children:
        child.Discard()

Notes:
- Encapsulates traversal logic
- Lifecycle order is consistent and centralized

---

## Refresh Infrastructure

### IRefreshDispatcher
- RequestRefresh(ModNode source)

Notes:
- Owned by Root / MainWindowAdapter
- Coalesces, filters, and schedules refreshes
- Model code never talks to UI directly

---

## Root Level

### RootNode : ContainerNode
- Mods => Children as IReadOnlyList<ModNode>
- RefreshDispatcher : IRefreshDispatcher

- override OnSave()
    // Optional global hooks

- override OnDiscard()
    // Optional global hooks

Notes:
- Root is the aggregation point
- Does NOT receive refresh requests directly from entries

---

## Mod Level (Refresh Scope)

### ModNode : ContainerNode
- Categories => Children as IReadOnlyList<CategoryNode>
- private IRefreshDispatcher Dispatcher

- constructor(dispatcher)
    Dispatcher = dispatcher

- public RequestRefresh(SettingsNode source)
    Dispatcher.RequestRefresh(this)

- override OnSave()
    // Optional mod-level hooks

- override OnDiscard()
    // Optional mod-level hooks

Notes:
- ModNode is the **refresh boundary**
- Entries never traverse upward: they call their owning Mod
- UI can filter refreshes per mod cleanly

---

## Category Level (Backend Boundary)

### CategoryNode : ContainerNode
- Entries => Children as IReadOnlyList<EntryNode>
- Backend : ISettingsBackend

- override OnSave()
    Backend.SaveCategory(this)

- override OnDiscard()
    Backend.LoadCategory(this)

Notes:
- Category owns persistence boundaries
- Backends are responsible for value I/O, not structure

---

## Backend Abstraction

### ISettingsBackend
- void SaveCategory(CategoryNode category)
- void LoadCategory(CategoryNode category)

- object GetEntryValue(EntryNode entry)
- void SetEntryValue(EntryNode entry, object value)

Notes:
- Backends never mutate the node structure
- Backends may ignore EditedValue semantics if unsupported

---

## Entry Level

### Abstract EntryNode : SettingsNode
- EditedValue : object
- private ModNode OwningMod

- constructor(mod)
    OwningMod = mod

- public SetEditedValue(object value)
    EditedValue = value
    OwningMod.RequestRefresh(this)

- override OnSave()
    // usually no-op (handled by category backend)

- override OnDiscard()
    // usually backend-driven

Notes:
- EntryNode knows **only** its owning Mod for refresh
- No parent traversal required
- Backend events can safely trigger refresh through OwningMod

---

## MelonPreferences Implementation Example

### MelonEntryNode : EntryNode
- DataSource : MelonPreferences_Entry

- constructor(mod, dataSource)
    EditedValue = dataSource.BoxedEditedValue
    subscribe to DataSource.OnEditedValueChanged

- OnEditedValueChanged(newValue)
    EditedValue = newValue
    OwningMod.RequestRefresh(this)

- override OnDiscard()
    EditedValue = DataSource.Value

Notes:
- Model-side subscription to MelonPreferences events
- UI is updated indirectly through refresh requests
- No adapter logic required for backend signals

---

## Design Guarantees

- Tree traversal is **structural only**
- Refresh routing is **explicit and scoped**
- Backends persist values, not structure
- Model never talks directly to UI
- UI never guesses model intent

---

## Mental Model

> Structure flows downward.  
> Lifecycle flows recursively.  
> Refresh responsibilities flow explicitly through Mod nodes.
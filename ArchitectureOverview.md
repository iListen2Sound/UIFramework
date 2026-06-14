# UI Framework Architecture Overview
# MVA(-ish) Design Pattern
UI framework keeps the UI and the data separate by applying a design pattern inspired by the model-view-adapter pattern

- ***Views***: In UI Framework's case, these are the game objects that serve as UI elements the user interacts with. 

- ***Adapters***: These are custom game object components added to the views that control how the view presents the data to the user.

- ***Models***:  These are wrapper classes around the data (currently only MelonPreferences) that provide a common interface for the adapters to interact with. 

This separation allows for UI Framework to be customizable and expandable. 

Expandability can be anything from using the [Advanced UI Customization Features](https://github.com/iListen2Sound/UIFramework/blob/main/CustomUI.md#making-custom-entryadapters) to  

## Component-Based Design
Views (with their adapters) in UI Framework are designed to be self-contained  

## 


### Models
### Adapters
### Views

## Bonus: UI Extension System

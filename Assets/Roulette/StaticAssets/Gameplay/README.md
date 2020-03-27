# Gameplay API Documentation

## Constructors
`GameplayApi()` _Default constructor to create a GameplayApi object_

## Methods
Name | Return type | Description
--- | --- | ---
Initialise | `Promise` | This method should be called in the beginning to initialize the API. Should be called after object creation.
GetPlayerBalance | `Promise<Int64>` | Use this method to get Player's balance.
GetInitialWin | `Promise<Int32>` | Use this method to get the Initial Win value, that should be multiplied after spin.
GetMultiplier | `Promise<Int32>` | Use this method to get the value of the Multiplier.
SetPlayerBalance | `Promise` | Use this method to set Player's balance.
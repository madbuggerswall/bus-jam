## LevelEditorScene

### LevelGrid

- Select a cell and press **Z, X, C, V, or B** to spawn a `Passenger` prefab (Key–prefab mappings can be configured via `KeyPrefabMapDefinition` under **Data/LevelEditor**):
  - **Z** → Default
  - **X** → Reserved
  - **C** → Secret
  - **V** → Cloak
  - **B** → Rope
- While a cell **containing a** `Passenger` is selected, press **0–9** to set the passenger color (Key–color mappings can be configured via `KeyColorMapDefinition` under **Data/LevelEditor**):
  - **1** Blue, **2** Brown, **3** Cyan, **4** Green, **5** Orange,
    **6** Pink, **7** Purple, **8** Red, **9** White, **0** Yellow
- While a cell **containing a** `Passenger` is selected, press **Backspace** to delete the passenger.
- While a cell **without a** `Passenger` is selected, press **P** to toggle the cell as empty.

---

### BusGrid

- Select a cell and press **Z** to spawn a `EditorBus`.
- Press **R** to toggle the reserved passenger capacity (**0 → 1 → 2 → 3**).
- While a cell **containing a** `EditorBus` is selected, press **0–9** to set the bus color:
  - **1** Blue, **2** Brown, **3** Cyan, **4** Green, **5** Orange,
    **6** Pink, **7** Purple, **8** Red, **9** White, **0** Yellow

---

### LevelEditorPanel

- Applying a new `LevelGrid` size or `BusGrid` size will despawn all existing elements.
- Press **Save** to store the level as a `ScriptableObject`.
- Press **Load** to load an existing `LevelDefinition` asset.

## LevelPlayScene

`LevelLoader` loads the assigned level or retrieves one from the `LevelPackManager`.
`LevelPackManager` selects a level based on persistent player data.
Any level can be tested by assigning a `LevelDefinition` directly to the `LevelLoader`.
`PlayerData` can be deleted via the `ContextMenu` under **LevelPlayContext/PersistenceManager**.

## Frolics Submodule

This submodule consists of common utilities I have implemented over time, including:

- `Contexts` (a DI/Service Locator love-child)
- `Tweens `(a tween system),
- `Grids`(a library for easy `SquareGrid` and `HexGrid` usage),
- `Input `(mobile and standalone input handling),
- `SignalBus`
- `CameraSystem `(a modular camera system)
- Some editor tools (_e.g._ `ManagedReferenceDrawer`)
- Some common utilities (_e.g._ `SaveManager`)

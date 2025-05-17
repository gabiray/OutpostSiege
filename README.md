# 🛠 Game Development Convention

> **Follow these rules when developing this project to ensure consistency and clarity across all assets and code.**

---

## 📁 Unity Object Naming
- Use **Capital Letters** and **spaces** if needed.  
  **Examples**:  
  - `Player`  
  - `Main Menu`  
  - `Enemy Tower`

---

## 📄 Script Naming
- Use **`Camel_Case_With_Underscore`**.  
  **Examples**:  
  - `Main_Menu_Manager.cs`  
  - `Tree_Interaction.cs`  
  - `Game_Controller.cs`

---

## 🧾 Commenting Style
- Always write **English comments**, with a **space after `//`**.  
  **Example**:
  ```csharp
  // Move the player forward
  ```

---

## 🧠 Function Naming
- Functions should use **PascalCase** (each word capitalized, no spaces).  
  **Examples**:  
  - `PlayGame()`  
  - `CutTree()`  
  - `UpdateUI()`

---

## 🗂 Folder Organization
- Every object/script/asset must go in its **own folder** in `Assets/`.
- Folder names must be in **Capital Letters**, with spaces if needed.  
  **Examples**:  
  - `UI Elements`  
  - `Player`  
  - `Audio Clips`

---

## 🔒 Code Structure & Fields
- All values that may need modification in Inspector must be marked as:
  ```csharp
  [SerializeField] private int health;
  ```

---

## 🚫 Restrictions
- **No special characters** in names (code, folders, files).
- Use **only alphanumeric characters** and underscores for code.

---

## 🌿 Git Workflow
- The `main` branch is always the **working version**.
- Developers must **pull the latest version** before pushing changes.
- Each update must be clearly written in the GitHub title:
  ```
  UPDATE (What you did)
  ```
- Detailed changes must follow in the description:
  ```
  - Short and clear point
  - Another short point
  ```

---

## 🔢 Version Naming
- If you worked on an outdated version but still pushed it, append a **b**:
  ```
  0.01b // based on version 0.01, but added after main updated
  ```

---

## ✅ Summary Checklist

| Area            | Convention                                      |
|-----------------|--------------------------------------------------|
| Unity Object    | Capitalized with spaces                         |
| Scripts         | Pascal_Case_With_Underscore.cs                  |
| Functions       | PascalCase()                                    |
| Comments        | // English comment with leading space           |
| Serialized Vars | `[SerializeField]` if configurable              |
| Folders         | Capitalized names, organized by type            |
| Git Updates     | `UPDATE (Title)` + bullet point description     |
| Versions        | Use `.b` if based on older main version         |
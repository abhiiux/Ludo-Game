# 🎲 Ludo Game (Unity 2D)  
**A assignment submission by [Abhidev Ramesh]** | **[https://abhidevramesh.framer.website/]**  

[Screenshots]  
<img width="367" alt="Screenshot 2025-06-25 at 4 43 05 PM" src="https://github.com/user-attachments/assets/6ccabfcd-1b04-4c49-ae7e-89fbe60e777e" />

## 🏆 **Key Highlights** *  
- **Architecture**: Implemented turn management and movement logic using **modular C# scripts**  
- **UX**: Designed intuitive click-based controls (no complex keybindings).  
- **Optimization**: Avoided Update() overuse with **event-driven logic** (mention if applicable).  

## **Code Structure**  
```plaintext
Assets/
├── Scripts/
│   ├── Dice.cs                   // Handles RNG and roll visualization
│   ├── PlayerController.cs       // Manages pieces and turn logic
│   └── TeamScript.cs             // Moves pieces according to input.
|         --PlayerScript.cs       // Tracks position and steps
|            ---TileScript.cs     // Handles Canceling Logic
└── Sprites/                      
```
## 🎮 How to Play & Test

### 🚀 Quick Start
-Assets/Scenes/Main.unity
### Game Rules
- Take turns rolling the dice by clicking it
- Move your pieces according to the rolled value
- First player to get all 4 pieces home wins!

### Testing Mode
To test specific dice rolls:
1. Select the **Dice** GameObject in Unity
2. In the Inspector:
- Enable `isLog` (checkbox in DiceScript)
- Enter desired value (1-6) in `RollValue` field

> **Important**: Disable isLog for normal randomized gameplay!

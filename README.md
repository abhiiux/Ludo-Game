# 🎲 Ludo Game (Unity 2D)  
**A assignment submission by [Abhidev Ramesh]** | **[https://abhidevramesh.framer.website/]**  

[Screenshots]  
<img width="367" alt="Screenshot 2025-06-25 at 4 43 05 PM" src="https://github.com/user-attachments/assets/6ccabfcd-1b04-4c49-ae7e-89fbe60e777e" />

## 🏆 **Key Highlights** *  
- **Architecture**: Implemented turn management and movement logic using **modular C# scripts**  
- **UX**: Designed intuitive click-based controls (no complex keybindings).  
- **Optimization**: Avoided Update() overuse with **event-driven logic** (mention if applicable).  

### **Code Structure**  
```plaintext
Assets/
├── Scripts/
│   ├── Dice.cs         // Handles RNG and roll visualization
│   ├── Player.cs       // Manages pieces and turn logic
│   └── GameManager.cs  // Central game state controller
└── Sprites/            // Custom/optimized 2D assets

## 🎮 How to Play & Test

### 🚀 Quick Start
-Assets/Scenes/Main.unity
🎲 Game Rules
Take turns rolling the dice by clicking it

Move your pieces according to the rolled value

First player to get all 4 pieces home wins!

🧪 Testing Mode
To test specific dice rolls:

Select the Dice GameObject in Unity

In the Inspector:

✔️ Enable Test Mode (checkbox in DiceScript)

🔢 Enter desired value (1-6) in Test Dice Value field

⚠️ Important: Disable Test Mode for normal randomized gameplay!

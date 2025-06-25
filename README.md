# 🎲 Ludo Game (Unity 2D)  
**A assignment submission by [Abhidev Ramesh]** | **[https://abhidevramesh.framer.website/]**  

![Gameplay GIF](Screenshots/gameplay.gif) *(Crucial! Show your work visually.)*  

## 🏆 **Key Highlights** *  
- **Architecture**: Implemented turn management and movement logic using **modular C# scripts**  
- **UX**: Designed intuitive click-based controls (no complex keybindings).  
- **Optimization**: Avoided Update() overuse with **event-driven logic** (mention if applicable).  

## 🛠️ **Technical Implementation** 
### **Code Structure**  
```plaintext
Assets/
├── Scripts/
│   ├── Dice.cs         // Handles RNG and roll visualization
│   ├── Player.cs       // Manages pieces and turn logic
│   └── GameManager.cs  // Central game state controller
└── Sprites/            // Custom/optimized 2D assets

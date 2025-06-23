using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<TeamScript> players = new List<TeamScript>();
    [SerializeField] private DiceScript diceScript;
    // [SerializeField] private bool isLog = false;

    // private List<Transform> tiles = new List<Transform>();
    // private int totalTiles;
    private int currentPlayerIndex = 0;

    private void OnEnable()
    {
        diceScript.DiceRoll += HandleDiceRoll;
    }

    private void OnDisable()
    {
        diceScript.DiceRoll -= HandleDiceRoll;
    }
    // private void Start()
    // {
    //     tiles = TileManager.Instance.CommonTiles();
    //     totalTiles = tiles.Count;
    // }

    public void HandleDiceRoll(int value)
    {
        TeamScript team = players[currentPlayerIndex];
        team.HandleInput(value);

        EndTurn(value);
    }
    private void EndTurn(int rollValue)
    {
        if (rollValue != 6)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        // UIManager.Instance.ShowTurns(currentPlayerIndex);
    }
}

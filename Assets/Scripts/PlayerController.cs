using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<TeamScript> players = new List<TeamScript>();
    [SerializeField] private DiceScript diceScript;
    [SerializeField] private bool isLog = false;


    private int currentPlayerIndex = 0;

    private void OnEnable()
    {
        diceScript.DiceRoll += HandleDiceRoll;
    }

    private void OnDisable()
    {
        diceScript.DiceRoll -= HandleDiceRoll;
    }

    public void HandleDiceRoll(int value)
    {
        TeamScript team = players[currentPlayerIndex];
        team.HandleInput(value);
        Log($" {currentPlayerIndex} team's is turn");
        UIManager.Instance.ShowTurns(currentPlayerIndex);

        EndTurn(value);
    }
    private void EndTurn(int rollValue)
    {
        if (rollValue != 6)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

    }
    private void Log(string message)
    {
        if (isLog)
        {
            Debug.Log(message);
        }
    }
}

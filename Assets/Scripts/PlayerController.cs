using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject[] allActiveTeams;
    [SerializeField] private DiceScript diceScript;
    [SerializeField] private bool isLog = false;

    private List<TeamScript> teams = new List<TeamScript>();
    private int currentPlayerIndex = 0;
    private int playerCount = 0;

    private void Awake()
    {
        playerCount = SceneData.NumberOfPlayers;
    }
    private void OnEnable()
    {
        diceScript.DiceRoll += HandleDiceRoll;
    }
    private void Start()
    {
        teams.Clear();

        for (int i = 0; i < allActiveTeams.Length; i++)
        {
            if (i < playerCount)
            {
                GameObject teamGO = allActiveTeams[i];
                teamGO.SetActive(true);

                TeamScript teamScript = teamGO.GetComponent<TeamScript>();
                if (teamScript != null)
                {
                    teams.Add(teamScript);
                }
                else if (isLog)
                {
                    Log($"No TeamScript found on {teamGO.name}");
                }
            }
            else
            {
                allActiveTeams[i].SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        diceScript.DiceRoll -= HandleDiceRoll;
    }

    public void HandleDiceRoll(int value)
    {
        TeamScript team = teams[currentPlayerIndex];
        EndTurn(value);
        team.HandleInput(value);
        // Log($" {currentPlayerIndex} team's is turn");

    }
    public void GiveChance()
    {
        currentPlayerIndex -= 1;
        Log($" give one more chance to team {currentPlayerIndex}");
        UIManager.Instance.GiveChance();
    }
    private void EndTurn(int rollValue)
    {
        if (rollValue != 6)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % teams.Count;
        }
        UIManager.Instance.SaveNextTurn(currentPlayerIndex);
    }
    private void Log(string message)
    {
        if (isLog)
        {
            Debug.Log(message);
        }
    }
}

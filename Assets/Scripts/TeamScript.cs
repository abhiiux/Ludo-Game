using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamScript : MonoBehaviour
{
    public List<PlayerScript> pawns = new List<PlayerScript>();
    [HideInInspector] public List<PlayerScript> moveablePawns = new List<PlayerScript>();
    [SerializeField] private bool isLog;
    [SerializeField] private List<Transform> teamtile = new List<Transform>();

    private TeamClickable teamClickable;
    private int moveDuration = 1;
    public int startPosition = 0;
    private int currentRollValue = 0;
    private List<Transform> tiles = new List<Transform>();
    private int totalTiles;

    void Awake()
    {
        pawns = GetComponentsInChildren<PlayerScript>().ToList();
        teamClickable = GetComponent<TeamClickable>();
    }
    void Start()
    {
        tiles = TileManager.Instance.CommonTiles();
        totalTiles = tiles.Count;
    }
#region InputHandling
    public void HandleInput(int rollValue)
    {
        currentRollValue = rollValue;
        PlayerOnField();

        if (rollValue == 6)
        {
            HandleSixRoll();
        }
        else
        {
            HandleNormalRoll();
        }
    }
    private void HandleSixRoll()
    {
        if (moveablePawns.Count == 0)
        {
            if (pawns.Count == 0)
            {
                Log("No pawns in jail to release.");
                return;
            }

            SpawnPawnFromJail(pawns[0]);
        }
        else
        {
            HandlePlayerSelection();
            HandleTeamSelection();
        }
    }
    private void PlayerOnField()
    {

        List<PlayerScript> toRemove = new List<PlayerScript>();
        for (int i = 0; i < pawns.Count; i++)
        {
            if (!pawns[i].inJail)
            {
                moveablePawns.Add(pawns[i]);
                toRemove.Add(pawns[i]);
            }
        }

        Log($" roll value is {currentRollValue} & {moveablePawns.Count} movable pawn ");
        foreach (var r in toRemove)
        {
            pawns.Remove(r);
        }
    }

    private void HandleNormalRoll()
    {
        Log($" {currentRollValue} is the roll value, starting normal roll handling");

        if (moveablePawns.Count == 0)
        {
            Log($"Roll value is {currentRollValue}, but no moveable pawns available.");
            UIManager.Instance.ShowTurns();
            return;
        }
        else if (moveablePawns.Count == 1)
        {
            MovePawn(moveablePawns[0]);
        }
        else if (moveablePawns.Count > 1)
        {
            HandlePlayerSelection();
        }
    }
    #endregion
    #region CallBack Handling
    private void HandlePlayerSelection()
    {
        foreach (var players in moveablePawns)
        {
            players.EnableSelection((selectedPlayer) =>
            {
                MovePawn(selectedPlayer);

                foreach (var p in moveablePawns)
                {
                    p.DisableSelection();
                }
            });
        }
    }
    private void HandleTeamSelection()
    {
        if (moveablePawns.Count < 4 && currentRollValue == 6)
        {
            teamClickable.EnableSelection(() =>
            {
                if (pawns[0] == null) return;
                SpawnPawnFromJail(pawns[0]);

                teamClickable.DisableSelection();
            });
        }
    }
    #endregion

    #region Moving
    private void HandleTileSwitch(Enum team)
    {
        teamtile.Clear();

        switch (team)
        {
            case Teams.Blue:
                teamtile = TileManager.Instance.BlueTiles();
                break;
            case Teams.Red:
                teamtile = TileManager.Instance.RedTiles();
                break;
            case Teams.Green:
                teamtile = TileManager.Instance.GreenTiles();
                break;
            case Teams.Yellow:
                teamtile = TileManager.Instance.YellowTiles();
                break;
        }
    }
    private void SpawnPawnFromJail(PlayerScript pawn)
    {
        StartCoroutine(MovePlayer(pawn.transform, tiles[startPosition].position));
        pawn.inJail = false;
        pawn.playerPosition = startPosition;
        pawn.playerSteps = 0;
    }

    private void MovePawn(PlayerScript pawn)
    {
        int totalSteps = pawn.playerSteps + currentRollValue;

        if (!pawn.inHomePath)
        {
            if (totalSteps < tiles.Count)
            {
                int newPos = (pawn.playerPosition + currentRollValue) % tiles.Count;
                StartCoroutine(MovePlayer(pawn.transform, tiles[newPos].position));
                pawn.playerPosition = newPos;
                pawn.playerSteps = totalSteps;
            }
            else
            {
                HandleTileSwitch(pawn.teamType);
                pawn.inHomePath = true;

                int homeIndex = totalSteps - tiles.Count;
                if (homeIndex < teamtile.Count)
                {
                    StartCoroutine(MovePlayer(pawn.transform, teamtile[homeIndex].position));
                    pawn.playerPosition = homeIndex;
                    pawn.playerSteps = totalSteps;

                    if (homeIndex == teamtile.Count - 1)
                    WinScenario(pawn);
                }
                else
                {
                    Log($"{pawn.name} can't move — overshoots home path.");
                }
            }
        }
        else
        {
            int homeIndex = pawn.playerPosition + currentRollValue;
            if (homeIndex < teamtile.Count)
            {
                StartCoroutine(MovePlayer(pawn.transform, teamtile[homeIndex].position));
                pawn.playerPosition = homeIndex;
                pawn.playerSteps = totalSteps;

                if (homeIndex == teamtile.Count - 1)
                    WinScenario(pawn);
            }
            else
            {
                Log($"{pawn.name} can't move — overshoots home path.");
            }
        }
    }

    private IEnumerator MovePlayer(Transform player, Vector3 targetPosition)
    {
        Vector3 start = player.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            player.position = Vector3.Lerp(start, targetPosition, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        player.position = targetPosition;

        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        if (!playerScript.inHomePath)
        {
            List<Transform> tile = TileManager.Instance.CommonTiles();
            TileScript landedTile = tile[playerScript.playerPosition].GetComponent<TileScript>();

            if (tile != null)
            {
                landedTile.OnPlayerLands(playerScript);
            }
        }
    }
    #endregion
    private void WinScenario(PlayerScript winner)
    {
        UIManager.Instance.StartDancing();

        moveablePawns.Remove(winner);
        PlayerController playerController = this.GetComponentInParent<PlayerController>();
        playerController.GiveChance();
    }
    private void Log(string message)
    {
        if (isLog)
        {
            Debug.Log(message);
        }
    }
}

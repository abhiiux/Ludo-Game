using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamScript : MonoBehaviour
{
    public List<PlayerScript> pawns = new List<PlayerScript>();
    public List<PlayerScript> moveablePawns = new List<PlayerScript>();
    public TeamClickable teamClickable;
    public int freePawns = 0;
    public int moveDuration = 1;
    public int startPosition = 0;
    public int currentRollValue = 0;
    public bool isLog;

    private List<Transform> tiles = new List<Transform>();
    private int totalTiles;
    void Awake()
    {
        pawns = GetComponentsInChildren<PlayerScript>().ToList();
    }
    void Start()
    {
        tiles = TileManager.Instance.CommonTiles();
        totalTiles = tiles.Count;
    }

    public void HandleInput(int rollValue)
    {
        currentRollValue = rollValue;
        PlayerOnField();

        if (rollValue == 6)
        {
            if (freePawns == 0)
            {
                StartCoroutine(MovePlayer(pawns[0].transform, tiles[startPosition].position));
                pawns[0].inJail = false;
            }
            HandleSelection();
        }
    }

    private void HandleSelection()
    {
        foreach (var players in moveablePawns)
        {
            players.EnableSelection((selectedPlayer) =>
            {
                StartCoroutine(MovePlayer(selectedPlayer.transform, tiles[currentRollValue].position));
                selectedPlayer.inJail = false;

                foreach (var p in moveablePawns)
                {
                    p.DisableSelection();
                }
            });
        }
        teamClickable.EnableSelection(() =>
        {
            if (pawns[0] == null) return;
    
            StartCoroutine(MovePlayer(pawns[0].transform, tiles[startPosition].position));
            pawns[0].inJail = false;

            teamClickable.DisableSelection();
        });
    }
    private IEnumerator MovePlayer(Transform player, Vector3 targetPosition)
    {
        Log($" rolled 6 and their is {freePawns} pawns are free");
        Vector3 start = player.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            player.position = Vector3.Lerp(start, targetPosition, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = targetPosition;

    }
    private void PlayerOnField()
    {
        moveablePawns.Clear();
        
        for (int i = 0; i < pawns.Count; i++)
        {
            if (!pawns[i].inJail)
            {
                freePawns++;
                moveablePawns.Add(pawns[i]);
                pawns.RemoveAt(i);
            }
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

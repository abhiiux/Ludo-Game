using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }
    [SerializeField] GameObject commonTile;
    private List<Transform> commonTiles = new List<Transform>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Transform[] allTransforms = commonTile.GetComponentsInChildren<Transform>();
        foreach (Transform item in allTransforms)
        {
            if (item != commonTile.transform)
            {
                commonTiles.Add(item);
            }
        }
    }

    public List<Transform> CommonTiles()
    {
        return commonTiles;
    }
}

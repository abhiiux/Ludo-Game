using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSetup", menuName = "Scriptable Objects/GameSetup")]
public class GameSetup : ScriptableObject
{
    public List<TeamSetup> teamSetup;
}

[System.Serializable]
public class TeamSetup
{
    public GameObject TeamObj;
    public GameObject TeamUi;
}
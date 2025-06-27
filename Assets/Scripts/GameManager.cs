using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadGame(int number)
    {
        SceneData.NumberOfPlayers = number;
        SceneManager.LoadScene("Main");
    }
}

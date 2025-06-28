using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void LoadGame(int number)
    {
        SceneData.NumberOfPlayers = number;

        switch (number)
        {
            case 2:
                SceneManager.LoadScene("Two Player");
                break;
            case 3:
                SceneManager.LoadScene("Three Player");
                break;
            case 4:
                SceneManager.LoadScene("Four Player");
                break;
        }
    }
}

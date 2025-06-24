using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject turnUI;
    [SerializeField] GameObject rollButton;
    [SerializeField] GameObject waitUI;

    public Image[] turnIndicators;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        turnIndicators = turnUI.GetComponentsInChildren<Image>();
    }

    public void ShowTurns(int index)
    {
        for (int i = 0; i < turnIndicators.Length; i++)
        {
            turnIndicators[i].enabled = (i == index);
        }
    }
    public void RollButton(int value)
    {
        bool r = value > 0 ? true : false;
        rollButton.SetActive(r);
    }
}

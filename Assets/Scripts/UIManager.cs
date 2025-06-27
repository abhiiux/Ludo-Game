using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject turnUI;
    [SerializeField] GameObject rollButton;
    [SerializeField] GameObject waitUI;

    private SpriteRenderer[] turnIndicators;
    private SpriteRenderer danceSprite;
    private Animator animator;
    private int nextTeam;

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
        turnIndicators = turnUI.GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        danceSprite = GetComponent<SpriteRenderer>();

        danceSprite.enabled = false;
    }
    public void SaveNextTurn(int value)
    {
        nextTeam = value;
        Debug.Log($" next turn saved for {nextTeam} ");
    }
    public void GiveChance()
    {
        nextTeam -= 1;
        Debug.Log($" giving a chance || next turn is for {nextTeam}");
        ShowTurns();
    }
    public void ShowTurns()
    {
        Debug.Log($" showing turns || turn is for {nextTeam} ");
        for (int i = 0; i < turnIndicators.Length; i++)
        {
            turnIndicators[i].enabled = (i == nextTeam);
        }
    }
    public void RollButton(int value)
    {
        bool r = value > 0 ? true : false;
        rollButton.SetActive(r);
    }
    public void StartDancing()
    {
        StartCoroutine(Dance());
    }
    private IEnumerator Dance()
    {
        danceSprite.enabled = true;
        animator.SetBool("isDancing", true);

        yield return new WaitForSeconds(5f);

        animator.SetBool("isDancing", false);
        danceSprite.enabled = false;
    }
}

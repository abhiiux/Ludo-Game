using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject turnUI;
    [SerializeField] GameObject rollButton;
    [SerializeField] GameObject waitUI;

    private Image[] turnIndicators;
    private SpriteRenderer danceSprite;
    private Animator animator;

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
        animator = GetComponent<Animator>();
        danceSprite = GetComponent<SpriteRenderer>();
        
        danceSprite.enabled = false;
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
    public void StartDancing()
    {
        StartCoroutine(Dance());
    }
    private IEnumerator Dance()
    {
        danceSprite.enabled = true;
        animator.SetBool("isDancing", true);

        yield return new WaitForSeconds(2f);

        animator.SetBool("isDancing", false);
        danceSprite.enabled = false;
    }
}

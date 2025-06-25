using System;
using System.Collections;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    [SerializeField] GameObject diceAnimation;
    [SerializeField] GameObject diceObjects;
    [SerializeField] bool isLog;
    [SerializeField] int rollValue;

    private int lastValue;
    private SpriteRenderer[] diceSprites;
    public Action<int> DiceRoll;

    void Start()
    {
        diceSprites = diceObjects.GetComponentsInChildren<SpriteRenderer>();

        foreach (var item in diceSprites)
        {
            item.enabled = false;
        }
    }

    public void StartDice()
    {
        StartCoroutine(RollDice());
    }

    private IEnumerator RollDice()
    {
        diceAnimation.SetActive(true);

        yield return new WaitForSeconds(1f);

        diceAnimation.SetActive(false);

        if (!isLog)
        {
            rollValue = UnityEngine.Random.Range(1, 7);
        }
        Log($"dice rolled :{rollValue}");
        ShowDice(rollValue - 1);
        DiceRoll?.Invoke(rollValue);
    }

    private void ShowDice(int value)
    {
        for (int i = 0; i < diceSprites.Length; i++)
        {
            diceSprites[i].enabled = (i == value);
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
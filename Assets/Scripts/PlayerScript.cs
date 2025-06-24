using System;
using UnityEngine;

public enum Teams
{
    Blue,
    Red,
    Green,
    Yellow
}
public class PlayerScript : MonoBehaviour, IClickable
{
    public bool inJail = true;
    public bool inHomePath = false;
    public int playerId;
    public int playerPosition;
    public int playerSteps = -1;
    public Transform startPoint;
    public Teams teamType;
    private bool isSelectable = false;
    private Action<PlayerScript> onSelected;

    public void EnableSelection(Action<PlayerScript> callback)
    {
        isSelectable = true;
        onSelected = callback;
        UIManager.Instance.RollButton(0);
    }

    public void DisableSelection()
    {
        isSelectable = false;
        onSelected = null;
        UIManager.Instance.RollButton(1);
    }

    public void OnClickable()
    {
        if (!isSelectable) return;

        onSelected?.Invoke(this);
        DisableSelection();
    }

}

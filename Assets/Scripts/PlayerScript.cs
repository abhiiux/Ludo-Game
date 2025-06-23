using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour,IClickable
{
    public bool inJail = true;
    public int playerId;
    public int playerPosition;
    private bool isSelectable = false;
    private Action<PlayerScript> onSelected;

    public void EnableSelection(Action<PlayerScript> callback)
    {
        isSelectable = true;
        onSelected = callback;
    }

    public void DisableSelection()
    {
        isSelectable = false;
        onSelected = null;
    }

    public void OnClickable()
    {
        if (!isSelectable) return;

        onSelected?.Invoke(this);
        DisableSelection();
    }

}

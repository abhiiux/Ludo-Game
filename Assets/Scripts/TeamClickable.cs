using System;
using UnityEngine;

public class TeamClickable : MonoBehaviour, IClickable
{
    private bool isSelectable = false;
    private Action onSelected;

    public void EnableSelection(Action callback)
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

        Debug.Log($" Clicked on team");
        onSelected?.Invoke();
        DisableSelection();
    }
}

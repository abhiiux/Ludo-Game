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
        // Debug.Log($"{gameObject.layer} is turned on");
        InputHandler.Instance.SetLayer(gameObject.layer);
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

        Debug.Log($" Clicked on team");
        onSelected?.Invoke();
        DisableSelection();
    }
}

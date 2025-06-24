using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private InputAction mousePosition;
    
    private void OnEnable()
    {
        mouseClick.started += OnClick;
        mouseClick.Enable();
        mousePosition.Enable();
    }

    private void OnDisable()
    {
        mouseClick.started -= OnClick;
        mouseClick.Disable();
        mousePosition.Disable();
    }

    /// <summary>
    /// Called when the mouse is clicked. Detects clicked tile.
    /// </summary>
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Vector2 screenPos = mousePosition.ReadValue<Vector2>();
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(screenPos));

        if (rayHit.collider != null)
        {
            // Debug.Log($" {rayHit.collider.gameObject.name}");

            IClickable clickable = rayHit.collider.GetComponent<IClickable>();
            clickable.OnClickable();
        }
    }
}

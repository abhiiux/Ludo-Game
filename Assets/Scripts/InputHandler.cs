using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;
    [SerializeField] bool isLog;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private InputAction mousePosition;

    private LayerMask currentLayer;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
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
    public void SetLayer(int layerNumber)
    {
        string layerName = LayerMask.LayerToName(layerNumber);
        currentLayer = LayerMask.GetMask(layerName);
        Log($" LayerMask is set to {layerName} which is {currentLayer}");
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        Log($" OnClick started !!");
        if (!context.started) return;

        Vector2 screenPos = mousePosition.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        var rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (rayHit.collider != null)
        {
            Log($" Clicked on {rayHit.collider.name}");
            IClickable clickable = rayHit.collider.GetComponent<IClickable>();

            if (clickable == null) return;
            clickable.OnClickable();
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

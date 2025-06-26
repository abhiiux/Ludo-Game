using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer board;
    void Start()
    {
        float orthoSize = board.bounds.size.x * Screen.height / Screen.width * 0.5f;
        Camera.main.orthographicSize = orthoSize;
    }


}

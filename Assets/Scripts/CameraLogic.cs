using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Transform player;

    [SerializeField] private float minX = -5f, maxX = 5f; // Ограничения по X

    void Update()
    {
        Vector3 targetPosition = player.position + offset;

        // Ограничиваем только X
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = transform.position.y; // Фиксируем Y

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}


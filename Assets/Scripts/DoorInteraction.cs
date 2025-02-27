using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float interactionDistance;
    private bool isPlayerNear = false;
    public Transform playerTransform;
    public GameObject interactionPrompt;

    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private Sprite closedDoorSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedDoorSprite;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= interactionDistance)
        {
            if (!isPlayerNear)
            {
                isPlayerNear = true;
                spriteRenderer.sprite = openDoorSprite;
            }
            ShowInteractionPrompt(true);
        }
        else
        {
            if (isPlayerNear)
            {
                isPlayerNear = false;
                spriteRenderer.sprite = closedDoorSprite;
            }
            ShowInteractionPrompt(false);
        }
    }

    private void ShowInteractionPrompt(bool show)
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(show);
        }
    }
}

using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float interactionDistance;
    private bool isPlayerNear = false;
    public Transform playerTransform;
    public GameObject interactionPrompt;
    void Start()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= interactionDistance)
        {
            isPlayerNear = true;
            ShowInteractionPrompt(true);
        }
        else
        {
            isPlayerNear = false;
            ShowInteractionPrompt(false);
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithDoor();
        }
    }

    private void ShowInteractionPrompt(bool show)
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(show);
        }
    }

    private void InteractWithDoor()
    {
        Debug.Log("Переход на новый уровень...");
    }
}

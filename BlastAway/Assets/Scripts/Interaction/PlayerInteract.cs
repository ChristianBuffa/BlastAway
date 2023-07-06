using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private KeyCode pressToInteract = KeyCode.E;
    public Transform actionPoint;

    private void Update()
    {
        if (Input.GetKeyDown(pressToInteract))
        {
            CheckItemsForInteraction();
        }
    }

    private void CheckItemsForInteraction()
    {
        Physics.BoxCast(transform.position, new Vector3(1, 1, 1), Vector3.forward, out RaycastHit hit);
        var interactable = hit.collider.GetComponent<IInteractable>();
        interactable?.OnInteract();
    }
}
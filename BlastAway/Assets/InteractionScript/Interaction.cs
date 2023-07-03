using UnityEngine;

public class Interaction : MonoBehaviour {
	[SerializeField] private KeyCode pressToInteract;

	private void Start() {
		if (pressToInteract == default) {
			pressToInteract = KeyCode.E;
		}
	}

	private void Update() {
		if (Input.GetKeyDown(pressToInteract)) {
			CheckItemsForInteraction();
		}
	}

	private void CheckItemsForInteraction() {
		Physics.BoxCast(transform.position, new Vector3(1, 1, 1), Vector3.forward, out RaycastHit hit);
		var interactable = hit.collider.GetComponent<IInteractable>();
		interactable?.OnInteract();
	}
}

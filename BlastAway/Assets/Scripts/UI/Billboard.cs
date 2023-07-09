using UnityEngine;

public class Billboard : MonoBehaviour {
	private Transform player;

	private void Start() {
		player = FindObjectOfType<PlayerController>().transform;
	}

	void LateUpdate() {
		transform.LookAt(transform.position + player.forward);
	}
}
using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

	public class UIAdder : MonoBehaviour {
		public event Action<GameObject> InRange;
		public event Action<GameObject> OutOfRange;

		private void Start() {
			GetComponent<SphereCollider>().radius = 2;
		}

		public void OnTriggerEnter(Collider other) {
			if (other.GetComponent<PlayerController>()) {
				Debug.Log("Enter");
				InRange?.Invoke(gameObject);
			}
		}

		public void OnTriggerExit(Collider other) {
			if (other.GetComponent<PlayerController>()) {
				Debug.Log("Exit");
				OutOfRange?.Invoke(gameObject);
			}
		}
	}
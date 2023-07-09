using UnityEngine;


	public class FloatingTextDestructible : MonoBehaviour {
		[SerializeField] private float destroyTime;
		private Vector3 randomizeIntensity = new Vector3(0.7f, 0, 0f);
		private void Start() {
			Destroy(gameObject, destroyTime);
			transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
				Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
				Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
		}
	}

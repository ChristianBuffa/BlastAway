using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIInteractionManager : MonoBehaviour {
	[SerializeField] private GameObject canvasInteraction;
	private PlayerInteract playerInteract;

	private List<Pickup> items;

	private List<GameObject> instantiatedCanvas;
	private Dictionary<GameObject, int> dictionary;

	private void Start() {
		dictionary = new Dictionary<GameObject, int>();
		items = new List<Pickup>();
		instantiatedCanvas = new List<GameObject>();
		items = FindObjectsOfType<Pickup>().ToList();
		foreach (Pickup item in items) {
			item.InRange += CreateInteractionCanvas;
			item.OutOfRange += DestroyCanvas;
			Debug.Log(item.name);
		}
		playerInteract = FindObjectOfType<PlayerInteract>();
	}

	private void CreateInteractionCanvas(GameObject item) {
		instantiatedCanvas.Add(Instantiate(canvasInteraction, item.transform));
		dictionary.Add(instantiatedCanvas.Last(), instantiatedCanvas.LastIndexOf(instantiatedCanvas.Last()));
		var text = canvasInteraction.GetComponentInChildren<TMP_Text>();
		text.text = $"Press {playerInteract.GetInteractionKey()} to interact";
	}

	private void DestroyCanvas(GameObject item) {
		dictionary.TryGetValue(item, out int index);
		Destroy(instantiatedCanvas[index]);
		dictionary.Remove(instantiatedCanvas[index]);
		instantiatedCanvas.Remove(instantiatedCanvas[index]);
	}

}
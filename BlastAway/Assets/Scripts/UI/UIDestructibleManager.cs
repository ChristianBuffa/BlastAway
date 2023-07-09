using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIDestructibleManager : MonoBehaviour {
    [SerializeField] private GameObject ui;
    private List<Destructible> destructibles;

    private void Start() {
        destructibles = new List<Destructible>();
        destructibles = FindObjectsOfType<Destructible>().ToList();
        foreach (Destructible destructible in destructibles) {
            Debug.Log(destructible.name);
            destructible.OnTakeDamageEvent += CreateFloatingText;
            destructible.OnDeathEvent += CreateFloatingTextDeath;
        }
    }

    private void CreateFloatingText(GameObject obj, int damage) {
        var instantiated = Instantiate(ui, obj.transform);
        var text = instantiated.GetComponentInChildren<TMP_Text>();
        text.text = damage.ToString();
    }

    private void CreateFloatingTextDeath(GameObject obj) {
        var instantiated = Instantiate(ui, obj.transform);
        var text = instantiated.GetComponentInChildren<TMP_Text>();
        text.text = "Death";
    }
}

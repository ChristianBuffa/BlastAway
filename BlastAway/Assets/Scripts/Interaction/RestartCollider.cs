using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            Debug.Log("boia");
            SceneManager.LoadScene(activeScene.name);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}

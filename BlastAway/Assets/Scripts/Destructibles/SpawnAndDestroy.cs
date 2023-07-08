using UnityEngine;

public class SpawnAndDestroy : MonoBehaviour
{
    public int waitTimeBeforeDestroy;

    private void Start()
    {
        Destroy(gameObject, waitTimeBeforeDestroy);
    }
}

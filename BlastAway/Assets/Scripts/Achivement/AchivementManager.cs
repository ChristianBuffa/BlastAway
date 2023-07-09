using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    private static AchivementManager instance;

    public static AchivementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AchivementManager>();

                if (instance == null)
                {
                    GameObject singletonAchivementManager = new GameObject(typeof(AchivementManager).Name);
                    instance = singletonAchivementManager.AddComponent<AchivementManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    [HideInInspector]
    public int destroyedTrees;
    [HideInInspector] 
    public int destroyedAnimals;
    [HideInInspector] 
    public int destroyedVillagers;

    private void Start()
    {
        destroyedTrees = 0;
        destroyedAnimals = 0;
        destroyedVillagers = 0;
    }

    public void CheckTreeNumber()
    {
        if (destroyedTrees >= 30)
        {
            Debug.Log("destroyed 30 trees");
        }
    }

    public void CheckAnimalNumber()
    {
        if (destroyedAnimals >= 10)
        {
            Debug.Log("killed 10 animals");
        }
    }

    public void CheckVillagerNumber()
    {
        if (destroyedVillagers >= 5)
        {
            Debug.Log("Genocida");
        }
    }
}

using System;
using Unity.VisualScripting;
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

    [SerializeField] private AudioClip start, damage01, damage02, trees, end;
    [SerializeField] private float maxTimer = 300f;

    private float timer;
    
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
        
        AudioManager.Instance.PlaySound(start);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= maxTimer)
            AudioManager.Instance.PlaySound(end);
    }

    public void CheckTreeNumber()
    {
        if(destroyedTrees == 100)
        {
            AudioManager.Instance.PlaySound(trees);
        }

        CheckEnd();
    }

    public void CheckAnimalNumber()
    {
        if (destroyedAnimals == 10)
        {
            AudioManager.Instance.PlaySound(damage02);
        }
        
        CheckEnd();
    }

    public void CheckVillagerNumber()
    {
        if (destroyedVillagers == 5)
        {
            AudioManager.Instance.PlaySound(damage01);
        }
        
        CheckEnd();
    }

    private void CheckEnd()
    {
        if (destroyedTrees >= 100 && destroyedAnimals >= 10 && destroyedVillagers >= 5)
        {
            AudioManager.Instance.PlaySound(end);
        }
    }
}

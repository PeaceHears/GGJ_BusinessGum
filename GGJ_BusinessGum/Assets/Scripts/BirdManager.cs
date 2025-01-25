using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public int pigeonCount = 5;
    public int seagullCount = 5;

    void Start()
    {
        CreateBirds();
    }

    void Update()
    {
        
    }

    void CreateBirds()
    {
        CreateBird(pigeonCount, "Assets/Characters/Birds/Pigeon/Pigeon_Bird.prefab");
        CreateBird(seagullCount, "Assets/Characters/Birds/Seagull/Seagull_Bird.prefab");
    }

    void CreateBird(int birdCount, string prefabPath)
    {
        for (int i = 0; i < birdCount; i++)
        {
            float randomNumberForXPosition = Random.Range(-5.0f, 10.0f);
            float randomNumberForYPosition = Random.Range(5.0f, 50.0f);

            Object pigeonPrefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
            GameObject clone = Instantiate(pigeonPrefab, new Vector2(randomNumberForXPosition, randomNumberForYPosition), Quaternion.identity) as GameObject;
        }
    }
}

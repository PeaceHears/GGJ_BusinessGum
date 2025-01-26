using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public int pigeonCount = 3;
    public int seagullCount = 3;

    void Start()
    {
        StartCoroutine(BirdCreationTimer());
    }

    IEnumerator BirdCreationTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            CreateBirds();
        }
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
            Object prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector2.zero, Quaternion.identity) as GameObject;
            clone.transform.parent = transform;
        }
    }
}

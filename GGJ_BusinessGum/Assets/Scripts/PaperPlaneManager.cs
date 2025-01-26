using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PaperPlaneManager : MonoBehaviour
{
    public int paperPlaneCount = 2;

    void Start()
    {
        StartCoroutine(PaperPlaneCreationTimer());
    }

    IEnumerator PaperPlaneCreationTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            CreatePaperPlanes();
        }
    }

    void Update()
    {
        
    }

    void CreatePaperPlanes()
    {
        CreatePaperPlane(paperPlaneCount, "Assets/Characters/PaperPlanes/PaperPlane.prefab");
    }

    void CreatePaperPlane(int paperPlaneCount, string prefabPath)
    {
        for (int i = 0; i < paperPlaneCount; i++)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector2.zero, Quaternion.identity) as GameObject;
            clone.transform.parent = transform;
        }
    }
}

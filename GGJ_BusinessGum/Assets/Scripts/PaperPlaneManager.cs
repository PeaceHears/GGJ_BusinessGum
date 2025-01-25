using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PaperPlaneManager : MonoBehaviour
{
    public int paperPlaneCount = 10;

    void Start()
    {
        CreatePaperPlanes();
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
        }
    }
}

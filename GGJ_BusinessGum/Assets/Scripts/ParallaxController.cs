using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public bool randomizeStart;
    public FloatReference speedMultiplier;

    public MoveType moveType;

    [Header("Only for follow transform")]
    public Transform transformToFollow;
    public VerticalDirection verticalDirection;

    private List<ParallaxImage> images;
    private float lastY;

    private void Start()
    {
        InitController();
    }

#if UNITY_EDITOR
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InitController();
        }
    }
#endif


    private void FixedUpdate()
    {
        if (images == null) return;

        if (moveType == MoveType.FollowTransform)
        {
            FollowTransformY();
        }
    }

    private void FollowTransformY()
    {
        if (verticalDirection == VerticalDirection.Fix) return;

        float distance = lastY - transformToFollow.position.y;
        if (Mathf.Abs(distance) < 0.001f) return;
        foreach (var item in images)
        {
            item.MoveY(distance);
        }
        lastY = transformToFollow.position.y;
    }

    private void InitController()
    {
        InitList();
        ScanForImages();

        foreach (var item in images)
        {
            item.InitImage(speedMultiplier, verticalDirection, moveType == MoveType.FollowTransform);
        }

        if (moveType == MoveType.FollowTransform)
        {
            lastY = transformToFollow.position.y;
        }
    }

    private void InitList()
    {
        if (images == null) images = new List<ParallaxImage>();
        else
        {
            foreach (var item in images)
            {
                item.CleanUpImage();
            }
            images.Clear();
        }
    }

    private void ScanForImages()
    {
        ParallaxImage pi;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                pi = child.GetComponent<ParallaxImage>();
                if (pi != null) images.Add(pi);
            }
        }
    }

}

[System.Serializable]
public class FloatReference
{
    [Range(0.01f, 5)]
    public float value = 1;
}

public enum VerticalDirection
{
    Fix,
    Up,
    Down
}

public enum MoveType
{
    OverTime,
    FollowTransform
}

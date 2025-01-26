using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxImage : MonoBehaviour
{
    public float speedY = 0;
    public int spawnCount = 2;
    public float repositionBuffer = .5f;

    public ImageType imageType;
    [Header("Only for Instances")]
    public float spawnBuffer = 0;

    public bool spawnRandom = false;
    [Range(0, 5)]
    public float spawnRandomRange = 0;

    [Space]

    public bool scaleRandom = false;
    public bool freezeYBottom = true;
    [Range(1, 4)]
    public float scaleRandomRange = 0;


    //private
    private const int roundFactor = 1000;

    private Transform[] controlledTransforms;
    private float imageWidth;
    private FloatReference speedMultiplier;
    private VerticalDirection vDir;
    private SpriteRenderer sr;
    private Vector3 startPos;

    private bool followTransform;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    public void MoveY(float moveBy)
    {
        moveBy *= speedY * speedMultiplier.value;
        if (vDir == VerticalDirection.Down) moveBy *= -1;

        for (int i = 0; i < controlledTransforms.Length; i++)
        {
            Vector3 newPos = controlledTransforms[i].position;
            newPos.y -= moveBy;
            controlledTransforms[i].position = newPos;
        }
    }

    public void CleanUpImage()
    {
        if (controlledTransforms != null)
        {
            for (int i = 1; i < controlledTransforms.Length; i++)
            {
                Destroy(controlledTransforms[i].gameObject);
            }
        }
    }

    public void InitImage(FloatReference speedMultiplier, VerticalDirection vDir, bool followTransform)
    {
        this.speedMultiplier = speedMultiplier;
        this.vDir = vDir;
        this.followTransform = followTransform;

        transform.position = startPos;

        int arraySize = spawnCount;
        if (followTransform) arraySize *= 2;
        arraySize += 1;

        controlledTransforms = new Transform[arraySize];
        controlledTransforms[0] = transform;

        for (int i = 1; i <= spawnCount; i++)
        {
            controlledTransforms[i] = PrepareCopyAt();
            if (followTransform) controlledTransforms[i + spawnCount] = PrepareCopyAt();
        }
    }

    private Transform PrepareCopyAt()
    {
        float posY = transform.position.y;
        Vector3 localScale = transform.localScale;

        if (imageType == ImageType.Instance && scaleRandom)
        {
            localScale.x = Random.Range(1, scaleRandomRange);
            if (Random.value < .5f) localScale.x = 1 / localScale.x;
            localScale.y = localScale.x;

            if (freezeYBottom)
            {
                posY = transform.position.y - (sr.bounds.size.y / 2) * ((transform.localScale.y - localScale.y) / transform.localScale.y);
            }
        }

        GameObject go = Instantiate(gameObject, new Vector3(0, posY, transform.position.z), Quaternion.identity, transform.parent);
        Destroy(go.GetComponent<ParallaxImage>());
        go.transform.localScale = localScale;

        return go.transform;
    }
}

public enum ImageType
{
    Seamless,
    Instance
}

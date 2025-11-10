using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10;

    private float imgFullWidth;
    private float imgHalfWidth;

    public void CalculateImageWidth()
    {
        imgFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imgHalfWidth = imgFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imgHalfWidth) - imageWidthOffset;
        float imageLeftEdge  = (background.position.x - imgHalfWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imgFullWidth;
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imgFullWidth;
    }
}


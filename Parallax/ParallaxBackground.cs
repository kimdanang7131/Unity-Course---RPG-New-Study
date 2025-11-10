using UnityEngine;

public class PallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastMainCameraPositionX;
    private float cameraHalfWidth;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        InitializeLayers();
    }

    private void FixedUpdate()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastMainCameraPositionX;
        lastMainCameraPositionX = currentCameraPositionX;

        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }
    
    private void InitializeLayers()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
            layer.CalculateImageWidth();
    }
}

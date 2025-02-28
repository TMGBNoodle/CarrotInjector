using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Sprite sprite;
        [Range(0f, 1f)]
        public float parallaxSpeedX = 0.5f;
        [Range(0f, 1f)]
        public float parallaxSpeedY = 0.5f;
        [HideInInspector]
        public Transform layerTransform;
        [HideInInspector]
        public SpriteRenderer mainRenderer;
        [HideInInspector]
        public SpriteRenderer leftRenderer;
        [HideInInspector]
        public SpriteRenderer rightRenderer;
        [HideInInspector]
        public float spriteWidth;
    }

    public ParallaxLayer[] layers;
    public Camera mainCamera;
    
    private Vector3 lastCameraPosition;
    private float cameraOrthographicSize;
    private float cameraAspectRatio;
    private float cameraWidth;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
            
        cameraOrthographicSize = mainCamera.orthographicSize;
        cameraAspectRatio = mainCamera.aspect;
        cameraWidth = cameraOrthographicSize * 2 * cameraAspectRatio;
        lastCameraPosition = mainCamera.transform.position;
        
        // Setup each layer
        for (int i = 0; i < layers.Length; i++)
        {
            SetupLayer(i);
        }
    }
    
    void SetupLayer(int index)
    {
        ParallaxLayer layer = layers[index];
        
        // Create a container object for this layer
        GameObject layerObj = new GameObject("ParallaxLayer_" + index);
        layerObj.transform.parent = transform;
        
        // Center it on the camera's initial position, but at z = 0
        layerObj.transform.position = new Vector3(
            mainCamera.transform.position.x, 
            mainCamera.transform.position.y, 
            0
        );
        
        // Store reference to layer transform
        layer.layerTransform = layerObj.transform;
        
        // Create main sprite
        GameObject mainSprite = CreateSprite("Main", layer.sprite, -index, layerObj.transform);
        layer.mainRenderer = mainSprite.GetComponent<SpriteRenderer>();
        
        // Get sprite width for tiling purposes
        layer.spriteWidth = layer.mainRenderer.bounds.size.x;
        
        // Create left and right sprites for seamless scrolling
        GameObject leftSprite = CreateSprite("Left", layer.sprite, -index, layerObj.transform);
        leftSprite.transform.localPosition = new Vector3(-layer.spriteWidth, 0, 0);
        layer.leftRenderer = leftSprite.GetComponent<SpriteRenderer>();
        
        GameObject rightSprite = CreateSprite("Right", layer.sprite, -index, layerObj.transform);
        rightSprite.transform.localPosition = new Vector3(layer.spriteWidth, 0, 0);
        layer.rightRenderer = rightSprite.GetComponent<SpriteRenderer>();
        
        // Scale the sprites to fit the camera height
        ScaleSpritesToFitCamera(layer);
    }
    
    GameObject CreateSprite(string name, Sprite sprite, int sortingOrder, Transform parent)
    {
        GameObject obj = new GameObject(name);
        obj.transform.parent = parent;
        obj.transform.localPosition = Vector3.zero;
        
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = sortingOrder;
        
        return obj;
    }
    
    void ScaleSpritesToFitCamera(ParallaxLayer layer)
    {
        // Get sprite dimensions
        float spriteWidth = layer.mainRenderer.sprite.bounds.size.x;
        float spriteHeight = layer.mainRenderer.sprite.bounds.size.y;
        
        // Calculate the scale needed to cover the camera view height
        float scaleY = (cameraOrthographicSize * 2.5f) / spriteHeight;
        // Maintain aspect ratio
        float scaleX = scaleY;
        
        // Apply the scale to all three sprites
        Vector3 scale = new Vector3(scaleX, scaleY, 1f);
        layer.mainRenderer.transform.localScale = scale;
        layer.leftRenderer.transform.localScale = scale;
        layer.rightRenderer.transform.localScale = scale;
        
        // Update the actual sprite width after scaling
        layer.spriteWidth = layer.mainRenderer.bounds.size.x;
    }
    
    void LateUpdate()
    {
        // Calculate camera movement delta
        Vector3 cameraDelta = mainCamera.transform.position - lastCameraPosition;
        
        // Update each layer's position based on parallax speeds
        for (int i = 0; i < layers.Length; i++)
        {
            ParallaxLayer layer = layers[i];
            
            // Skip if layer has no transform
            if (layer.layerTransform == null)
                continue;
                
            // Calculate parallax offset
            Vector3 parallaxOffset = new Vector3(
                cameraDelta.x * (1f - layer.parallaxSpeedX),
                cameraDelta.y * (1f - layer.parallaxSpeedY),
                0
            );
            
            // Apply parallax movement
            layer.layerTransform.position += parallaxOffset;
            
            // Check if we need to reposition sprites for seamless scrolling
            CheckRepositionSprites(layer);
        }
        
        // Store current camera position for next frame
        lastCameraPosition = mainCamera.transform.position;
    }
    
    void CheckRepositionSprites(ParallaxLayer layer)
    {
        // Get camera position in layer's local space
        Vector3 cameraLocalPos = layer.layerTransform.InverseTransformPoint(mainCamera.transform.position);
        
        // Check if main sprite is too far left
        if (cameraLocalPos.x - layer.mainRenderer.transform.localPosition.x > layer.spriteWidth)
        {
            // Move the left sprite to become the right sprite
            layer.leftRenderer.transform.localPosition = layer.mainRenderer.transform.localPosition + new Vector3(layer.spriteWidth, 0, 0);
            
            // Swap references
            SpriteRenderer temp = layer.leftRenderer;
            layer.leftRenderer = layer.mainRenderer;
            layer.mainRenderer = layer.rightRenderer;
            layer.rightRenderer = temp;
        }
        // Check if main sprite is too far right
        else if (layer.mainRenderer.transform.localPosition.x - cameraLocalPos.x > layer.spriteWidth)
        {
            // Move the right sprite to become the left sprite
            layer.rightRenderer.transform.localPosition = layer.mainRenderer.transform.localPosition - new Vector3(layer.spriteWidth, 0, 0);
            
            // Swap references
            SpriteRenderer temp = layer.rightRenderer;
            layer.rightRenderer = layer.mainRenderer;
            layer.mainRenderer = layer.leftRenderer;
            layer.leftRenderer = temp;
        }
    }
}
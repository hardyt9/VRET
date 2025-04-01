using UnityEngine;

public class TransparencyToggle : MonoBehaviour
{
    public Renderer cubeRenderer; // Assign the cube's Renderer in the Inspector
    public bool isTransparent = false; // Tracks the current state
    public bool canToggleToTransparent = false; // Only set to true if the player collides

    void Start()
    {
        if (cubeRenderer == null)
        {
            //Debug.LogError("Cube Renderer is not assigned!");
            return;
        }

        // Automatically detect if the material starts as transparent
        isTransparent = cubeRenderer.material.renderQueue > 2500;
    }

    void Update()
    {
        // Allow toggling only if we can toggle back to transparent and the cube is opaque
        if (Input.GetKeyDown(KeyCode.T) && !isTransparent && canToggleToTransparent)
        {
            ToggleTransparency();
        }
    }

    public void ToggleTransparency()
    {
        if (isTransparent)
        {
            SetOpaque();
        }
        else
        {
            SetTransparent();
        }

        isTransparent = !isTransparent; // Toggle state
    }

    void SetOpaque()
    {
        Material material = cubeRenderer.material;
        material.SetFloat("_Surface", 0); // Set to Opaque (URP/Lit Shader)
        material.renderQueue = 2000; // Standard Opaque queue
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
       // Debug.Log("Cube is now opaque.");
    }

    void SetTransparent()
    {
        Material material = cubeRenderer.material;
        material.SetFloat("_Surface", 1); // Set to Transparent (URP/Lit Shader)
        material.renderQueue = 3000; // Transparent queue
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.EnableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        //Debug.Log("Cube is now transparent.");
    }

}

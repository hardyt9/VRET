using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Color originalColor;
    private Color transparentColor;
    private Material cubeMaterial;
    private bool isPlayerInTrigger = false; // Track if the player is inside the trigger

    // Add a field to track transparency state
    public bool isTransparent { get; private set; } = false;

    void Start()
    {
        // Get the Renderer component of the cube
        cubeRenderer = GetComponent<Renderer>();

        // Store the original color of the cube (assumed to be blue in the beginning)
        originalColor = cubeRenderer.material.color;

        // Set the transparent color (fully transparent, blue with 0 alpha)
        transparentColor = new Color(0f, 0f, 1f, 0f); // Blue color with 0 alpha

        // Store the material so we can modify the transparency later
        cubeMaterial = cubeRenderer.material;

        // Ensure the material is using a shader that supports transparency
        cubeMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        cubeMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        cubeMaterial.SetInt("_ZWrite", 1); // Ensure depth writing is enabled initially (opaque mode)
        cubeMaterial.SetInt("_RenderQueue", 2000); // Normal opaque render queue

        // Ensure it starts in opaque mode
        SetTransparent(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter() - Player has entered the trigger area.");
            isPlayerInTrigger = true; // Player is inside the trigger
            // Change the cube's material color to transparent
            SetTransparent(true);
        }
        else
        {
            Debug.Log("OnTriggerEnter() - The object that entered is not the player.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has exited the trigger area
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerExit() - Player has exited the trigger area.");
            isPlayerInTrigger = false; // Player is no longer inside the trigger
            // Reset the cube's color back to blue
            SetTransparent(false);
        }
        else
        {
            Debug.Log("OnTriggerExit() - The object that exited is not the player.");
        }
    }

    public void SetTransparent(bool transparent)
    {
        if (transparent)
        {
            Debug.Log("SetTransparent() - Setting color to transparent.");
            // Set the color to transparent (with 0 alpha)
            cubeMaterial.color = transparentColor;

            // Switch to transparent rendering mode
            cubeMaterial.SetInt("_ZWrite", 0); // Disable depth writing for transparency
            cubeMaterial.SetInt("_RenderQueue", 3000); // Set render queue for transparent objects

            // Mark as transparent
            isTransparent = true;
        }
        else
        {
            Debug.Log("SetTransparent() - Restoring color to original (blue).");
            // Set the color back to original (blue)
            cubeMaterial.color = originalColor;

            // Switch to opaque rendering mode
            cubeMaterial.SetInt("_ZWrite", 1); // Enable depth writing for opaque
            cubeMaterial.SetInt("_RenderQueue", 2000); // Normal opaque render queue

            // Mark as opaque
            isTransparent = false;
        }
    }

    void Update()
    {
        // If there is no collision (player not in trigger), keep it opaque
        if (!isPlayerInTrigger)
        {
            SetTransparent(false); // Keep the cube opaque when the player is not in the trigger
        }
    }
}

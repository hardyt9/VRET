using UnityEngine;
using TMPro; // For TMP_Text

public class BreathingTextController : MonoBehaviour
{
    public Transform circle; // Assign the 3D cylinder's transform
    public TMP_Text breathText; // TMP Text to display the message

    private float lastScale;
    private bool isBreathingIn;

    void Start()
    {
        lastScale = circle.localScale.x; // Assuming uniform scale (same value for x, y, z)
    }

    void Update()
    {
        float currentScale = circle.localScale.x;

        // If the circle is increasing in size, set "Breathe In"
        if (currentScale > lastScale)
        {
            if (!isBreathingIn)
            {
                breathText.text = "Breathe In";
                isBreathingIn = true;
            }
        }
        // If the circle is decreasing in size, set "Breathe Out"
        else if (currentScale < lastScale)
        {
            if (isBreathingIn)
            {
                breathText.text = "Breathe Out";
                isBreathingIn = false;
            }
        }

        lastScale = currentScale; // Update the last scale for the next frame
    }
}

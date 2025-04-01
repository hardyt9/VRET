using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPopupController : MonoBehaviour
{
    public GameObject popupCanvas; // Assign the UI Canvas in the Inspector
    public Transform xrRig; // Assign the XR Rig (or Camera) here
    public float spawnDistance = 1.5f; // Distance in front of the player

    private bool isPaused = false;

    void Update()
    {
        if (CheckInput(XRNode.RightHand, CommonUsages.primaryButton))
        {
            TogglePopup();
        }
    }

    private bool CheckInput(XRNode hand, InputFeatureUsage<bool> button)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(hand);
        if (device.TryGetFeatureValue(button, out bool pressed))
        {
            return pressed;
        }
        return false;
    }

    public void TogglePopup()
    {
        if (!isPaused) // Open menu
        {
            PositionPopupInFront();
            Time.timeScale = 0f; // Pause game
        }
        else // Close menu
        {
            ResumeGame();
        }

        isPaused = !isPaused;
        popupCanvas.SetActive(isPaused);
    }

    private void PositionPopupInFront()
    {
        if (xrRig == null)
        {
            Debug.LogWarning("XR Rig not assigned!");
            return;
        }

        Transform cameraTransform = xrRig.GetComponentInChildren<Camera>().transform;
        Vector3 newPosition = cameraTransform.position + cameraTransform.forward * spawnDistance;
        popupCanvas.transform.position = newPosition;
        popupCanvas.transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
    }

    // Call this from the Resume button!
    public void ResumeGame()
    {
        isPaused = false;
        popupCanvas.SetActive(false);
        Time.timeScale = 1f; // Unpause game
    }
}

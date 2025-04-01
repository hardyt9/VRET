using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        // Example: Listen for the Oculus Quest 2 A button
        if (Input.GetKeyDown(KeyCode.JoystickButton0)) // Change based on your device/input
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);

        // Optional: Freeze game time
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
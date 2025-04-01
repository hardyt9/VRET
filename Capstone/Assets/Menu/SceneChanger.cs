using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Set this in the Inspector

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

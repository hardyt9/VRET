using UnityEngine;

public class PulsingCircle : MonoBehaviour
{
    public float pulseDuration = 4f; // Time to complete a full pulse in and out
    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float time = Time.time;
        float t = (Mathf.Sin((time / pulseDuration) * Mathf.PI * 2) + 1f) / 2f; // t goes from 0 to 1
        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = originalScale * scale;
    }
}

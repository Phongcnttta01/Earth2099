using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;
    public float intensity = 0.1f;  // Thêm biến intensity

    public void ShakeCamera(float duration, float intensity)
    {
        originalPosition = transform.position;
        this.intensity = intensity;  // Gán giá trị cho biến intensity
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", duration);
    }

    private void DoShake()
    {
        float offsetX = Random.Range(-intensity, intensity);
        float offsetY = Random.Range(-intensity, intensity);
        transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
    }

    private void StopShake()
    {
        CancelInvoke("DoShake");
        transform.localPosition = originalPosition;
    }
    void Update()
    {
        
    }
}
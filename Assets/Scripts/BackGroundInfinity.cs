using UnityEngine;
using UnityEngine.UI;

public class BackGroundInfinity : MonoBehaviour
{
    public Transform mainCam;
    public Transform midBg;
    public Transform sideBg;
    public float length;
    void Start()
    {
        if (mainCam.position.x > midBg.position.x)
        {
            UpdateBGPosition(Vector3.right);
        }
        else if (mainCam.position.x < midBg.position.x)
        {
            UpdateBGPosition(Vector3.left);
        }
    }

    void UpdateBGPosition(Vector3 direction)
    {
        sideBg.position = midBg.position + direction * length;
        Transform temp = midBg;
        midBg = sideBg;
        sideBg = temp;
    }
}

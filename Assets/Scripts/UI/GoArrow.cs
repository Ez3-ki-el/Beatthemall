using UnityEngine;
using UnityEngine.UI;

public class GoArrow : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 initialcameraTransform;
    public Image arrowImage; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialcameraTransform = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(initialcameraTransform.x - cameraTransform.position.x) >= Mathf.Abs(2.4f))
        {
            arrowImage.enabled = false;
        }
        else
        {
            arrowImage.enabled = true;
        }
    }
}

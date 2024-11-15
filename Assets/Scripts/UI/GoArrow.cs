using UnityEngine;
using UnityEngine.UI;

public class GoArrow : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 initialcameraTransform;
    public Image arrowImage;
    public AudioSource arrowAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrowAudioSource = gameObject.GetComponent<AudioSource>();
        initialcameraTransform = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(initialcameraTransform.x - cameraTransform.position.x) >= Mathf.Abs(2.4f))
        {
            arrowAudioSource.enabled = false;
            arrowImage.enabled = false;
        }
        else
        {
            arrowAudioSource.enabled = true;
            arrowImage.enabled = true;
        }
    }
}

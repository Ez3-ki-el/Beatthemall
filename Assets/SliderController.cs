using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;

    private void Start()
    {
        slider1.minValue = 1;
        slider1.maxValue = 5;
        slider1.value = 1;

        slider2.minValue = 1;
        slider2.maxValue = 5;
        slider2.value = 2;

        slider1.onValueChanged.AddListener(OnSlider1ValueChanged);
        slider2.onValueChanged.AddListener(OnSlider2ValueChanged);
    }

    private void OnSlider1ValueChanged(float value)
    {
        if (slider1.value == slider2.value)
        {
            if (slider1.value < slider1.maxValue)
            {
                slider1.value += 1;
            }
            else
            {
                slider1.value -= 1;
            }
        }
    }

    private void OnSlider2ValueChanged(float value)
    {
        if (slider1.value == slider2.value)
        {
            if (slider2.value < slider2.maxValue)
            {
                slider2.value += 1;
            }
            else
            {
                slider2.value -= 1;
            }
        }
    }

    public void DecrementSlider1()
    {
        if (slider1.value > 1)
        {
            slider1.value -= 1;
        }
    }

    public void DecrementSlider2()
    {
        if (slider2.value > 1)
        {
            slider2.value -= 1;
        }
    }
}


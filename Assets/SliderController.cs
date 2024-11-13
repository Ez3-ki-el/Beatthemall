using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider1;  // R�f�rence au premier Slider
    public Slider slider2;  // R�f�rence au deuxi�me Slider

    private void Start()
    {
        // Initialiser les sliders avec des valeurs valides
        slider1.minValue = 1;
        slider1.maxValue = 5;
        slider1.value = 1;

        slider2.minValue = 1;
        slider2.maxValue = 5;
        slider2.value = 2;

        // Ajouter des listeners pour surveiller les changements de valeur
        slider1.onValueChanged.AddListener(OnSlider1ValueChanged);
        slider2.onValueChanged.AddListener(OnSlider2ValueChanged);
    }

    // M�thode appel�e lorsqu'une valeur de slider1 change
    private void OnSlider1ValueChanged(float value)
    {
        // Si slider1 et slider2 ont la m�me valeur, on ajuste slider1 pour �viter le conflit
        if (slider1.value == slider2.value)
        {
            // Si slider1 est �gal � slider2, on ajuster slider1 de +1 ou -1
            if (slider1.value < slider1.maxValue)
            {
                slider1.value += 1;  // Incr�menter slider1 pour r�soudre le conflit
            }
            else
            {
                slider1.value -= 1;  // Si d�j� � la valeur max, d�cr�menter slider1
            }
        }
    }

    // M�thode appel�e lorsqu'une valeur de slider2 change
    private void OnSlider2ValueChanged(float value)
    {
        // Si slider1 et slider2 ont la m�me valeur, on ajuste slider2 pour �viter le conflit
        if (slider1.value == slider2.value)
        {
            // Si slider2 est �gal � slider1, on ajuster slider2 de +1 ou -1
            if (slider2.value < slider2.maxValue)
            {
                slider2.value += 1;  // Incr�menter slider2 pour r�soudre le conflit
            }
            else
            {
                slider2.value -= 1;  // Si d�j� � la valeur max, d�cr�menter slider2
            }
        }
    }

    // M�thode pour d�cr�menter slider1 de 1
    public void DecrementSlider1()
    {
        // D�cr�menter slider1, mais ne pas descendre en dessous de 1
        if (slider1.value > 1)
        {
            slider1.value -= 1;
        }
    }

    // M�thode pour d�cr�menter slider2 de 1
    public void DecrementSlider2()
    {
        // D�cr�menter slider2, mais ne pas descendre en dessous de 1
        if (slider2.value > 1)
        {
            slider2.value -= 1;
        }
    }
}


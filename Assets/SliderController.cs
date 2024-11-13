using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider1;  // Référence au premier Slider
    public Slider slider2;  // Référence au deuxième Slider

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

    // Méthode appelée lorsqu'une valeur de slider1 change
    private void OnSlider1ValueChanged(float value)
    {
        // Si slider1 et slider2 ont la même valeur, on ajuste slider1 pour éviter le conflit
        if (slider1.value == slider2.value)
        {
            // Si slider1 est égal à slider2, on ajuster slider1 de +1 ou -1
            if (slider1.value < slider1.maxValue)
            {
                slider1.value += 1;  // Incrémenter slider1 pour résoudre le conflit
            }
            else
            {
                slider1.value -= 1;  // Si déjà à la valeur max, décrémenter slider1
            }
        }
    }

    // Méthode appelée lorsqu'une valeur de slider2 change
    private void OnSlider2ValueChanged(float value)
    {
        // Si slider1 et slider2 ont la même valeur, on ajuste slider2 pour éviter le conflit
        if (slider1.value == slider2.value)
        {
            // Si slider2 est égal à slider1, on ajuster slider2 de +1 ou -1
            if (slider2.value < slider2.maxValue)
            {
                slider2.value += 1;  // Incrémenter slider2 pour résoudre le conflit
            }
            else
            {
                slider2.value -= 1;  // Si déjà à la valeur max, décrémenter slider2
            }
        }
    }

    // Méthode pour décrémenter slider1 de 1
    public void DecrementSlider1()
    {
        // Décrémenter slider1, mais ne pas descendre en dessous de 1
        if (slider1.value > 1)
        {
            slider1.value -= 1;
        }
    }

    // Méthode pour décrémenter slider2 de 1
    public void DecrementSlider2()
    {
        // Décrémenter slider2, mais ne pas descendre en dessous de 1
        if (slider2.value > 1)
        {
            slider2.value -= 1;
        }
    }
}


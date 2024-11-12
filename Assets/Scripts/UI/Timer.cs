using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Référence au texte UI pour afficher le timer
    public TextMeshProUGUI timerText;

    // Variables pour la gestion du temps
    private float timeRemaining = 0f;
    public bool countDown;
    private bool timerIsRunning = false;

    // Définir un temps initial, par exemple 60 secondes
    public float startTime = 0f;

    void Start()
    {
        // Démarre le timer avec le temps initial
        if (countDown)
        {
            timeRemaining = startTime;
        }
        timerIsRunning = true;
    }

    void Update()
    {

        if (timerIsRunning)
        {
            if (countDown)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                }
                DisplayTime(timeRemaining);
            }

            else
            {
                startTime += Time.deltaTime;
                DisplayTime(startTime);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
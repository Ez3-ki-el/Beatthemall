using Assets.Scripts.ScriptableObjects;

using UnityEngine;
using UnityEngine.UI;

public class Filling : MonoBehaviour
{
    public Image barLife;
    public Image barUlti;

    public PlayerPoints PlayerPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float value = PlayerPoints.LifePoints / PlayerPoints.MaxLifePoints;
        if (barLife != null)
            barLife.fillAmount = value;

        float valueUlti = PlayerPoints.UltiPoints / PlayerPoints.MaxUltiPoints;
        if (barUlti != null)
            barUlti.fillAmount = valueUlti;
    }
}

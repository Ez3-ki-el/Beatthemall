using System.Collections;

using Assets.Scripts.Player;

using UnityEngine;

using static Unity.Collections.AllocatorManager;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Cans : MonoBehaviour
{

    public CansType Type;
    public enum CansType
    {
        BLUE,
        GREEN,
        RED
    }

    public float blinkDuration = 1.0f; // Durée totale du clignotement
    public float blinkInterval = 0.1f; // Intervalle de clignotement

    public float lifeTime = 7f;
    public float blinkingTime = 3f;
    private float chronoLife = 0;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        chronoLife += Time.deltaTime;

        if (chronoLife > blinkingTime && !isBlinking)
        {
            StartCoroutine(BlinkItem());
            isBlinking = true;
        }

        if (chronoLife > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && (collision.CompareTag("Player1") || collision.CompareTag("Player2")))
        {
            audioSource.Play();
            GetComponent<SpriteRenderer>().enabled = false; // Cache l'item visuellement
            Destroy(gameObject, audioSource.clip.length); // Détruit l'objet après que l'audio soit terminé
        }
    }


    private IEnumerator BlinkItem()
    {
        while (chronoLife < lifeTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
        spriteRenderer.enabled = true;
    }
}

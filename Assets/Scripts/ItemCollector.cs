using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int fruit = 0;

    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private Text ScoreText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            fruit++;
            ScoreText.text = "Score: " + fruit;
        }
    }
}

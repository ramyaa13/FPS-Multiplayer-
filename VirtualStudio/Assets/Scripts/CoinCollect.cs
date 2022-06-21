using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollect : MonoBehaviour
{
    private int score;
    private Manager manager;
    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Debug.Log("coins collect");
            manager.CoinCollected(other.gameObject);
            score++;
            Score(score);

        }
    }

    public void Score(int score)
    {

        ScoreText.text = score.ToString();

    }
}

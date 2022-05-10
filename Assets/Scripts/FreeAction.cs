using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeAction : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI scoreText;
    private int score;
    public GameObject canFreeText;

    public Hazard hazard;

    void Start()
    {
        score = 0;
        canFreeText.SetActive(false);

        SetScoreText();
    }

    // While player is near Hazard, they can perform actions and text appears on screen
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("I can free the Cat!");
            if (hazard.trapped == true)
            {
                canFreeText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    hazard.freeCat();
                    score += 9;
                    SetScoreText();
                }
            }
        }
    }

    // Upon leaving Hazard area, text will disappear
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            canFreeText.SetActive(false);
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

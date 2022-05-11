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

    public static bool freeTime = false;
    public Hazard hazard;

    void Start()
    {
        score = 0;
        canFreeText.SetActive(false);

        SetScoreText();
    }

    private void Update()
    {
        if(freeTime == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                HUDscript.freeMeter += 1;
                if (HUDscript.freeMeter >= 3)
                {
                    Debug.Log("F pressed!");
                    hazard.freeCat();
                    score += 9;
                    HUDscript.freeMeter = 0;
                    SetScoreText();
                }
            }
        }
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
                freeTime = true;
            }
            else
            {
                canFreeText.SetActive(false);
                freeTime = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            freeTime = false;
            canFreeText.SetActive(false);
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

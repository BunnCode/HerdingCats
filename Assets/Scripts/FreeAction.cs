using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeAction : MonoBehaviour
{
    public GameObject player;
    public GameObject freeTextObject;

    //public GameObject scoreTextObject;
    //private int score;

    public Hazard hazard;

    //ScriptB MyScript;

    void Start()
    {
        //score = 0;
        //SetScoreText();
        freeTextObject.SetActive(false);
    }

    // While player is near Hazard, they can perform actions and text appears on screen
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("I can free the Cat!");
            freeTextObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(hazard.trapped == true)
                {
                    hazard.freeCat();
                }
            }
        }
    }

    // Upon leaving Hazard area, text will disappear
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            freeTextObject.SetActive(false);
        }
    }

    /*
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    */
}

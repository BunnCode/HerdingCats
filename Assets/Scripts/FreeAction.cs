using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeAction : MonoBehaviour
{
    public GameObject player;
    public GameObject freeTextObject;

    // Hides text on screen
    void Start()
    {
        freeTextObject.SetActive(false);
    }

    // While player is near Hazard, they can perform actions and text appears on screen
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("I can free the Cat!");
            freeTextObject.SetActive(true);

            // player can now use Free action
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
}

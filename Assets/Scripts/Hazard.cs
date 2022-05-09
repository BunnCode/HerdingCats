using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private int trapped;
    // Start is called before the first frame update
    void Start()
    {
        trapped = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void freeCat(command)
    {
        if (command == true)
        {
            trapped = false;
            Debug.Log("The cat is free!")
        }
    }
}

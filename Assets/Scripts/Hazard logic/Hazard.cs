using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    //A 'trapped' boolean to notify the player object(s) that the hazard object
    // can be interacted with.
    public bool trapped = false;
    
    //An 'occupied' boolean for the cat objects so they don't end up dog-piling
    // (pun intended) the hazard.
    public bool occupied = false;
    
    //A cat object.  To be defined in TrapCat()
    public CatAI Cat;

    //Since the hazard is totally reactionary, there's not really anything going
    // on during startup.
    void Start()
    {

    }

    //Probly should've put something in here, too.. Everything seems to work though
    // soooo...
    void Update()
    {
        
    }

    //Calls the rescue function in the CatAI script and resets the cat object.
    //Will change this to call the help() function once we get a health bar going.
    public void freeCat()
    {
        trapped = false;
        Cat.rescue();
        Cat = null;
        Debug.Log("The cat is free!");
    }

    //Gets the CatAI component from the function call and stores it in the cat
    // variable.  Then, set the cat's state to distressed.
    void TrapCat(GameObject other)
    {
        Cat = other.GetComponent(typeof(CatAI)) as CatAI;
        Debug.Log("The cat is trapped!");
        Cat.setState(CatState.DISTRESS);
        trapped = true;
    }

    //The trigger to detect a cat collision! Calls the TrapCat function and
    // passes it the cat.
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Cat")
        {
            TrapCat(col.gameObject);
        }
    }
}

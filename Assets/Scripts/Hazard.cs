using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public bool trapped = false;
    public bool occupied = false;
    public CatAI Cat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void freeCat()
    {
        trapped = false;
        Cat.rescue();
        Cat = null;
        Debug.Log("The cat is free!");
    }

    void TrapCat(GameObject other)
    {
        Cat = other.GetComponent(typeof(CatAI)) as CatAI;
        Debug.Log("The cat is trapped!");
        Cat.setState(CatState.DISTRESS);
        trapped = true;
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Cat")
        {
            TrapCat(col.gameObject);
        }
        /*CatAI cat = col.getComponent<Cat>();
        if (cat != null)
        {
            TrapCat(cat);
        }*/
    }
}

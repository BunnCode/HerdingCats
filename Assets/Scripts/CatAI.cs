using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//The different states the cat can be in.  The program will check its state
//when updating.
public enum catState
{
    ROAMING,
    CURIOUS,
    DISTRESSED,
    DEAD
}
public class CatAI : MonoBehaviour
{
    //Cat state is roaming by default.
    private catState currentState = catState.ROAMING;

    //The different target locations a cat might go.
    [SerializeField] private List<Transform> randomPositions = new List<Transform>();
    [SerializeField] private List<Transform> goalPositions = new List<Transform>();

    //Different time variables for the timers.
    private float decisionTime = 5f;
    private float distressTime = 9f;

    //The cat's curiousity levels.  This will increase over time.
    private float curiosity = 10f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Debug.Log("Something is working.");
        //Call the romaing loop
        StartCoroutine("decisionTimer");
    }

    private IEnumerator decisionTimer()
    {
        while (currentState == catState.ROAMING)
        {

            //Pick a random number between the cat's current curiosity level and 100.
            //If the number is above the threshold, the cat enters its curious state.
            //Ensures that, eventually, the cat's curiosity will overwhelm it.
            if (Random.Range(curiosity, 100) >= 95)
            {
                currentState = catState.CURIOUS;
            }
            //Otherwise, the cat's curiosity grows.
            else
            {
                chooseRandomTarget();
                curiosity = Mathf.Clamp(curiosity + 10, 0, 100);
            }

            //Kind of like a sleep() function for the loop.  Very necessary.
            yield return new WaitForSeconds(decisionTime);
        }

        //Cat selects its hazard of choice to be its doom.
        chooseGoalTarget();
    }

    private void chooseRandomTarget()
    {
        if (randomPositions.Count == 0) return;

        Debug.Log("choosing a random position. Curiosity: " + curiosity);

        //Just picks a random point on the mesh to walk to.
        var target = randomPositions[Random.Range(0, randomPositions.Count - 1)];
        agent.destination = target.position;
    }

    private void chooseGoalTarget()
    {
        if (goalPositions.Count == 0) return;

        Debug.Log("I have chosen death. Curiosity: " + curiosity);

        //Cat selects his hazard of choice and travels to it.
        //Right now, the cat can choose the same spot over a over, which is kinda lame.
        var target = goalPositions[Random.Range(0, goalPositions.Count - 1)];
        agent.destination = target.position;
    }
}

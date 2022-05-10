using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//The different states the cat can be in.  The program will check its state
//when updating.
public enum CatState
{
    ROAMING,
    CURIOUS,
    DISTRESS,
    DEAD
}
public class CatAI : MonoBehaviour
{
    private CatSpawner catSpawner;

    //Cat state is roaming by default.
    private CatState currentState = CatState.ROAMING;

    //The different target locations a cat might go.
    [SerializeField] private List<Transform> randomPositions = new List<Transform>();
    [SerializeField] private List<Transform> goalPositions = new List<Transform>();

    //The hazard.  We'll change this to a list once we add more of them.
    [SerializeField] private Hazard hazard;

    //Different time variables for the timers.
    private float decisionTime = 5f;
    private float distressTime = 5f;

    //The cat's curiousity levels.  This will increase over time.
    private float curiosity = 10f;

    private const float HELP_POINTS = 27;
    private float playerHelp = 0;

    //NavMesh stuff.
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void setSpawner(CatSpawner catSpawner)
    {
        this.catSpawner = catSpawner;
    }

    public void setPositions(List<Transform> randomPositions, List<Transform> goalPositions, Hazard hazard)
    {
        this.randomPositions = randomPositions;
        this.goalPositions = goalPositions;
        this.hazard = hazard;
    }

    private void Start()
    {
        setState(currentState);
    }

    public void setState(CatState catState)
    {
        StopAllCoroutines();
        currentState = catState;

        switch (currentState)
        {
            //If the cat is roaming, do the romaing function. Etc.
            case CatState.ROAMING:
                StartCoroutine("roam");
                break;
            case CatState.CURIOUS:
                StartCoroutine("ApproachHazard");
                break;
            case CatState.DISTRESS:
                StartCoroutine("CatInTrouble");
                break;
            case CatState.DEAD:
                Debug.Log("A cat has fallen.");
                hazard.occupied = false;
                Destroy(this.gameObject);
                break;
        }
    }

    private IEnumerator roam()
    {
        while (currentState == CatState.ROAMING)
        {

            //Pick a random number between the cat's current curiosity level and 100.
            //If the number is above the threshold, the cat enters its curious state.
            //Ensures that, eventually, the cat's curiosity will overwhelm it.
            if (Random.Range(curiosity, 100) >= 95)
            {
                if (hazard.occupied == true)
                {
                    chooseRandomTarget();
                }
                else
                {
                    hazard.occupied = true;
                    setState(CatState.CURIOUS);
                }
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
    }

    private void chooseRandomTarget()
    {
        if (randomPositions.Count == 0) return;

        Debug.Log("choosing a random position. Curiosity: " + curiosity);

        //Just picks a random point on the mesh to walk to.
        //Right now, the cat can choose the same spot over and over, which is kinda lame.
        var target = randomPositions[Random.Range(0, randomPositions.Count - 1)];
        agent.destination = target.position;
    }

    private IEnumerator ApproachHazard()
    {
        Debug.Log("I have chosen death. Curiosity: " + curiosity);


        var target = goalPositions[Random.Range(0, goalPositions.Count - 1)];

        agent.destination = target.position;

        while (Vector3.Distance(transform.position, target.position) > agent.stoppingDistance)
        {
            yield return new WaitForEndOfFrame();
        }
        //setState(CatState.DISTRESS);
    }

    private IEnumerator CatInTrouble()
    {
        Debug.Log("I'm hella distressed!");
        yield return new WaitForSeconds(distressTime);
        setState(CatState.DEAD);

    }

    public void help()
    {
        playerHelp += 9;

        if (playerHelp >= HELP_POINTS)
        {
            rescue();
        }
    }

    public void rescue()
    {
        Debug.Log("Rescued!");
        StopAllCoroutines();
        hazard.occupied = false;
        catSpawner.spawnCat();
        Destroy(this.gameObject);

    }
}
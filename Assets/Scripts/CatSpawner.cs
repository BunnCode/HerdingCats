using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public List<Transform> randomPositions;
    public List<Transform> goalPositions;
    public Hazard hazard;

    private const float SPAWN_RATE = 30;

    public CatAI catPrefab;

    public void spawnCat()
    {
        CatAI catAI = Instantiate(catPrefab, transform.position, Quaternion.identity);
        catAI.setSpawner(this);
        catAI.setPositions(randomPositions, goalPositions, hazard);
    }

    private void Start()
    {
        StartCoroutine("spawnTimer");
    }

    private IEnumerator spawnTimer()
    {
        while (true)
        {
            spawnCat();
            yield return new WaitForSeconds(SPAWN_RATE);
        }
    }
}

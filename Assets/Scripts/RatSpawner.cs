using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    private const float DECREMENT_COOLDOWN = 5f;
    private const float SPAWN_TIMER_DECREMENT = 0.25f;
    private const float SPAWN_TIMER_MIN = 0.5f;
    private const float SPAWN_TIMER_START= 5f;

    private List<Transform> platforms;
    public GameObject ratPrefab;
    private bool lost = false;
    private float platformLength = 1.22f;
    private float spawnTimer = SPAWN_TIMER_START;

    private Coroutine reduceTimerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        platforms = new List<Transform>();
        foreach(Transform t in transform){
            platforms.Add(t);
            t.GetComponent<SpriteRenderer>().enabled = false;
        }
        spawnRat();
        StartCoroutine(ratSpawner());
        reduceTimerCoroutine = StartCoroutine(reduceTimer());
    }

    private void Update()
    {
        if (spawnTimer <= SPAWN_TIMER_MIN)
        {
            StopCoroutine(reduceTimerCoroutine);
        }
    }

    private Vector2 choosePosition()
    {
        int platform = Random.Range(0, platforms.ToArray().Length);

        float xMin = platforms[platform].transform.position.x - platformLength / 2;
        float xMax = platforms[platform].transform.position.y + platformLength / 2;
        float posX = Random.Range(xMin, xMax);
        float posY = platforms[platform].transform.position.y + 0.3f;

        return new Vector2(posX, posY);
    }

    private void spawnRat()
    {
        Vector2 position = choosePosition();
        Instantiate(ratPrefab, position, Quaternion.Euler(0, 0, 0));
    }

    IEnumerator ratSpawner()
    {
        while(!lost)
        {
            yield return new WaitForSeconds(spawnTimer);
            spawnRat();
            Debug.Log("spawn timer: " + spawnTimer);
        }
    }

    IEnumerator reduceTimer()
    {
        while(!lost)
        {
            yield return new WaitForSeconds(DECREMENT_COOLDOWN);
            spawnTimer -= SPAWN_TIMER_DECREMENT;
        }
    }
}

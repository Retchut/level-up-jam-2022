using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RatSpawner : MonoBehaviour
{
    private const float DECREMENT_COOLDOWN = 5f;
    private const float SPAWN_TIMER_DECREMENT = 0.25f;
    private const float SPAWN_TIMER_MIN = 0.5f;
    private const float SPAWN_TIMER_START= 5f;

    private const int MAX_RAT_COUNT = 10;
    private const int MENU_SCENE = 0;

    private List<Transform> platforms;
    public GameObject ratPrefab;
    private bool lost = false;
    private float platformLength = 1.22f;
    private float spawnTimer = SPAWN_TIMER_START;

    private Coroutine reduceTimerCoroutine;

    [HideInInspector] public int ratCount = 0;
    public TMP_Text ratText;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loseGame();
        }
        ratText.text = "Rats Alive: " + ratCount.ToString() + "/10";
        checkLoss();
    }

    private void checkLoss()
    {
        if(ratCount > MAX_RAT_COUNT)
        {
            Debug.Log("should lose");
            lost = true;
            loseGame();
        }
    }

    private void loseGame()
    {
        Debug.Log("should be losing");
        SceneManager.LoadScene(MENU_SCENE);
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
        GameObject childRat = Instantiate(ratPrefab, position, Quaternion.Euler(0, 0, 0));
        ratCount++;
        childRat.GetComponent<RatController>().ratSpawnerScript = this;
    }

    IEnumerator ratSpawner()
    {
        while(!lost)
        {
            yield return new WaitForSeconds(spawnTimer);
            spawnRat();
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

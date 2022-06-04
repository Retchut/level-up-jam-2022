using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    private List<Transform> platforms;
    public GameObject ratPrefab;
    private bool lost = false;
    private float platformLength = 1.22f;

    // Start is called before the first frame update
    void Start()
    {
        platforms = new List<Transform>();
        foreach(Transform t in transform){
            platforms.Add(t);
        }
        StartCoroutine(spawnRat());
    }

    IEnumerator spawnRat()
    {
        while (!lost) {
            yield return new WaitForSeconds(1f);
            if (!GameObject.Find("rat(Clone)")) {
                Vector2 position = choosePosition();

                Instantiate(ratPrefab, position, Quaternion.Euler(0,0,0));
            }
        }
    }

    private Vector2 choosePosition()
    {
        int platform = Random.Range(0, platforms.ToArray().Length);

        float xMin = platforms[platform].transform.position.x - platformLength / 2;
        float xMax = platforms[platform].transform.position.y + platformLength / 2;
        float posX = Random.Range(xMin, xMax);
        float posY = platforms[platform].transform.position.y + 1;

        return new Vector2(posX, posY);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private Collider2D collider;

    void Start()
    {
        collider = this.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")){
            Destroy(collision.gameObject);
            StartCoroutine(die());
        }
    }

    private IEnumerator die()
    {
        //TODO: change to dead sprite
        //TODO: play death animation of rat
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}

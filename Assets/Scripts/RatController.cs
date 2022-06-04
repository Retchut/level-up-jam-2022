using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private bool fadeOut = false;
    private const float FADE_AMOUNT = 2f;

    void Start()
    {
        collider = this.GetComponent<Collider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (fadeOut)
        {
            //this.spriteRenderer.color -= FADE_AMOUNT;
            Color oldColor = this.spriteRenderer.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - FADE_AMOUNT * Time.deltaTime);
            this.spriteRenderer.color = newColor;

            if (this.spriteRenderer.color.a <= 0)
            {
                fadeOut = false;
                Destroy(this.gameObject);
            }
        }
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
        //TODO: play death sound
        yield return new WaitForSeconds(2);
        this.fadeOut = true;
    }
}
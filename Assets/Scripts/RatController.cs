using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RatController : MonoBehaviour
{
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    public RuntimeAnimatorController deathAnimatorController;
    private bool fadeOut = false;
    private const float FADE_AMOUNT = 2f;

    public RatSpawner ratSpawnerScript;

    void Start()
    {
        collider = this.GetComponent<Collider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (fadeOut)
        {
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
            StartCoroutine(die());
        }
    }

    private IEnumerator die()
    {
        Animator anim = this.GetComponent<Animator>();
        anim.runtimeAnimatorController = deathAnimatorController;
        //TODO: play death sound
        ratSpawnerScript.ratCount--;
        yield return new WaitForSeconds(2);
        this.fadeOut = true;
    }
}

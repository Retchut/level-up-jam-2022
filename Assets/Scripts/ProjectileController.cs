using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool fadeOut = false;
    private const float FADE_AMOUNT = 2f;

    public CannonController cannonController;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            Color oldColor = this.spriteRenderer.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - FADE_AMOUNT * Time.deltaTime);
            this.spriteRenderer.color = newColor;

            if (this.spriteRenderer.color.a <= 0)
            {
                fadeOut = false;
                destroyProjectile();
            }
        }
        checkIfLeftTheScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rat"))
        {
            this.fadeOut = true;
            cannonController.score++;
        }
    }

    private void checkIfLeftTheScreen()
    {
        Renderer renderer = this.GetComponent<Renderer>();
        if(!renderer.isVisible)
        {
            destroyProjectile();
        }
    }

    private void destroyProjectile()
    {
        Destroy(this.gameObject);
    }
}

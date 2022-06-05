using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonController : MonoBehaviour
{
    enum mouseButtons {PRIMARY, SECONDARY, MIDDLE}
    private const float PROJECTILE_RANDOM_TORQUE_MIN = 25f;
    private const float PROJECTILE_RANDOM_TORQUE_MAX = 1000f;
    private const float PROJECTILE_MIN_FORCE = 1f;
    private const float PROJECTILE_MAX_FORCE = 100f;
    private const float PROJECTILE_FORCE_INCREMENT = 0.1f;
    private float force = PROJECTILE_MIN_FORCE;

    public RuntimeAnimatorController idleAnimController;
    public RuntimeAnimatorController fireAnimController;
    public GameObject grannyGO;

    private Vector3 mousePosition;

    public GameObject bulletPrefab;
    public Transform cannonBarrel;

    public Sprite[] projectiles;

    [HideInInspector] public int score = 0;
    public TMP_Text scoreText;

    public AudioSource shootSound;

// Update is called once per frame
void Update()
    {
        mousePosition = Input.mousePosition;

        float rotation = getCannonAngle();
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        handleInput();
        scoreText.text = "Score: " + score.ToString();
    }

    float getCannonAngle(){
        Vector2 cannonToMouse = Camera.main.ScreenToWorldPoint(mousePosition) - this.transform.position;
        cannonToMouse.Normalize();

        return Mathf.Atan2(cannonToMouse.y, cannonToMouse.x) * Mathf.Rad2Deg;
    }

    void handleInput()
    {
        if (Input.GetMouseButton((int)mouseButtons.PRIMARY))
        {
            if (force <= PROJECTILE_MAX_FORCE)
            {
                force += PROJECTILE_FORCE_INCREMENT;
            }
        }  
        else if (Input.GetMouseButtonUp((int)mouseButtons.PRIMARY)){
            Coroutine grannyCoroutine = StartCoroutine(changeGrannyAnim());
            shoot(force);
            force = PROJECTILE_MIN_FORCE;
        }
    }

    IEnumerator changeGrannyAnim()
    {
        Animator anim = grannyGO.GetComponent<Animator>();
        anim.runtimeAnimatorController = fireAnimController;
        yield return new WaitForSeconds(fireAnimController.animationClips[0].length);
        anim.runtimeAnimatorController = idleAnimController;
    }

    void shoot(float force)
    {
        shootSound.Play();
        GameObject clone = Instantiate(bulletPrefab, cannonBarrel.position, cannonBarrel.rotation);
        clone.GetComponent<SpriteRenderer>().sprite = getNextProjectileSprite();
        Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D>();

        cloneRB.gameObject.GetComponent<ProjectileController>().cannonController = this;

        cloneRB.AddForce(calculateProjectileForceVector(force), ForceMode2D.Impulse);
        cloneRB.AddTorque(Random.Range(PROJECTILE_RANDOM_TORQUE_MIN, PROJECTILE_RANDOM_TORQUE_MAX));
    }

    Vector2 calculateProjectileForceVector(float force)
    {
        float barrelAngleRad = getCannonAngle() * Mathf.Deg2Rad;
        float forceX = force * Mathf.Cos(barrelAngleRad);
        float forceY = force * Mathf.Sin(barrelAngleRad);
        return new Vector2(forceX, forceY);
    }

    Sprite getNextProjectileSprite()
    {
        int itemIndex = Random.Range(0, projectiles.Length);

        return projectiles[itemIndex];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    enum mouseButtons {PRIMARY, SECONDARY, MIDDLE}
    private const float PROJECTILE_RANDOM_TORQUE_MIN = 25;
    private const float PROJECTILE_RANDOM_TORQUE_MAX = 1000;



    private Vector3 mousePosition;

    public GameObject bulletPrefab;
    public Transform cannonBarrel;

    private float force = 10;

    public Sprite[] projectiles;

    public float testing = 1000;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        float rotation = getCannonAngle();
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        handleInput();
    }

    float getCannonAngle(){
        Vector2 cannonToMouse = Camera.main.ScreenToWorldPoint(mousePosition) - this.transform.position;
        cannonToMouse.Normalize();

        return Mathf.Atan2(cannonToMouse.y, cannonToMouse.x) * Mathf.Rad2Deg;
    }

    void handleInput()
    {
        if (Input.GetMouseButtonDown((int)mouseButtons.PRIMARY))
        {
            shoot();
        }  
    }

    void shoot()
    {
        GameObject clone = Instantiate(bulletPrefab, cannonBarrel.position, cannonBarrel.rotation);
        clone.GetComponent<SpriteRenderer>().sprite = getNextProjectileSprite();
        Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D>();

        cloneRB.AddForce(calculateProjectileForce(), ForceMode2D.Impulse);

        cloneRB.AddTorque(Random.Range(PROJECTILE_RANDOM_TORQUE_MIN, PROJECTILE_RANDOM_TORQUE_MAX));
    }

    Vector2 calculateProjectileForce()
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

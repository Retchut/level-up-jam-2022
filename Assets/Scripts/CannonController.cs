using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    enum mouseButtons {PRIMARY, SECONDARY, MIDDLE}

    private Vector3 mousePosition;

    public GameObject bulletPrefab;
    public Transform cannonBarrel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        Vector2 cannonToMouse = Camera.main.ScreenToWorldPoint(mousePosition) - this.transform.position;
        cannonToMouse.Normalize();

        float rotation = Mathf.Atan2(cannonToMouse.y, cannonToMouse.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        handleInput();
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
    }
}

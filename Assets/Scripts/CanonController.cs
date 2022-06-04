using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    private Vector3 mousePosition;
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
    }
}

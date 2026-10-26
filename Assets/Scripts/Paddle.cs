
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float x = worldPosition.x;

        if (x < -2.77f)
        {
            x = -2.77f;
        }

        if (x > 2.77f)
        {
            x = 2.77f;
        }

        transform.position = new Vector3(x, 0.15f, 0f);
    }
}


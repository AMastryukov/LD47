using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float velocity = 300f;
    [SerializeField] private float MIN_SCREEN_BOUND_Y = -448f;
    private int keyboardKey;

    // Initialize is to be called whenever this prefab is instantiated.
    // Sets the key column it is in and begins motion for its Rigidbody2D
    public void Initialize(int keyColumn)
    {
        keyboardKey = keyColumn;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity);
    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.y <= MIN_SCREEN_BOUND_Y)
        {
            Destroy(gameObject);
        }
    }
}

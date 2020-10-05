using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float velocity = 100f;

    public void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity);
    }
}

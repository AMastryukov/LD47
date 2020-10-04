using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public MusicMinigameManager musicMinigameManager;
    private int parentKey;
    [SerializeField] private float velocity = 300f;
    [SerializeField] private float MIN_SCREEN_BOUND_Y = -448f;

    // Initialize is to be called whenever this prefab is instantiated.
    // Sets the key row it is in, registers itself with the game manager,
    // and begins motion for its Rigidbody2D
    public void Initialize(int key)
    {
        parentKey = key;
        musicMinigameManager.RegisterBeat(gameObject.transform, parentKey);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity);
    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.y <= MIN_SCREEN_BOUND_Y)
        {
            musicMinigameManager.RemoveOldestBeat(parentKey);
            Destroy(gameObject);
        }
    }
}

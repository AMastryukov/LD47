using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Virtual_keyboard : MonoBehaviour
{
    [SerializeField] private AudioClip[] notes;

    private AudioSource piano;

    private void Awake()
    {
        piano = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { piano.PlayOneShot(notes[0]); }
        if (Input.GetKeyDown(KeyCode.W)) { piano.PlayOneShot(notes[1]); }
        if (Input.GetKeyDown(KeyCode.E)) { piano.PlayOneShot(notes[2]); }
        if (Input.GetKeyDown(KeyCode.R)) { piano.PlayOneShot(notes[3]); }
        if (Input.GetKeyDown(KeyCode.T)) { piano.PlayOneShot(notes[4]); }
        if (Input.GetKeyDown(KeyCode.Y)) { piano.PlayOneShot(notes[5]); }
        if (Input.GetKeyDown(KeyCode.U)) { piano.PlayOneShot(notes[6]); }
        if (Input.GetKeyDown(KeyCode.I)) { piano.PlayOneShot(notes[7]); }
        if (Input.GetKeyDown(KeyCode.O)) { piano.PlayOneShot(notes[8]); }
        if (Input.GetKeyDown(KeyCode.P)) { piano.PlayOneShot(notes[9]); }

    }
}
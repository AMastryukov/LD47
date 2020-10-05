using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSounds : MonoBehaviour
{
    //audiosource
    [SerializeField]
    AudioSource main;

    //Piano notes
    [SerializeField]
    AudioClip[] Notes;

    //Keyboard Clicks
    [SerializeField]
    AudioClip[] clicks;
    [SerializeField]
    AudioClip select_coding_game;

    //art game sounds
    [SerializeField]
    AudioClip select_art_game;

    //UI button sound
    [SerializeField]
    AudioClip UI_Button;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_click_sound()
    {
        int index = Random.Range(0, clicks.Length - 1);
        main.PlayOneShot(clicks[index]);
    }

    public void Play_note(KeyCode key)
    {
        if (key == KeyCode.Q) { main.PlayOneShot(Notes[0]); }
        if (key == KeyCode.W) { main.PlayOneShot(Notes[1]); }
        if (key == KeyCode.E) { main.PlayOneShot(Notes[2]); }
        if (key == KeyCode.R) { main.PlayOneShot(Notes[3]); }
    }

    public void coding_game()
    {
        main.PlayOneShot(select_coding_game);
    }
    public void music_game()
    {
        main.PlayOneShot(Notes[0]);
    }
    public void art_game()
    {
        main.PlayOneShot(select_art_game);
    }
    public void UI_Button_press()
    {
        main.PlayOneShot(UI_Button);
    }


}

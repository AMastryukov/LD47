using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip pregameTrack;
    [SerializeField] private AudioClip[] gameplayTracks;
    [SerializeField] private AudioClip buttonHighlight;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip[] pianoNotesHit;
    [SerializeField] private AudioClip[] pianoNotesMiss;
    [SerializeField] private AudioClip codingGame;
    [SerializeField] private AudioClip artGame;
    [SerializeField] private AudioClip[] keyboardClicks;

    public AudioClip CurrentSong 
    {
        get 
        { 
            return musicSource.clip;
        }
    }

    private void Awake()
    {
        GameManager.onGameStarted += PlayGameTrack;
        GameManager.onGameFinished += PlayPreGameTrack;

        PlayPreGameTrack();
    }

    private void OnDestroy()
    {
        GameManager.onGameStarted -= PlayGameTrack;
        GameManager.onGameFinished -= PlayPreGameTrack;
    }

    public void PlayGameTrack()
    {
        // Select a random soundtrack and play it
        musicSource.clip = gameplayTracks[UnityEngine.Random.Range(0, gameplayTracks.Length)];
        musicSource.loop = false;
        musicSource.Play();
    }

    public void PlayPreGameTrack()
    {
        musicSource.clip = pregameTrack;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffectSource.PlayOneShot(clip);
    }

    public void PlayButtonHighlight()
    {
        soundEffectSource.PlayOneShot(buttonHighlight);
    }

    public void PlayButtonClick()
    {
        soundEffectSource.PlayOneShot(buttonClick);
    }

    public void PlayPianoNote(int key, bool hit)
    {
        if (hit) { soundEffectSource.PlayOneShot(pianoNotesHit[key], 0.25f); }
        else { soundEffectSource.PlayOneShot(pianoNotesMiss[key]); }
    }

    public void PlayMusicGameSound()
    {
        soundEffectSource.PlayOneShot(pianoNotesHit[Random.Range(0, pianoNotesHit.Length)], 0.25f);
    }

    public void PlayCodingGameSound()
    {
        soundEffectSource.PlayOneShot(codingGame);
    }

    public void PlayArtGameSound()
    {
        soundEffectSource.PlayOneShot(artGame);
    }

    public void PlayRandomClick()
    {
        soundEffectSource.PlayOneShot(keyboardClicks[Random.Range(0, keyboardClicks.Length)]);
    }
}

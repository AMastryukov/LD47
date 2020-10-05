using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource preGameTrack;
    [SerializeField] private AudioSource gameplayTrack;
    [SerializeField] private AudioClip[] gameplaySongs;

    private void Awake()
    {
        GameManager.onGameStarted += PlayGameTrack;
        GameManager.onGameFinished += PlayPreGameTrack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStarted -= PlayGameTrack;
        GameManager.onGameFinished -= PlayPreGameTrack;
    }

    public void PlayGameTrack()
    {
        // Select a random soundtrack and play it
        gameplayTrack.clip = gameplaySongs[UnityEngine.Random.Range(0, gameplaySongs.Length)];
        gameplayTrack.Play();

        StartCoroutine(FadeOutTrack(preGameTrack));
    }

    public void PlayPreGameTrack()
    {
        preGameTrack.Play();

        StartCoroutine(FadeOutTrack(gameplayTrack));
    }

    private IEnumerator FadeOutTrack(AudioSource track)
    {
        float defaultVolume = track.volume;
        float soundtrackFadeOutTime = 1f;

        while (track.volume > 0f)
        {
            track.volume -= 0.025f;

            yield return new WaitForSeconds(soundtrackFadeOutTime * 0.025f);
        }

        track.Stop();
        track.volume = defaultVolume;
    }
}

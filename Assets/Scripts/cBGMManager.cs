using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBGMManager : MonoBehaviour
{
    public AudioClip[] clips; //배경음악들
    private AudioSource source;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 1f;
    }

    public void Play(int _playMusicTrack)
    {
        source.clip = clips[_playMusicTrack];
        source.Play();
    }

    public void SetVolumn(float _volumn)
    {
        source.volume = _volumn;
    }
}

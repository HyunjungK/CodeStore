using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectSound
{
    public string name; //효과음 이름

    public AudioClip clip;
    private AudioSource effectSource;

    public float Volume;
    public bool loop;

    public void SetSource(AudioSource _source)
    {
        effectSource = _source;
        effectSource.clip = clip;
        effectSource.loop = loop;
    }

    public void SetVolume()
    {
        effectSource.volume = Volume;
    }

    public void Play()
    {
        effectSource.Play();
    }
}
public class cAudioManager : MonoBehaviour
{
    static public cAudioManager instance;

    [SerializeField]
    public EffectSound[] sounds;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);
            for (int i = 0; i < sounds.Length; i++)
            {
                GameObject SoundObject = new GameObject("사운드 파일 이름 : " + i + "=" + sounds[i].name);
                sounds[i].SetSource(SoundObject.AddComponent<AudioSource>());
                SoundObject.transform.SetParent(this.transform);
            }
            instance = this;
        }
    }

    public void Play(string _name)
    {
        for(int i=0; i<sounds.Length; i++)
        {
            if(_name==sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void SetVolume(float _volume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Volume = _volume;
            sounds[i].SetVolume();
        }


    }
}

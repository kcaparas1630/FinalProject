using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    public AudioClip monsterScream;
    public AudioClip dungeonSound;
    public AudioSource source;
    
    public void EnvironmentSound()
    {
        source.clip = dungeonSound;
        source.Play();
    }
    public void MonsterAwake()
    {
        source.clip = monsterScream;
        source.Play();
    }
}

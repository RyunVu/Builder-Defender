using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    public static SoundManager Instance{ get; private set; }

    public enum Sound {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }

    private AudioSource _audioSource;
    private Dictionary<Sound, AudioClip> _soundAudioClipsDictionary;

    private void Awake() {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        _soundAudioClipsDictionary = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound))) {
            _soundAudioClipsDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }   

    }

    public void PlaySound(Sound sound) {        
        _audioSource.PlayOneShot(_soundAudioClipsDictionary[sound]);
    }
}

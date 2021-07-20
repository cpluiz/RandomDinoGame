using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour{
    private static AudioController _instance;
    public static AudioController instance{get{return _instance;}}
    private AudioSource source;
    private AudioSource musicSource;

    private float BGMVolume, SFXVolume, UIVolume;

    void Awake(){
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(gameObject);
            source = GetComponent<AudioSource>();
            GameController.SetupVolumes();
        }else{
            Destroy(gameObject);
        }
    }

    public static void PlaySFX(AudioClip clip){
        _instance.source.PlayOneShot(clip, _instance.SFXVolume);
    }
    public static void PlayUI(AudioClip clip){
        _instance.source.PlayOneShot(clip, _instance.UIVolume);
    }
    public static void PlayBGM(AudioClip clip){
        _instance.musicSource.clip = clip;
        _instance.musicSource.Play();
    }

    public static void SetBGMVolume(float volumeLevel, bool saveVolume = false){
        _instance.BGMVolume = volumeLevel;
        if(saveVolume)
            GameController.SetVolume("BGMVolume", volumeLevel);
        if(_instance.musicSource != null)
            _instance.musicSource.volume = _instance.BGMVolume;
    }
    public static void SetSFXVolume(float volumeLevel, bool saveVolume = false){
        _instance.SFXVolume = volumeLevel;
        if(saveVolume)
            GameController.SetVolume("SFXVolume", volumeLevel);
    }
    public static void SetUIVolume(float volumeLevel, bool saveVolume = false){
        _instance.UIVolume = volumeLevel;
        if(saveVolume)
            GameController.SetVolume("UIVolume", volumeLevel);
    }
    public static void SetBGMSource(AudioSource bgmSource){
        _instance.musicSource = bgmSource;
        _instance.musicSource.volume = _instance.BGMVolume;
    }
}

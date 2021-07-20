using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
    private static GameController _instance;
    public static GameController instance{get{return _instance;}}
    public static int PlayerLifes{get{return _instance.playerLifes;}}
    private int playerLifes;
    void Awake(){
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetupPlayer();
        }else
            Destroy(this.gameObject);
    }

    public static void SetupVolumes(){
        AudioController.SetBGMVolume(GetVolume("BGMVolume"));
        AudioController.SetSFXVolume(GetVolume("SFXVolume"));
        AudioController.SetUIVolume(GetVolume("UIVolume"));
    }

    private void SetupPlayer(){
        playerLifes = GetCurrentLifes();
    }
    public static float GetVolume(string volumeType){
        return PlayerPrefs.GetFloat(volumeType, 1);
    }
    public static void SetVolume(string volumeType, float volume){
        PlayerPrefs.SetFloat(volumeType, volume);
    }

    //TODO - move this data to savefile instead of player preferences
    public static void SetCurrentLifes(int lifes){
        PlayerPrefs.SetInt("PlayerLifes", lifes);
    }
    public static int GetCurrentLifes(){
        return PlayerPrefs.GetInt("PlayerLifes", 3);
    }
    public static void LoseLife(){
        _instance.playerLifes --;
    }

    public static void DeleteSaveGame(){
        PlayerPrefs.SetInt("PlayerLifes", 3);
        PlayerPrefs.SetInt("LastLevel", 1);
        _instance.playerLifes = 3;
    }
    public static int LastLoadedLevel(){
        return PlayerPrefs.GetInt("LastLevel");
    }
}

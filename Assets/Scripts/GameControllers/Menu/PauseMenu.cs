using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour{
    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pauseWindow;
    [SerializeField]
    private GameObject optionsMenu, pauseMenu, audioMenu;
    [Header("Audio Menu")]
    [SerializeField]
    private GameObject AudioWindow;
    public Slider BGMVolume, SFXVolume, UIVolume;
    private List<GameObject> openedWindows;
    void Awake(){
        openedWindows = new List<GameObject>();
    }
    public void GoToMainMenu(){
        CloseAllWindows();
        LevelManager.LoadScene(0);
    }
    public void OpenMenu(bool pauseGame){
        pauseWindow.SetActive(true);
        if(pauseGame){
            Time.timeScale = 0;
            OpenWindow(pauseMenu);
        }else
            OpenWindow(optionsMenu);
    }
    public void OpenAudioMenu(){
        OpenWindow(audioMenu);
    }
    private void OpenWindow(GameObject window){
        HideLastWindow();
        window.SetActive(true);
        openedWindows.Add(window);
    }
    private void HideLastWindow(){
        if(openedWindows.Count>0)
            openedWindows[openedWindows.Count-1].SetActive(true);
    }
    public void CloseMenu(){
        GameObject lastWindow = openedWindows[openedWindows.Count-1];
        if(lastWindow != null){
            lastWindow.SetActive(false);
            openedWindows.Remove(lastWindow);
        }
        if(openedWindows.Count<=0)
            CloseAllWindows();
        else
            openedWindows[openedWindows.Count-1].SetActive(true);
    }
    public void CloseAllWindows(){
        pauseWindow.gameObject.SetActive(false);
        if(openedWindows.Count > 0){
            foreach(GameObject window in openedWindows)
                window.SetActive(false);
        }
        openedWindows = new List<GameObject>();
        Time.timeScale = 1;
    }
    public void SetBGMVolume(float volume){
        BGMVolume.value = volume;
    }
    public void SetSFXVolume(float volume){
        SFXVolume.value = volume;
    }
    public void SetUIVolume(float volume){
        UIVolume.value = volume;
    }
    public void ChangeBGMVolume(float volume){
        AudioController.SetBGMVolume(volume, true);
    }
    public void ChangeSFXVolume(float volume){
        AudioController.SetSFXVolume(volume, true);
    }
    public void ChangeUIVolume(float volume){
        AudioController.SetUIVolume(volume, true);
    }
}

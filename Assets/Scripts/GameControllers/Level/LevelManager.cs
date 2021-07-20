using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour{
    private static LevelManager _instance;
    public static LevelManager instance{
        get{return _instance;}
    }
    public static int currentCheckPointID{get{ return _instance.checkPointID;}}
    public static bool isOnMainMenu{get{return SceneManager.GetActiveScene().buildIndex == 0;}}
    private Level currentLevel;
    private UIController uiController;
    private PauseMenu pauseMenuController;
    private string currentLevelName;
    private Transform checkPoint;
    private int checkPointID = 0;
    private PlayerController player;
    public PlayerController playerPrefab;
    private Cinemachine.CinemachineVirtualCamera cam;
    void Awake(){
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(_instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
            cam = GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
            uiController = GetComponentInChildren<UIController>();
            pauseMenuController = GetComponentInChildren<PauseMenu>();
            pauseMenuController.CloseAllWindows();
            checkPointID = 0;
        }else
            Destroy(gameObject);
    }

    public static void ReloadScene(){
        if(GameController.PlayerLifes >= 0)
            SceneManager.LoadScene(_instance.currentLevelName);
        else
            SceneManager.LoadScene(0);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        currentLevel = FindObjectOfType<Level>();
        if(currentLevelName == null || currentLevelName != scene.name)
            ChangeManagedScene();
        SetupScene(LevelManager.isOnMainMenu);
        SetupGameConfig();
        pauseMenuController.CloseAllWindows();
    }
    void SetupScene(bool isMainMenu){
        AudioController.SetBGMSource(Camera.main.GetComponent<AudioSource>());
        AudioController.PlayBGM(currentLevel.bgmMusic);
        cam.gameObject.SetActive(!isMainMenu);
        uiController.ShowGameUI(!isMainMenu);
        if(!isMainMenu){
            uiController.GetCurrentCamera();
            GoToCheckpoint();
        }
    }
    void SetupGameConfig(){
        uiController.SetLifes(GameController.PlayerLifes);
        pauseMenuController.SetBGMVolume(GameController.GetVolume("BGMVolume"));
        pauseMenuController.SetSFXVolume(GameController.GetVolume("SFXVolume"));
        pauseMenuController.SetUIVolume(GameController.GetVolume("UIVolume"));
    }
    void ChangeManagedScene(){
        currentLevelName = currentLevel.sceneName;
        checkPointID = 0;
    }
    void GoToCheckpoint(){
        checkPoint = currentLevel.checkpoints[checkPointID].t;
        player = Instantiate<PlayerController>(playerPrefab, checkPoint.position, Quaternion.identity);
        cam.m_Follow = player.transform;
        cam.m_LookAt = player.transform;
    }

    public static void SetCheckpoint(CheckpointPosition checkpoint){
        int checkpointID = GetCheckpointID(checkpoint);
        _instance.checkPointID = _instance.checkPointID < checkpointID ? checkpointID : _instance.checkPointID;
    }

    public static int GetCheckpointID(CheckpointPosition checkpoint){
        return System.Array.IndexOf(_instance.currentLevel.checkpoints, checkpoint);
    }
    public static void LoadScene(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }

    public static void OpenMenu(bool pause = false){
        _instance.pauseMenuController.OpenMenu(pause);
    }

    void OnDisable(){
        if(_instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour{
    public static void NewGame(){
        LevelManager.LoadScene(1);
        GameController.DeleteSaveGame();
    }
    public static void LoadGame(){
        LevelManager.LoadScene(GameController.LastLoadedLevel());
    }

    public static void OpenMenuWindow(){
        LevelManager.OpenMenu();
    }
}

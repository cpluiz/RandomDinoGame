using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour{
    [SerializeField]
    private Canvas UICanvas;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private TMP_Text lifes;

    void Awake(){
        UICanvas = GetComponent<Canvas>();
    }

    public void ShowGameUI(bool showUI){
        gameUI.SetActive(showUI);
    }
    public void GetCurrentCamera(){
        UICanvas.worldCamera = Camera.main;
    }

    public void SetLifes(int lifes){
        this.lifes.SetText(lifes.ToString());
    }
}

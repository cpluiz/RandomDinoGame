using UnityEngine;
[ExecuteAlways]
public class Level : MonoBehaviour{
    static Level _instance;
    public static Level instance{get{return _instance;}}

    void Awake(){
        _instance = this;
    }
    [ExecuteInEditMode]
    void Start(){
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
    public string sceneName;
    public AudioClip bgmMusic;
    public CheckpointPosition[] checkpoints;
}

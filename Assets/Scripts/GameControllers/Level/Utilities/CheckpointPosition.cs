using UnityEngine;
[System.Serializable]
public class CheckpointPosition : MonoBehaviour{
    public Transform t;
    public bool isStartPosition = false;
    public Sprite disabledCheckPoint, enabledCheckPoint;
    private SpriteRenderer sp;

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        ChangeCheckpointSprite();
    }

    void ChangeCheckpointSprite(){
        sp.sprite = LevelManager.GetCheckpointID(this) == LevelManager.currentCheckPointID ? enabledCheckPoint : disabledCheckPoint;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(!other.CompareTag("Player"))
            return;
        LevelManager.SetCheckpoint(this);
        ChangeCheckpointSprite();
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillZone : MonoBehaviour{
    private Collider2D triggerArea;
    void Start(){
        triggerArea = GetComponent<Collider2D>();
        triggerArea.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D obj){
        if(obj.gameObject.CompareTag("Enemy"))
            Destroy(obj.gameObject);
        if(obj.gameObject.CompareTag("Player"))
            obj.GetComponent<PlayerController>().LoseLife();
    }
}

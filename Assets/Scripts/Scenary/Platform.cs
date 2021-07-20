using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour{
    private MovablePlatform movablePlatform;
    public bool playerOnPlatform;

    void Awake(){
        movablePlatform = GetComponentInParent<MovablePlatform>();
        if(movablePlatform.autoStart)
            movablePlatform.StartMovement();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(!movablePlatform.isStarted || !movablePlatform.ping) return;
        if(other.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerController>().LoseLife();
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            playerOnPlatform = true;
            if(!movablePlatform.autoStart)
                movablePlatform.StartMovement();
            other.gameObject.GetComponent<PlayerController>().SetPlatformReference(movablePlatform);
        }
    }
    
    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            playerOnPlatform = false;
            other.gameObject.GetComponent<PlayerController>().RemovePlatformReference();
        }
    }
}

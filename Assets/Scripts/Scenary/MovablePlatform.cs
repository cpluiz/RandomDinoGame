using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour{
    [Header("Movable Platform Configurations")]
    [Range(1f, 15f)]
    public float movementTime;
    [SerializeField]
    private Transform startPoint, endPoint, platformTransform;
    [SerializeField]
    private Platform platform;
    private float timeElapsed;
    public bool ping = true; // true = Ping, false = Pong (vai e volta)
    private Vector3 _lastPosition, _deltaPosition;
    public Vector3 deltaPosition { get{ return _deltaPosition;}}
    public bool autoStart = true;
    public bool isStarted;
    public void StartMovement(){
        isStarted = true;
    }

    void FixedUpdate(){
        if(!isStarted) return;
        MovePlatform();
    }

    private void MovePlatform(){
        _lastPosition = platformTransform.position;
        platformTransform.position = Vector3.Lerp(startPoint.position, endPoint.position, timeElapsed / movementTime);
        _deltaPosition = platformTransform.position - _lastPosition;
        timeElapsed = 
            (timeElapsed < movementTime && ping) ? timeElapsed += Time.deltaTime :
            (timeElapsed > 0 && !ping) ? timeElapsed -= Time.deltaTime :
            ping ? movementTime : 0;
        //Essa parte varia da plataforma automática para a de contato
        if(autoStart)
            ping = (timeElapsed < movementTime && ping) || (timeElapsed == 0);
        else{
            ping = platform.playerOnPlatform;
            
        }
    }

    //return the Y position for player foot
    public float GetFootReference(){
        return platform.GetComponentInChildren<Renderer>().bounds.max.y;
    }
}

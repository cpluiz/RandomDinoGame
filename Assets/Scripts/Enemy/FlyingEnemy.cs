using System.Collections;
using UnityEngine;
public class FlyingEnemy : Enemy{
    
    [Range(1,5)]
    public float frequence = 1;
    [Range(1,30)]
    public float verticalAmplitude, horizontalAmplitude = 1;
    private Vector3 startPosition;
    new protected void Awake(){
        base.Awake();
        startPosition = transform.position;
    }
    void Update(){
        if(isDiyng || Time.timeScale == 0) return;
        transform.Translate(new Vector3(facingDirection * enemySpeed * Time.deltaTime, Mathf.Sin(Time.time * frequence)/(100/verticalAmplitude),0));
        transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                startPosition.x - horizontalAmplitude,
                startPosition.x + horizontalAmplitude
            ), transform.position.y, 1
        );
    }
    new void LateUpdate(){
        if(Time.timeScale == 0) return;
        if(transform.position.x <= startPosition.x - horizontalAmplitude || transform.position.x >= startPosition.x + horizontalAmplitude)
            base.FlipDirection();
        base.LateUpdate();
    }
    new protected IEnumerator DestroyEnemy(){
        yield return null;
    }
}
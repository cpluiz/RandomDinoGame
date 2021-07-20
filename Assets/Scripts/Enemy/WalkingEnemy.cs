using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy{
    [SerializeField]
    private Sensor wallSensor, floorSensor;
    [SerializeField]
    void Update(){
        if(isDiyng) return;
        transform.Translate(Vector2.right * facingDirection * enemySpeed * Time.deltaTime);
    }

    new void LateUpdate(){
        if(wallSensor.IsActive || (floorSensor != null && floorSensor.IsActive))
            FlipDirection();
        base.LateUpdate();
    }

    new protected IEnumerator DestroyEnemy(){
        enemyAnimator.SetBool("isDying", isDiyng);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(enemyAnimator.GetNextAnimatorClipInfo(0)[0].clip.length);
        Destroy(gameObject);
    }

}

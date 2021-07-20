using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{
    [Header("Enemy configuration")]
    [Range(1f, 20f)]
    public float enemySpeed = 1;
    public AudioClip diyngSound;
    protected Animator enemyAnimator;
    protected bool isDiyng, isKilling;
    [SerializeField]
    protected int facingDirection=1; // 1 right, -1 left
    protected void Awake(){
        enemyAnimator = GetComponentInChildren<Animator>();
    }
    protected void LateUpdate(){
        transform.localScale = new Vector3(facingDirection, 1, 1);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(isDiyng || isKilling) return;
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerController>().ApplyJump();
            isDiyng = true;
            AudioController.PlaySFX(diyngSound);
            StartCoroutine(nameof(DestroyEnemy));
        }
    }

    protected void FlipDirection(){
        facingDirection *= -1;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(isDiyng) return;
        isKilling = other.gameObject.CompareTag("Player");
        if(isKilling)
            other.gameObject.GetComponent<PlayerController>().LoseLife();
    }

    protected IEnumerator DestroyEnemy(){
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    [Header("Player configuration")]
    [Range(1f, 10f)]
    public float playerSpeed = 1;
    [Range(1f, 5f)]
    public float runMultiplier = 1.5f;
    [Range(1f, 100f)]
    public float jumpForce = 1;
    public LayerMask floorLayer;
    public Transform footSensor;
    [Header("Player Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip gameOver;

    // 1 is Right, -1 is left
    private int playerDirection = 1;
    private bool isWalking = false;
    private bool isRunning, isJumping, isOnFloor, jumpButtonPressed = false;
    [SerializeField]
    private MovablePlatform platformTransformReference;
    private bool isOnPlatform { get { return platformTransformReference != null ;}}

    //Animation Controller
    private Animator playerAnimator;
    private Rigidbody2D rb;
    void Start(){
        playerAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        CheckIsWalking();
        CheckIsRunning();
        CheckIsJumping(CheckIsOnGround());
    }
    void FixedUpdate(){
        WalkOrRun(playerDirection);
        CheckOnPlatform();
        Jump();
    }

    void LateUpdate(){
        playerAnimator.SetBool("walking", isWalking);
        playerAnimator.SetBool("running", isRunning);
        playerAnimator.SetBool("jumping", isJumping || !isOnFloor);
        playerAnimator.SetFloat("horizontalSpeed", Mathf.Abs(rb.velocity.x));
        playerAnimator.SetFloat("verticalSpeed", rb.velocity.y);
    }
    private void CheckIsWalking(){
        isWalking = Input.GetAxis("Horizontal") != 0;
        playerDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
    }
    private void CheckIsRunning(){
        isRunning = Input.GetButton("Run");
    }
    private void CheckIsJumping(bool grounded){
        isOnFloor = grounded;
        //Se estou segurando o botão de pulo, checar quando largo
        if(jumpButtonPressed)
            jumpButtonPressed = Input.GetButton("Jump");
        //Se o personagem não está no chão ou o personagem está pulando
        if(!isOnFloor || isJumping) return; //Não preciso verficar mais nada, sai da função
        isJumping = Input.GetButtonDown("Jump");
        jumpButtonPressed = Input.GetButton("Jump");
    }
    private void WalkOrRun(int dir){
        if(!isWalking) return;
        transform.Translate(Vector2.right * dir * playerSpeed * (isRunning ? runMultiplier : 1) * Time.fixedDeltaTime);
        transform.localScale = new Vector3(dir,1f,1f);
    }
    private void Jump(){
        if(isJumping && isOnFloor){
            ApplyJump();
            AudioController.PlaySFX(jumpSound);
            isJumping = false;
        }
        if(isOnPlatform)
            return;
        if(!isOnFloor && !jumpButtonPressed && rb.velocity.y > 0 && rb.velocity.y < jumpForce/2)
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private void CheckOnPlatform(){
        if(!isOnPlatform) return;
        transform.position += platformTransformReference.deltaPosition;
    }
    public void ApplyJump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    public void SetPlatformReference(MovablePlatform platformReference){
        platformTransformReference = platformReference;
        transform.position = new Vector3(transform.position.x, platformReference.GetFootReference());
    }
    public void RemovePlatformReference(){
        platformTransformReference = null;
    }
    private bool CheckIsOnGround(){
        RaycastHit2D hit = Physics2D.CircleCast(footSensor.position, 0.1f, Vector2.down, 1f, floorLayer);
        return hit.collider != null;
    }

    public void LoseLife(){
        Destroy(this);
        GameController.LoseLife();
        LevelManager.ReloadScene();
    }
}
